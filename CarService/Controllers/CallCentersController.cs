using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarService.Models;
using CarService.Repository;
using CarService.ViewModel;
using Microsoft.AspNetCore.SignalR;
using CarService.Hubs;
using System.Diagnostics.Metrics;
using CarService.Reposatories;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarService.Controllers
{

    [Authorize(Roles = "كول سنتر")]
    public class CallCentersController : Controller
    {
        CarServiceEntities context;
        IData data;
        IUserInfoRepsatories userInfoRepsatories;
        IHubContext<AddOrderHub> hubContext;
        List<PreRequst> preRequst = new List<PreRequst>();
        List<CallCenterDataVM> list = new List<CallCenterDataVM>();

        //constructor
        public CallCentersController(CarServiceEntities _context, IData _data, IHubContext<AddOrderHub> hubContext, IUserInfoRepsatories userInfoRepsatories)
        {
            context = _context;
            data = _data;
            this.hubContext = hubContext;
            this.userInfoRepsatories = userInfoRepsatories;
        }

        // callcenters/index
        public IActionResult Index()
        {
            List<PreRequst> clint = data.GetAll();
            ViewBag.UserName = context.Users.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();

            string CallcentrName = User.Identity.Name;
            string CallCenterID = context.Users.Where(n => n.UserName == CallcentrName).Select(e => e.Id).FirstOrDefault();
            ViewData["CallCenterId"] = CallCenterID;

            foreach (var c in clint.Where(x => x.CallCenterId == CallCenterID))
            {
                if (c.IsExist == true)
                {
                    list.Add(new CallCenterDataVM
                    {
                        Id = c.Id,
                        Date = c.Date,
                        Time = c.Time,
                        City = c.City.Name,
                        Destrict = c.Destrict,
                        Service = c.Services.Name,
                        PhoneNumber = c.PhoneNumber
                    });
                }
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult AddClient(CallCenterDataVM client)
        {
            CallCenterData clientData = new CallCenterData();
            if (client.OrderStatus == "غيرجاد")
            {
                //var ID = data.DeleteClientFromCallCenter(client.Id);
                // data.Delete(client.Id);
                clientData.CoordenaitorExist = false;
                var req = context.PreRequst.Where(i => i.Id == client.Id).FirstOrDefault();
                req.IsExist = false;
                context.SaveChanges();


            }
            else
            {
                if (ModelState.IsValid)
                {
                    context.PreRequst.FirstOrDefault(p => p.Id == client.Id).IsExist = false;
                    clientData.PreReqId = client.Id;
                    clientData.Time = DateTime.Now.ToString("HH:mm");
                    clientData.Date = DateTime.Now.ToShortDateString();
                    clientData.PhoneNumber = client.PhoneNumber;
                    clientData.OrderStatus = client.OrderStatus;
                    clientData.Name = client.Name;
                    clientData.ServiceDate = client.ServiceDate.Date;
                    clientData.Notes = client.Notes;
                    clientData.Price = client.Price;
                    clientData.Service = client.Service;
                    clientData.OrderStatus = client.OrderStatus;
                    clientData.Source = client.Source;
                    clientData.Destrict= client.Destrict;
                    clientData.CoordenaitorExist = true;
                    data.Insert(clientData);

                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



    }
}
