using CarService.Hubs;
using CarService.Models;
using CarService.Reposatories;
using CarService.Repository;
using CarService.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CarService.Controllers
{
    public class UserInfoController : Controller
    {
        CarServiceEntities context = new CarServiceEntities();
        IUserInfoRepsatories userInfoRepsatories;

        IHubContext<AddOrderHub> hubContext;
        ICityRepository cityRepository;
        public UserInfoController(IUserInfoRepsatories _userInfoRepsatories, ICityRepository _city,
             IHubContext<AddOrderHub> hubContext
            )
        {


            userInfoRepsatories = _userInfoRepsatories;
            cityRepository = _city;
            this.hubContext = hubContext;
        }


        // / UserInfo/FormInfo
        [HttpGet]
        public IActionResult FormInfo()
        {
            ViewData["Cities"] = cityRepository.GetAllCities();
            ViewData["Services"] = context.Services.ToList();
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> FormInfo(PreRequestVM preRequst)
        {
            PreRequst PreUserInfo = new PreRequst();
            ViewData["Cities"] = cityRepository.GetAllCities();

            if (ModelState.IsValid)
            {
                PreUserInfo.Date = DateTime.Now.ToShortDateString();//DateTime.Now.Value().ToShortDateString();

                PreUserInfo.Time = DateTime.Now.ToString("HH:mm");

                PreUserInfo.Destrict = preRequst.Destrict;
                PreUserInfo.ServiceId = preRequst.ServiceId;
                //PreUserInfo.Service = preRequst.Service;
                PreUserInfo.CityId = preRequst.CityId;
                PreUserInfo.PhoneNumber = preRequst.PhoneNumber;
                PreUserInfo.IsExist = true;
                PreUserInfo.IsFinshed = false;

                var cityObj = context.City.Where(c => c.Id == PreUserInfo.CityId).FirstOrDefault();


                List<CallCenter> CallCenters = context.CallCenter.Include(c => c.User).Where(c => c.User.Pinding == "Confirm").ToList();
                if (CallCenters.Count == 0)
                {
                    ModelState.AddModelError("", "لا يوجد اشخاص للخدمه الا حاول في وقت لاحق");
                    ViewData["Cities"] = cityRepository.GetAllCities();
                    ViewData["Services"] = context.Services.ToList();

                    return View(preRequst);
                }
                int preRequsts = context.PreRequst.Count();

                int NextAssign = GetNextCallCenter(CallCenters, preRequsts);
                string Id = CallCenters[NextAssign].User.Id;
                PreUserInfo.CallCenterId = Id;
                userInfoRepsatories.Insert(PreUserInfo);

                hubContext.Clients.All.SendAsync("NewOrderAdded", PreUserInfo, cityObj);


                return RedirectToAction("FormInfo");
            }

            /*   return View(preRequst);*/
            return RedirectToAction("FormInfo", preRequst);
        }

        private int GetNextCallCenter(List<CallCenter> callCenters, int preRequsts)
        {
            int NumCalCenter = callCenters.Count();
            int NumPreRequest = preRequsts + 1;
            int NextIndex = NumPreRequest % NumCalCenter;
            return NextIndex;
        }
    }
}

