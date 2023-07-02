using CarService.Hubs;
using CarService.Models;
using CarService.Reposatories;
using CarService.Repository;
using CarService.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarService.Controllers
{

    [Authorize(Roles = "منسق")]
    public class CoordenatorController : Controller
    {
        CarServiceEntities Context;

        IData data;
        IUserInfoRepsatories userInfoRepsatories;

        IHubContext<AddOrderHub> _hubContext;
        IWebHostEnvironment _webHost;
        public CoordenatorController(CarServiceEntities _Context, IData data, IUserInfoRepsatories userInfoRepsatories,
            IHubContext<AddOrderHub> hubContext, IWebHostEnvironment _webHost)
        {
            Context = _Context;
            this.data = data;
            _hubContext = hubContext;
            this.userInfoRepsatories = userInfoRepsatories;
            this._webHost = _webHost;

        }
        public IActionResult Index()
        {

            var UserName = User.Identity.Name;
            ViewBag.UserName = Context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            string id = Context.Users.Where(u => u.UserName == UserName).Select(u => u.Id).FirstOrDefault();
            int CityID = Context.Coordinator.Where(c => c.Id == id).Select(c => c.CityID).FirstOrDefault();
            //City City = Context.ClientData.Where(c => c.City == City.Name).ToList();
            City City = Context.City.FirstOrDefault(c => c.Id == CityID);
            // Context.ClientData.Where(c => c.City == City.Name).ToList();
            ViewData["Times"] = Context.Times.ToList();
            List<CallCenterData> clientData = new List<CallCenterData>();
            List<CoordinatorVM> coordinatorVMs = new List<CoordinatorVM>();
            int DamamId = Context.City.Where(c => c.Name == "الدمام").Select(c => c.Id).FirstOrDefault();
            int GadahId = Context.City.Where(c => c.Name == "جده").Select(c => c.Id).FirstOrDefault();
            //need to rewrite dmam correctly!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (City.Id == DamamId || City.Id == GadahId)
            {
                clientData = Context.CallCenterData.Where(c => c.PreRequst.City.Id == DamamId || c.PreRequst.City.Id == GadahId).
                    Include(c => c.PreRequst).ToList();

            }
            else
            {
                clientData = Context.CallCenterData.Where(c => c.PreRequst.City.Name == City.Name).Include(c => c.PreRequst).ToList();
            }
            foreach (var item in clientData)
            {
                if (item.ServiceDate.Date.ToString() == DateTime.Now.AddDays(1).Date.ToString() && item.CoordenaitorExist == true)
                {

                    coordinatorVMs.Add(new CoordinatorVM
                    {
                        Id = item.PreReqId,
                        Notes = item.Notes,
                        Date = item.ServiceDate.ToString(),
                        City = item.PreRequst.City.Name,
                        Destrict = item.Destrict,
                        Name = item.Name,
                        Service = item.Service,
                        PhoneNumber = item.PhoneNumber,
                        OrderStatus = item.OrderStatus,
                        Price = item.Price,
                        ServiceDate = (DateTime)item.ServiceDate,

                    });
                }

            }

            return View(coordinatorVMs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //technical id not in function yet
        public async Task<IActionResult> CoordinatorUpdatedData(
            [Bind("Id, Date , City , Destrict,AssignDate,AssignTime,  " +
            "Name ,Price ,ServiceDate ,TabbyID,   PaymentImageFile , PaymentImage," +
            "TechnicanID,PaymentMethod,Service ,IsPayed      ")] CoordinatorVM coordinator)
        {
            //ClientData clientData = new ClientData();
            CoordinatorData coordinatorData = new CoordinatorData();
            if (ModelState.IsValid)
            {
                #region Image
                if (coordinator.PaymentImageFile != null)
                {
                    string wwwRootPath = _webHost.WebRootPath;
                    string FileName = Path.GetFileNameWithoutExtension(coordinator.PaymentImageFile.FileName);
                    string Extension = Path.GetExtension(coordinator.PaymentImageFile.FileName);
                    coordinator.PaymentImage = FileName + Extension;
                    string path = Path.Combine(wwwRootPath + "/UploadedImgs/" + (FileName + Extension));
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await coordinator.PaymentImageFile.CopyToAsync(fileStream);
                    }
                }
                #endregion

                #region OldClienData
                /*clientData = Context.ClientData.FirstOrDefault(c => c.Id == coordinator.Id);
                        clientData.AssignDate = coordinator.AssignDate;
                        clientData.AssignTime = coordinator.AssignTime;
                        clientData.PaymentMethod = coordinator.PaymentMethod;
                        clientData.IsPayed = coordinator.IsPayed;
                        clientData.TabbyID = coordinator.TabbyID;
                        clientData.PaymentImage = coordinator.PaymentImage;
                        clientData.TechnicanID = coordinator.TechnicanID;
                        clientData.CoordenaitorExist = false;
                        clientData.TechnicanExist = true;*/
                #endregion

                Context.CallCenterData.FirstOrDefault(c => c.PreReqId == coordinator.Id).CoordenaitorExist = false;
                coordinatorData.PreReqId = coordinator.Id;
                coordinatorData.City = coordinator.City;
                coordinatorData.AssignDate = coordinator.AssignDate;
                coordinatorData.AssignTime = coordinator.AssignTime;
                coordinatorData.PaymentMethod = coordinator.PaymentMethod;
                coordinatorData.IsPayed = coordinator.IsPayed;
                coordinatorData.TabbyID = coordinator.TabbyID;
                coordinatorData.PaymentImage = coordinator.PaymentImage;
                coordinatorData.TechnicanID = coordinator.TechnicanID;
                coordinatorData.TechnicanExist = true;

                Context.CoordinatorData.Add(coordinatorData);
                Context.SaveChanges();






                TechnicalOrderDay technicalOrderDay = Context.TechnicalOrderDay
                    .FirstOrDefault(t => t.TechnicalId == coordinator.TechnicanID && t.ServiceDay == coordinator.AssignDate);

                if (technicalOrderDay == null)
                {
                    technicalOrderDay = new TechnicalOrderDay();
                    technicalOrderDay.TechnicalId = coordinator.TechnicanID;
                    technicalOrderDay.ServiceDay = coordinator.AssignDate;
                    Context.TechnicalOrderDay.Add(technicalOrderDay);
                    Context.SaveChanges();
                    TechnicalOrderDayTime technicalOrderDayTime = new TechnicalOrderDayTime();
                    technicalOrderDayTime.OrderDayID = technicalOrderDay.Id;
                    technicalOrderDayTime.ClientDataId = coordinatorData.PreReqId;
                    technicalOrderDayTime.TimeId = int.Parse(coordinatorData.AssignTime);

                    Context.TechnicalOrderDayTime.Add(technicalOrderDayTime);
                    Context.SaveChanges();

                }
                else
                {
                    TechnicalOrderDayTime technicalOrderDayTime = new TechnicalOrderDayTime();
                    technicalOrderDayTime.OrderDayID = technicalOrderDay.Id;
                    technicalOrderDayTime.ClientDataId = coordinatorData.PreReqId;
                    technicalOrderDayTime.TimeId = int.Parse(coordinatorData.AssignTime);
                    Context.TechnicalOrderDayTime.Add(technicalOrderDayTime);
                    Context.SaveChanges();
                }

                Context.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public IActionResult GetTechnicans(string Date, string City, int TimeId)
        {
            List<Technican> Technicans = Context.Technicans.Where(t => t.City.Name == City).Include(t => t.User).ToList();
            List<Technican> AssignTech = new List<Technican>();
            foreach (Technican Tech in Technicans)
            {
                TechnicalOrderDay technicalOrderDay =
                    Context.TechnicalOrderDay
                    .FirstOrDefault(t => t.ServiceDay.Date.ToString() == Date && t.TechnicalId == Tech.Id);

                if (technicalOrderDay == null)
                {
                    AssignTech.Add(Tech);
                }
                else
                {
                    int CounterOrder = 0;
                    List<TechnicalOrderDayTime> technicalOrderDayTimes = Context.TechnicalOrderDayTime
                        .Where(t => t.OrderDayID == technicalOrderDay.Id).ToList();
                    if (technicalOrderDayTimes.Count < 4)
                    {

                        foreach (TechnicalOrderDayTime technicalOrderDT in technicalOrderDayTimes)
                        {
                            if (TimeId + 8 < technicalOrderDT.TimeId || TimeId - 8 > technicalOrderDT.TimeId)
                            {
                                CounterOrder++;
                            }
                        }
                        if (CounterOrder == technicalOrderDayTimes.Count)
                        {
                            AssignTech.Add(Tech);
                        }
                    }
                }

            }
            return Json(AssignTech);
        }
    }
}