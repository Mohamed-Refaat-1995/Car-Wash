using CarService.Hubs;
using CarService.Models;
using CarService.Repository;
using CarService.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarService.Controllers
{
    public class AccountController : Controller
    {
        CarServiceEntities Context = new CarServiceEntities();
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        IRulesRepository RolesRepository;
        ICityRepository cityRepository;
        IHubContext<AddEmployeeHub> hubContext;
        public AccountController
         (UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> signInManager,
            IRulesRepository _Roles,
            ICityRepository _city,
             IHubContext<AddEmployeeHub> hubContext
            )
        {
            this.userManager = _userManager;
            this.signInManager = signInManager;
            RolesRepository = _Roles;
            cityRepository = _city;
            this.hubContext = hubContext;
        }


        [HttpGet]
        public IActionResult TechnicanRegistration()
        {
            return View();
        }


        // /Account/TechnicanRegistration
        [HttpGet]
        public IActionResult TechnicanUsers()
        {
            ViewBag.Rules = RolesRepository.GetAllRulesInfo();
            ViewData["Cities"] = cityRepository.GetAllCities();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TechnicanUsers(RegistrationViewModel UserviewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = UserviewModel.FirstName;
                userModel.LastName = UserviewModel.LastName;
                userModel.PasswordHash = UserviewModel.Password;
                userModel.Address = UserviewModel.Address;
                userModel.UserName = UserviewModel.UserName;
                userModel.Pinding = "Pinding";
                IdentityResult result =
                  await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "فني");
                    Technican newtechnican = new Technican();
                    newtechnican.Id = userModel.Id;
                    newtechnican.CityID = UserviewModel.CityID;

                    Context.Technicans.Add(newtechnican);
                    Context.SaveChanges();

                    await signInManager.SignInAsync(userModel, false);

                    hubContext.Clients.All.SendAsync("NewEmployeeAdded");

                    return RedirectToAction("sytemIndex" ,"Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            ViewBag.Rules = RolesRepository.GetAllRulesInfo();
            ViewData["Cities"] = cityRepository.GetAllCities();
            return View(UserviewModel);
        }

        [HttpGet]
        public IActionResult CoordinatorRegistration()
        {
            return View();
        }

        // /account/CoordinatorRegistration

        [HttpGet]
        public IActionResult CoordinatorUsers()
        {
            ViewBag.Rules = RolesRepository.GetAllRulesInfo();
            ViewData["Cities"] = cityRepository.GetAllCities();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CoordinatorUsers(RegistrationViewModel UserviewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = UserviewModel.FirstName;
                userModel.LastName = UserviewModel.LastName;
                userModel.PasswordHash = UserviewModel.Password;
                userModel.Address = UserviewModel.Address;
                userModel.UserName = UserviewModel.UserName;
                userModel.Pinding = "Pinding";

                IdentityResult result =
                  await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "منسق");
                    Coordinator newCordinator = new Coordinator();
                    newCordinator.Id = userModel.Id;
                    newCordinator.CityID = UserviewModel.CityID;
               

                    Context.Coordinator.Add(newCordinator);
                    Context.SaveChanges();

                    await signInManager.SignInAsync(userModel, false);

                    hubContext.Clients.All.SendAsync("NewEmployeeAdded");

                    return RedirectToAction("sytemIndex", "Home");
                    // return RedirectToAction("CoordenatorLogin");//need to change
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            ViewBag.Rules = RolesRepository.GetAllRulesInfo();
            ViewData["Cities"] = cityRepository.GetAllCities();
            return View(UserviewModel);
        }

        [HttpGet]
        public IActionResult CallCenterRegistration()
        {

            return View();
        }
        // /account/CallCenterRegistration
        [HttpGet]
        public IActionResult CallCenterUsers()
        {
            ViewBag.Rules = RolesRepository.GetAllRulesInfo();
            ViewData["Cities"] = cityRepository.GetAllCities();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CallCenterUsers(RegistrationViewModel UserviewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = UserviewModel.FirstName;
                userModel.LastName = UserviewModel.LastName;
                userModel.PasswordHash = UserviewModel.Password;
                userModel.Address = UserviewModel.Address;
                userModel.UserName = UserviewModel.UserName;
                userModel.Pinding = "Pinding";

                IdentityResult result =
                  await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "كول سنتر");
                    CallCenter newcallcenter = new CallCenter();
                    newcallcenter.Id = userModel.Id;
                    newcallcenter.CityID = UserviewModel.CityID;



                    Context.CallCenter.Add(newcallcenter);
                    Context.SaveChanges();


                    await signInManager.SignInAsync(userModel, false);

                    hubContext.Clients.All.SendAsync("NewEmployeeAdded");

                    return RedirectToAction("sytemIndex", "Home");
                    //return RedirectToAction("CallCenterLogin");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            ViewBag.Rules = RolesRepository.GetAllRulesInfo();
            ViewData["Cities"] = cityRepository.GetAllCities();
            return View(UserviewModel);
        }


       

        [HttpGet]
        public IActionResult CallCenterLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CallCenterLogin(LoginViewModel UserFromReq)//username,password,remeberme 
        {
            if (ModelState.IsValid)
            {
                //check valid  account "found in db"

                ApplicationUser userModel =
                    await userManager.FindByNameAsync(UserFromReq.Username);
                if (userModel != null)
                {
                    //cookie
                    Microsoft.AspNetCore.Identity.SignInResult rr =
                        await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                    //check cookie
                    if (rr.Succeeded && await userManager.IsInRoleAsync(userModel, "كول سنتر") && userModel.Pinding == "Confirm")

                        return RedirectToAction("index", "CallCenters");

                    if (rr.Succeeded && await userManager.IsInRoleAsync(userModel, "كول سنتر") && userModel.Pinding == "Pinding")

                    {
                        ModelState.AddModelError("", " Your Account Still Pending !!");

                        return View(UserFromReq);
                    }
                    if (rr.Succeeded && await userManager.IsInRoleAsync(userModel, "كول سنتر") && userModel.Pinding == "Rejected")

                    {
                        ModelState.AddModelError("", " Your Account Rejected !!");

                        return View(UserFromReq);
                    }
                }
             
            }
            ModelState.AddModelError("", " Wrong Data Try Again !!");
            return View(UserFromReq);
        }




        [HttpGet]
        public IActionResult CoordenatorLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CoordenatorLogin(LoginViewModel UserFromReq)//username,password,remeberme 
        {
            if (ModelState.IsValid)
            {
                //check valid  account "found in db"

                ApplicationUser userModel =
                    await userManager.FindByNameAsync(UserFromReq.Username);

                if (userModel != null)
                {
                    //cookie
                    Microsoft.AspNetCore.Identity.SignInResult rr =
                        await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                    //check cookie
                    if (rr.Succeeded && await userManager.IsInRoleAsync(userModel, "منسق") && userModel.Pinding == "Confirm")
                    return RedirectToAction("index", "Coordenator");

                    if (rr.Succeeded && await userManager.IsInRoleAsync(userModel, "منسق") && userModel.Pinding == "Pinding")

                    {
                        ModelState.AddModelError("", " Your Account Still Pending !!");

                        return View(UserFromReq);
                    }
                    if (rr.Succeeded && await userManager.IsInRoleAsync(userModel, "منسق") && userModel.Pinding == "Rejected")

                    {
                        ModelState.AddModelError("", " Your Account Rejected !!");

                        return View(UserFromReq);
                    }

                }
               
                
            }
           ModelState.AddModelError("", " Wrong Data Try Again !!");
            return View(UserFromReq);
        }




        [HttpGet]
        public IActionResult TechnicanLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TechnicanLogin(LoginViewModel UserFromReq)//username,password,remeberme 
        {
            if (ModelState.IsValid)
            {
                //check valid  account "found in db"

                ApplicationUser userModel =
                    await userManager.FindByNameAsync(UserFromReq.Username);
                if (userModel != null)
                {
                    //cookie
                    Microsoft.AspNetCore.Identity.SignInResult rr =
                        await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                    //check cookie
                    if (rr.Succeeded && (await userManager.IsInRoleAsync(userModel, "فني") || await userManager.IsInRoleAsync(userModel, "فنى")))

                        return RedirectToAction("index", "Technican");


                    if (rr.Succeeded && (await userManager.IsInRoleAsync(userModel, "فني") || await userManager.IsInRoleAsync(userModel, "فنى")) && userModel.Pinding == "Pinding")

                    {
                        ModelState.AddModelError("", " Your Account Still Pending !!");

                        return View(UserFromReq);
                    }
                    if (rr.Succeeded && (await userManager.IsInRoleAsync(userModel, "فني") || await userManager.IsInRoleAsync(userModel, "فنى")) && userModel.Pinding == "Rejected")

                    {
                        ModelState.AddModelError("", " Your Account Rejected !!");

                        return View(UserFromReq);
                    }

                }
               
                
            }
           ModelState.AddModelError("", " Wrong Data Try Again !!");
            return View(UserFromReq);
        }




        /*Admin*/

        [HttpGet]
        public IActionResult AdminRegister()
        {
          
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRegister(AdminRegisterionVM UserviewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = UserviewModel.FirstName;
                userModel.LastName = UserviewModel.LastName;
                userModel.PasswordHash = UserviewModel.Password;
   
                userModel.UserName = UserviewModel.UserName;
           
                IdentityResult result =
                  await userManager.CreateAsync(userModel, UserviewModel.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "أدمن");
                   

                    await signInManager.SignInAsync(userModel, false);


                    return RedirectToAction("AdminLogin", "Account");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
          
            return View(UserviewModel);
        }




        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminLogin(LoginViewModel UserFromReq)//username,password,remeberme 
        {
            if (ModelState.IsValid)
            {
                

                ApplicationUser userModel =
                    await userManager.FindByNameAsync(UserFromReq.Username);
                if (userModel != null)
                {
                    //cookie
                    Microsoft.AspNetCore.Identity.SignInResult rr =
                        await signInManager.PasswordSignInAsync(userModel, UserFromReq.Password, UserFromReq.rememberMe, false);
                    //check cookie
                    
                    if (rr.Succeeded && (await userManager.IsInRoleAsync(userModel, "أدمن")))
                    {


                        return RedirectToAction("Index", "NewDasboard");
                    }

                }


            }
            ModelState.AddModelError("", " Wrong Data Try Again !!");
            return View(UserFromReq);
        }














        public async Task<IActionResult> signOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("sytemIndex", "Home");
        }









    }
}