using CarService.Hubs;
using CarService.Models;
using CarService.Reposatories;
using CarService.Repository;
using CarService.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarService.Controllers
{


    [Authorize(Roles = "فني")]
    public class TechnicanController : Controller
    {
        CarServiceEntities context;
        IData data;
        IUserInfoRepsatories userInfoRepsatories;
        IWebHostEnvironment _webHost;

        //constructor
        public TechnicanController(CarServiceEntities context, IData data, IUserInfoRepsatories userInfoRepsatories, IWebHostEnvironment _webHost)
        {
            this.context = context;
            this.data = data;
            this.userInfoRepsatories = userInfoRepsatories;
            this._webHost = _webHost;
        }

        public IActionResult Index()
        {


            ViewBag.UserName = context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
            var UserName = User.Identity.Name;
            string id = context.Users.Where(u => u.UserName == UserName).Select(u => u.Id).FirstOrDefault();
            int CityID = context.Technicans.Where(c => c.Id == id).Select(c => c.CityID).FirstOrDefault();
            City City = context.City.FirstOrDefault(c => c.Id == CityID);

            List<CoordinatorData> clientData = context.CoordinatorData.Where(c => c.City == City.Name && c.TechnicanID == id)
                .Include(c => c.CallCenterData).ThenInclude(c => c.PreRequst).ToList();
            List<TechnicanVM> technicanVMs = new List<TechnicanVM>();

            foreach (var item in clientData)
            {
                TechnicanData technicanData = context.TechnicanData.OrderBy(c => c.Id).LastOrDefault(c => c.ClineDataId == item.PreReqId && c.TechnicanId == id);
                if (item.TechnicanExist == true)
                {
                    if (technicanData == null)
                    {
                        technicanVMs.Add(new TechnicanVM
                        {
                            Id = item.PreReqId,
                            PaymentMethod = item.PaymentMethod,
                            IsPayed = item.IsPayed,
                            Notes = item.CallCenterData.Notes,
                            Date = item.AssignDate?.ToString("yyyy/MM/dd"),
                            City = item.City,
                            Destrict = item.CallCenterData.Destrict,
                            Time = context.Times.Where(t => t.Id == int.Parse(item.AssignTime)).Select(t => t.Time).FirstOrDefault(),
                            ClientName = item.CallCenterData.Name,
                            Service = item.CallCenterData.Service,
                        });
                    }
                    else
                    {
                        technicanVMs.Add(new TechnicanVM
                        {
                            Id = item.PreReqId,
                            PaymentMethod = item.PaymentMethod,
                            IsPayed = item.IsPayed,
                            Notes = item.CallCenterData.Notes,
                            Date = item.AssignDate?.ToString("yyyy/MM/dd"),
                            City = item.City,
                            Destrict = item.CallCenterData.Destrict,
                            Time = context.Times.Where(t => t.Id == int.Parse(item.AssignTime)).Select(t => t.Time).FirstOrDefault(),
                            ClientName = item.CallCenterData.Name,
                            Service = item.CallCenterData.Service,
                            StatusId = technicanData.StatusId,
                            location = technicanData.Location,
                        });
                    }

                }
            }
            return View(technicanVMs);
        }


        public async Task<IActionResult> TechnicanUpdatedData(TechnicanVM technican)
        {
            var UserName = User.Identity.Name;
            string id = context.Users.Where(u => u.UserName == UserName).Select(u => u.Id).FirstOrDefault();
            //ClientData clientData = new ClientData();
            TechnicanData technicanData = new TechnicanData();
            //CoordinatorData coordinatorData = new CoordinatorData();
            if (ModelState.IsValid)
            {
                SaveImage(technican);

                //clientData = context.ClientData.FirstOrDefault(c => c.Id == technican.Id);
                //coordinatorData = context.CoordinatorData.FirstOrDefault(c => c.PreReqId == technican.Id);




                technicanData.TechnicanId = id;
                technicanData.Date = DateTime.Now.ToShortDateString();
                technicanData.Time = DateTime.Now.ToShortTimeString();
                technicanData.Location = technican.location;
                technicanData.StatusId = technican.StatusId;
                technicanData.ClineDataId = technican.Id;

                technicanData.ImageStatus = technican.ImageStatus;

                context.TechnicanData.Add(technicanData);

                /*if (clientData.CarBeforService == null)
                {
                    clientData.CarBeforService = technican.CarBeforService;

                }
                if (clientData.CarAfterService == null)
                {

                    clientData.CarAfterService = technican.CarAfterService;
                }
                if (clientData.PaymentImage == null)
                {
                    clientData.PaymentImage = technican.paymentImg;

                }
                clientData.PaymentMethod = technican.PaymentMethod;
                if (clientData.BillAfterService == null)
                {

                    clientData.BillAfterService = technican.BillAfterService;
                }
            
               
*/

                if (technican.StatusId == 7)
                {
                    context.CoordinatorData.FirstOrDefault(c => c.PreReqId == technicanData.ClineDataId).TechnicanExist = false;
                    var q=context.PreRequst.Where(e => e.Id == technican.Id).FirstOrDefault();
                    q.IsFinshed = true;
                    context.SaveChanges();

                }
                context.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public async void SaveImage(TechnicanVM technican)
        {
            string wwwRootPath = _webHost.WebRootPath;
            /*var PayimentImg = context.ClientData.Where(i => i.Id == technican.Id ).Select(e => e.PaymentImage).FirstOrDefault();*/
            //paymentImg
            if (technican.ImageStatusFile != null)
            {

                string paymentImgFileName = Path.GetFileNameWithoutExtension(technican.ImageStatusFile.FileName);
                string paymentExtension = Path.GetExtension(technican.ImageStatusFile.FileName);
                technican.ImageStatus = paymentImgFileName + paymentExtension;
                string paymentPath = Path.Combine(wwwRootPath + "/UploadedImgs/" + (paymentImgFileName + paymentExtension));
                using (var fileStream = new FileStream(paymentPath, FileMode.Create))
                {
                    await technican.ImageStatusFile.CopyToAsync(fileStream);
                }
            }

            #region Images
            /* if (technican.CarBeforServiceFile != null)
                {

                    //CarBeforService
                    string CarBeforServiceFileName = Path.GetFileNameWithoutExtension(technican.CarBeforServiceFile.FileName);
                    string beforeExtension = Path.GetExtension(technican.CarBeforServiceFile.FileName);
                    technican.CarBeforService = CarBeforServiceFileName + beforeExtension;
                    string CarBeforServicepath = Path.Combine(wwwRootPath + "/UploadedImgs/" + (CarBeforServiceFileName + beforeExtension));

                    using (var fileStream = new FileStream(CarBeforServicepath, FileMode.Create))
                    {
                        await technican.CarBeforServiceFile.CopyToAsync(fileStream);
                    }

                }
                if (technican.CarAfterServiceFile != null)
                {

                    //CarAfterService
                    string CarAfterServiceFileName = Path.GetFileNameWithoutExtension(technican.CarAfterServiceFile.FileName);
                    string afterExtension = Path.GetExtension(technican.CarAfterServiceFile.FileName);
                    technican.CarAfterService = CarAfterServiceFileName + afterExtension;
                    string CarAfterServicepath = Path.Combine(wwwRootPath + "/UploadedImgs/" + (CarAfterServiceFileName + afterExtension));

                    using (var fileStream = new FileStream(CarAfterServicepath, FileMode.Create))
                    {
                        await technican.CarAfterServiceFile.CopyToAsync(fileStream);
                    }
                }
                if (technican.BillAfterServiceFile != null)
                {
                    //BillAfterService
                    string BillAfterServiceFileName = Path.GetFileNameWithoutExtension(technican.BillAfterServiceFile.FileName);
                    string BillExtension = Path.GetExtension(technican.BillAfterServiceFile.FileName);
                    technican.BillAfterService = BillAfterServiceFileName + BillExtension;
                    string Billpath = Path.Combine(wwwRootPath + "/UploadedImgs/" + (BillAfterServiceFileName + BillExtension));

                    using (var fileStream = new FileStream(Billpath, FileMode.Create))
                    {
                        await technican.BillAfterServiceFile.CopyToAsync(fileStream);
                    }
                }*/
            #endregion
        }

    }
}

