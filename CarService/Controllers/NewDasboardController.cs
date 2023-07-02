using CarService.Hubs;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace NaqiWash.Controllers
{
    public class NewDasboardController : Controller
    {

        CarServiceEntities context;
        IHubContext<AddEmployeeHub> hubContext;
        // GET: NewDasboard


        public NewDasboardController(CarServiceEntities _Context, IHubContext<AddEmployeeHub> hubContext)
        {
            context = _Context;
            this.hubContext = hubContext;

        }
        public IActionResult Index()
        {
            ViewBag.Cities = context.City.ToList();
                
            ViewBag.Technicans = context.Technicans
                .Where(p => p.User.Pinding == "Pinding")
                .Include(c => c.User)
                .Include(c => c.City)
                .ToList();

            ViewBag.CallCenter = context.CallCenter
                .Where(p => p.User.Pinding == "Pinding")
                .Include(c => c.City)
                .Include(c => c.User).
                ToList();

            ViewBag.Coordinator = context.Coordinator
                .Where(p => p.User.Pinding == "Pinding")
                .Include(c => c.City)
                .Include(c => c.User)
                .ToList();


            ViewBag.numberOfCompletedReq = context.TechnicanData.Where(s => s.StatusId == 7).Count();
            ViewBag.numberOfPreReq = context.PreRequst.Count();
            ViewBag.numOfCanceldPreReq = context.PreRequst.Count(c => c.IsExist == false);
            ViewBag.PendingClients = context.PreRequst.Count(s => s.IsFinshed == false);

            return View();
        }

       
       
        public IActionResult ViewCallCenters()
        {


            return View(context.CallCenter.Include(e => e.User).Include(c => c.City).ToList());


        }


        [HttpGet]
        public IActionResult AddCity()
        {
            ViewBag.Cities = context.City.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult AddCity(City c)
        {

            if (ModelState.IsValid)
            {
                context.City.Add(c);
                context.SaveChanges();
            }
            ViewBag.Cities = context.City.ToList();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddService()
        {
            ViewBag.Cities = context.City.ToList();
            return View();
        }


        [HttpPost]

        public IActionResult AddService(Services s)
        {

            if (ModelState.IsValid)
            {
                context.Services.Add(s);
                context.SaveChanges();
            }
            ViewBag.Cities = context.City.ToList();
            return RedirectToAction("Index");
        }





    }
}