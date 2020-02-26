using BLL;
using Models;
using SG_Dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new HomeVM();
            vm.SetFeaturedVehicles(ManagerFactory.Create());
            return View(vm);
        }

        public ActionResult Specials()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact(int? id)
        {
            var vm = new ContactVM();
            if (id != null)
            {
                var mgr = ManagerFactory.Create();
                var vin = mgr.GetVehicle(id.Value).VIN;

                vm.EmbedVinToMessage(vin);
                return View(vm);
            }
            else
            {
                return View(vm);
            }
        }

        [HttpPost]
        public ActionResult Contact(ContactVM vm)
        {
            if (vm.Phone == null && vm.Email == null)
            {
                ModelState.AddModelError("", "Either phone or email must be provided.");
                return View(vm);
            }

            else
            {
                var contact = new Contact();
                contact.Name = vm.Name;
                if (vm.Phone != null)
                {
                    contact.Phone = vm.Phone;
                }
                if (vm.Email != null)
                {
                    contact.Email = vm.Email;
                }
                contact.Message = vm.Message;

                ManagerFactory.Create().AddContactRequest(contact);
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index");
        }
    }
}
