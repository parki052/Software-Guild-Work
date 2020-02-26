using BLL;
using Data.Repos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Models.Identity;
using Models.VehicleDetails;
using SG_Dealership.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vehicles()
        {
            var manager = ManagerFactory.Create();
            var vm = new AdminVehiclesVM();
            vm.SetVehicles(manager);
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddVehicle()
        {
            var vm = new AddVehicleVM();
            vm.SetAllLists(ManagerFactory.Create());
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddVehicle(AddVehicleVM vm)
        {
            var manager = ManagerFactory.Create();

            if (vm.Year < 2000 || vm.Year > DateTime.Now.Year + 1)
            {
                ModelState.AddModelError("", "Error: Vehicle year must be between 2000 and " + (DateTime.Now.Year + 1) + ".");
            }

            if (manager.GetCondition(vm.SelectedConditionId).Name == "New" && vm.Mileage > 1000)
            {
                ModelState.AddModelError("", "Error: If condition is set to new, mileage must be less than 1,000.");
            }

            if (manager.GetCondition(vm.SelectedConditionId).Name == "Used" && vm.Mileage <= 1000)
            {
                ModelState.AddModelError("", "Error: If condition is set to used, mileage must be greater than 1,000.");
            }

            if (vm.VIN == "")
            {
                ModelState.AddModelError("", "Error: VIN cannot be blank.");
            }

            if (vm.MSRP <= 0)
            {
                ModelState.AddModelError("", "Error: MSRP must be greater than 0.");
            }

            if (vm.SalePrice <= 0)
            {
                ModelState.AddModelError("", "Error: sale price must be greater than 0.");
            }

            if (vm.SalePrice > vm.MSRP)
            {
                ModelState.AddModelError("", "Error: Sale price must be lower than MSRP.");
            }

            if (vm.Description == "")
            {
                ModelState.AddModelError("", "Error: A description is required.");
            }
            if (!ModelState.IsValid)
            {
                vm.SetAllLists(manager);
                return View(vm);
            }
            else
            {
                Vehicle toAdd = new Vehicle
                {
                    BodyStyle = manager.GetBodyStyle(vm.SelectedStyleId),
                    ConditionType = manager.GetCondition(vm.SelectedConditionId),
                    Description = vm.Description,
                    ExteriorColor = manager.GetColor(vm.SelectedColorId),
                    InteriorColor = manager.GetColor(vm.SelectedColorId),
                    Mileage = vm.Mileage,
                    ModelType = manager.GetModel(vm.SelectedModelId),
                    MSRP = vm.MSRP,
                    SalePrice = vm.SalePrice,
                    Trans = manager.GetTransmission(vm.SelectedTransId),
                    VIN = vm.VIN,
                    Year = vm.Year,
                    IsFeatured = false
                    //picture path

                };
                var saved = manager.AddVehicle(toAdd);
                string dir = Server.MapPath("~/Images");

                vm.Picture.SaveAs(Path.Combine(dir, "inventory-" + saved.Id.ToString() + ".jpg"));
                saved.PicturePath = "~/Images/" + "inventory-" + saved.Id.ToString() + ".jpg";
                manager.EditVehicle(saved);
                return RedirectToAction("Vehicles");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditVehicle(int Id)
        {
            var manager = ManagerFactory.Create();
            var selectedVehicle = manager.GetVehicle(Id);
            var vm = new EditVehicleVM();
            vm.SetSelectionsForEdit(selectedVehicle);
            vm.SetAllLists(manager);
            vm.VehicleId = selectedVehicle.Id;

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditVehicle(EditVehicleVM vm)
        {
            {
                var manager = ManagerFactory.Create();

                if (vm.Year < 2000 || vm.Year > DateTime.Now.Year + 1)
                {
                    ModelState.AddModelError("", "Error: Vehicle year must be between 2000 and " + (DateTime.Now.Year + 1) + ".");
                }

                if (manager.GetCondition(vm.SelectedConditionId).Name == "New" && vm.Mileage > 1000)
                {
                    ModelState.AddModelError("", "Error: If condition is set to new, mileage must be less than 1,000.");
                }

                if (manager.GetCondition(vm.SelectedConditionId).Name == "Used" && vm.Mileage <= 1000)
                {
                    ModelState.AddModelError("", "Error: If condition is set to used, mileage must be greater than 1,000.");
                }

                if (vm.VIN == "")
                {
                    ModelState.AddModelError("", "Error: VIN cannot be blank.");
                }

                if (vm.MSRP <= 0)
                {
                    ModelState.AddModelError("", "Error: MSRP must be greater than 0.");
                }

                if (vm.SalePrice <= 0)
                {
                    ModelState.AddModelError("", "Error: sale price must be greater than 0.");
                }

                if (vm.SalePrice > vm.MSRP)
                {
                    ModelState.AddModelError("", "Error: Sale price must be lower than MSRP.");
                }

                if (vm.Description == "")
                {
                    ModelState.AddModelError("", "Error: A description is required.");
                }

                if (!ModelState.IsValid)
                {
                    vm.SetAllLists(manager);
                    var vehicleToEdit = manager.GetVehicle(vm.VehicleId);
                    vm.SetSelectionsForEdit(vehicleToEdit);
                    return View(vm);
                }
                else
                {


                    Vehicle toEdit = new Vehicle
                    {
                        Id = vm.VehicleId,
                        BodyStyle = manager.GetBodyStyle(vm.SelectedStyleId),
                        ConditionType = manager.GetCondition(vm.SelectedConditionId),
                        Description = vm.Description,
                        ExteriorColor = manager.GetColor(vm.SelectedColorId),
                        InteriorColor = manager.GetColor(vm.SelectedColorId),
                        Mileage = vm.Mileage,
                        ModelType = manager.GetModel(vm.SelectedModelId),
                        MSRP = vm.MSRP,
                        SalePrice = vm.SalePrice,
                        Trans = manager.GetTransmission(vm.SelectedTransId),
                        VIN = vm.VIN,
                        Year = vm.Year,
                        IsFeatured = vm.IsFeatured,
                        PicturePath = vm.PicturePath
                        

                    };


                    if (vm.Picture != null)
                    {
                        string dir = Server.MapPath("~/Images");

                        vm.Picture.SaveAs(Path.Combine(dir, "inventory-" + toEdit.Id.ToString() + ".jpg"));
                        toEdit.PicturePath = "~/Images/" + "inventory-" + toEdit.Id.ToString() + ".jpg";
                    }
                    manager.EditVehicle(toEdit);
                    return RedirectToAction("Vehicles");
                }
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new EntityRepo()));
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new EntityRepo()));
            var vm = new UsersVM();
            vm.SetUsers(userManager, roleManager);
            return View(vm);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddUser()
        {
            var vm = new AddUserVM();
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new EntityRepo()));
            vm.SetRoles(roleManager);
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddUser(AddUserVM vm)
        {
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new EntityRepo()));
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(new EntityRepo()));
            if (vm.Password != vm.ConfirmPassword)
            {
                ModelState.AddModelError("", "The password and confirm password fields do not match.");
            }


            if(!ModelState.IsValid)
            {

                vm.SetRoles(roleManager);
                return View(vm);
            }
            else
            {
                var selectedRole = roleManager.FindById(vm.SelectedRoleId).Name;
                var user = new AppUser
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Email = vm.Email,
                    UserName = vm.Email                                      
                };
                userManager.Create(user, vm.Password);
                userManager.AddToRole(user.Id, selectedRole);
                return RedirectToAction("Users");
            }
        }


        [Authorize(Roles = "Admin")]
        public ActionResult EditUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Makes()
        {
            var vm = new MakeModelVM();
            var manager = ManagerFactory.Create();
            vm.SetMakesAndModels(manager);

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            vm.UserId = userManager.FindByName(User.Identity.Name).Id;

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Makes(MakeModelVM vm)
        {
            var manager = ManagerFactory.Create();
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();

            vm.SetMakesAndModels(manager);

            if (vm.AllMakes.Any(m => m.Name == vm.SubmittedMake))
            {
                ModelState.AddModelError("", "Error: That make is already in the database.");
            }

            if (vm.SubmittedMake == null)
            {
                ModelState.AddModelError("", "Error: the Make field is required.");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                manager.AddMake(new Make
                {

                    Name = vm.SubmittedMake,
                    DateAdded = DateTime.Now,
                    UserAddedBy = userManager.FindById(vm.UserId)

                }, vm.UserId);
                return RedirectToAction("Makes");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Models()
        {
            var vm = new MakeModelVM();
            var manager = ManagerFactory.Create();
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            vm.UserId = userManager.FindByName(User.Identity.Name).Id;

            vm.SetMakesAndModels(manager);
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Models(MakeModelVM vm)
        {
            var manager = ManagerFactory.Create();
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();

            vm.SetMakesAndModels(manager);

            if (vm.AllModels.Any(m => m.Name == vm.SubmittedModel))
            {
                ModelState.AddModelError("", "Error: This model already exists for the given make.");
            }

            if (vm.SubmittedModel == null)
            {
                ModelState.AddModelError("", "Error: the model field may not be blank.");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                manager.AddModel(new Model
                {
                    Name = vm.SubmittedModel,
                    Maker = manager.GetMake(vm.SubmittedMakeId)
                }, vm.UserId);
                return RedirectToAction("Models");
            }
        }

        public ActionResult DeleteVehicle(int Id)
        {
            var manager = ManagerFactory.Create();
            manager.DeleteVehicle(Id);
            return RedirectToAction("Vehicles", "Admin");
        }

    }
}