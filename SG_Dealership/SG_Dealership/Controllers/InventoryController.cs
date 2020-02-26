using BLL;
using SG_Dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Controllers
{
    public class InventoryController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            var vm = new SearchVM();
            vm.SetLists();
            return View(vm);
        }

        public ActionResult Used()
        {
            var vm = new SearchVM();
            vm.SetLists();
            return View(vm);
        }

        public ActionResult Details(int Id)
        {
            var vm = new DetailsVM();
            vm.SetVehicle(ManagerFactory.Create(), Id);
            return View(vm);
        }
    }
}