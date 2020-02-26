using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repos;
using SG_Dealership.Models;
using Microsoft.Owin.Security;

namespace SG_Dealership.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginVM();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginVM vm, string returnUrl)
        {

            if(!ModelState.IsValid)
            {

            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new EntityRepo()));
            AppUser user = userManager.Find(vm.Username, vm.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");

                return View(vm);
            }
            else
            {
                vm.Role = roleManager.FindById(userManager.FindByName(user.UserName).Roles.ToList().Single().RoleId).Name;
            }
            if (vm.Role == "Deactivated")
            {
                ModelState.AddModelError("", "There was an error logging in.");
                return View(vm);
            }

            var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(new AuthenticationProperties { IsPersistent = vm.RememberMe }, identity);

            if(!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }
    }
}