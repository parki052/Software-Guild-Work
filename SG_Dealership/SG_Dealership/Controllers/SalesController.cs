using BLL;
using Data.Repos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Models;
using Models.Identity;
using SG_Dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Controllers
{
    public class SalesController : Controller
    {
        public ActionResult Index()
        {
            var vm = new SearchVM();
            vm.SetLists();
            return View(vm);
        }

        public ActionResult Purchase(int Id)
        {
            var manager = ManagerFactory.Create();
            var vehicle = manager.GetVehicle(Id);
            if(manager.GetAllSales().Any(s => s.PurchasedVehicle.Id == Id))
            {
                return RedirectToAction("Index", "Home");
            }

            var vm = new PurchaseVM();
            vm.SetListItems(manager);
            vm.Vehicle = vehicle;

            return View(vm);
        }

        [HttpPost]
        
        public ActionResult Purchase(PurchaseVM vm)
        {
            var manager = ManagerFactory.Create();
            vm.Vehicle = manager.GetVehicle(vm.Vehicle.Id);

            if (vm.Email == null && vm.Phone == null)
            {
                ModelState.AddModelError("", "Either Email or Phone must be provided.");
            }

            decimal minPurchasePrice = vm.Vehicle.SalePrice * .95M;
            if (vm.PurchasePrice < minPurchasePrice)
            {
                ModelState.AddModelError("", $"Purchase price cannot be less than 95% of the sales price ({string.Format("{0:C}", minPurchasePrice)}).");
            }

            if (!ModelState.IsValid)
            {
                vm.SetListItems(manager);
                return View(vm);
            }
            else
            {
                if (vm.Vehicle.IsFeatured)
                {
                    manager.RemoveFromFeatured(vm.Vehicle);
                }

                var address = new Address
                {
                    City = vm.City,
                    CustState = manager.GetState(vm.SelectedStateId),
                    Street1 = vm.Street1,
                    ZipCode = vm.Zipcode
                };

                if (vm.Street2 != null)
                {
                    address.Street2 = vm.Street2;
                }

                var customer = new Customer();

                var existingCustomer = manager.GetAllCustomers()
                    .Where(c => c.Name == vm.Name &&
                    c.CustAddress.CustState.Id == address.CustState.Id &&
                    c.CustAddress.City == address.City &&
                    c.CustAddress.Street1 == address.Street1 &&
                    c.CustAddress.ZipCode == address.ZipCode).ToList().SingleOrDefault();

                if (existingCustomer != null)
                {
                    customer = existingCustomer;
                }
                else
                {
                    customer = new Customer
                    {
                        Name = vm.Name,
                        CustAddress = address,
                    };

                    if (vm.Email != null)
                    {
                        customer.Email = vm.Email;
                    }

                    if (vm.Phone != null)
                    {
                        customer.Phone = vm.Phone;
                    }
                }
                var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new EntityRepo()));
                var userId = userManager.FindByName(User.Identity.Name).Id;
                AppUser currentUser = userManager.FindById(userId);

                var sale = new Sale
                {
                    Employee = currentUser,
                    Buyer = customer,
                    Price = vm.PurchasePrice,
                    PurchasedVehicle = vm.Vehicle,
                    SaleDate = DateTime.Now,
                    SaleType = manager.GetPurchaseType(vm.SelectedPurchaseTypeId)                   

                };

                manager.AddSale(sale);
                return RedirectToAction("Index", "Sales");
            }
        }
    }
}