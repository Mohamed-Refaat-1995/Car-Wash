using CarService.Hubs;
using CarService.Models;
using CarService.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace NaqiWash.Controllers
{
    public class CitiesController : Controller
    {
        // GET: Cities
        CarServiceEntities context;
        IHubContext<AddEmployeeHub> hubContext;
        // GET: NewDasboard
        public int cityID { get; set; }
        public CitiesController(CarServiceEntities _Context, IHubContext<AddEmployeeHub> hubContext)
        {
            context = _Context;
            this.hubContext = hubContext;

        }
        public IActionResult Index(int id)
        {

            HttpContext.Session.SetInt32("cityID", id);
            return RedirectToAction("CallCenterIndx");

        }

        // Cities/CallCenterIndxCity City

        public IActionResult CallCenterIndx()
        {
            ViewBag.Cities = context.City.ToList();
            int id = (int)HttpContext.Session.GetInt32("cityID");
            List<CallCenter> callCenters = context.CallCenter.Where(c => c.City.Id == id && c.User.Pinding == "Pinding")
                                                                   .Include(u => u.User).Include(c => c.City).ToList();


            return View(callCenters);
        }


        public IActionResult CoordinatorIndex(int id)
        {

            ViewBag.Cities = context.City.ToList();
            List<Coordinator> coordinators = new List<Coordinator>();
            int? CityID = (int)HttpContext.Session.GetInt32("cityID");
            if (id == 0)
            {

                coordinators = context.Coordinator.Where(c => c.City.Id == CityID && c.User.Pinding == "Pinding")
                   .Include(u => u.User).Include(c => c.City).ToList();
            }
            if (id != 0)
            {

                coordinators = context.Coordinator.Where(c => c.City.Id == id && c.User.Pinding == "Pinding")
                     .Include(u => u.User).Include(c => c.City).ToList();
            }


            return View(coordinators);
        }




        public IActionResult TechnicalIndex(int id)
        {
            ViewBag.Cities = context.City.ToList();
            List<Technican> tech = new List<Technican>();
            int? CityID = (int)HttpContext.Session.GetInt32("cityID");


            if (id == 0)
            {

                tech = context.Technicans.Where(c => c.City.Id == CityID && c.User.Pinding == "Pinding")
                   .Include(u => u.User).Include(c => c.City).ToList();
            }
            if (id != 0)
            {

                tech = context.Technicans.Where(c => c.City.Id == id && c.User.Pinding == "Pinding")
                     .Include(u => u.User).Include(c => c.City).ToList();
            }

            return View(tech);

        }
        public ActionResult QualityIndex()
        {
            return View();
        }

        public IActionResult AcceptAndIgnore(string id, string Accept, string Reject)
        {

            if (Accept == "Accept")
            {
                context.Users.FirstOrDefault(i => i.Id == id).Pinding = "Confirm";
                context.SaveChanges();

            }

            if (Reject == "Reject")
            {
                context.Users.FirstOrDefault(i => i.Id == id).Pinding = "Reject";
                context.SaveChanges();

            }
            var roles = context.UserRoles.Where(i => i.UserId == id).Select(n => n.RoleId).FirstOrDefault();
            var role_name = context.Roles.Where(e => e.Id == roles).Select(r => r.Name).FirstOrDefault();

            if (role_name == "منسق")
                return RedirectToAction("CoordinatorIndex");

            if (role_name == "فني" || role_name == "فني")
                return RedirectToAction("TechnicalIndex");
            /*  if (role_name == "كول سنتر")*/
            return RedirectToAction("CallCenterIndx");

        }

    }
}