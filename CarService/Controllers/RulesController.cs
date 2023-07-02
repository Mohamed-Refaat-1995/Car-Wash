using CarService.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    public class RulesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RulesController(RoleManager<IdentityRole> RoleManager)
        {
            roleManager = RoleManager;
        }



        // Rules/New
   
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = roleVM.RoleName;
                IdentityResult result = await roleManager.CreateAsync(roleModel);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
                }

            }
            return View(roleVM);
        }

    }
}
