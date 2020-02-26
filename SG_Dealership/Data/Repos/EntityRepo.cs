using Data.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using Models.Identity;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Data.Repos
{
    public class EntityRepo : IdentityDbContext<AppUser>, IRepository
    {
        public EntityRepo() : base("SG_Dealership") { }

        public DbSet<Contact> ContactRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PurchaseType> PurchaseTypes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Sale> Sales { get; set; }

        //VehicleDetails sets
        public DbSet<Color> Colors { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public Contact AddContactRequest(Contact toAdd)
        {
            ContactRequests.Add(toAdd);
            SaveChanges();
            return toAdd;
        }

        public Make AddMake(Make toAdd, string userId)
        {
            var user = Users.Single(u => u.Id == userId);
            toAdd.UserAddedBy = user;
            Makes.Add(toAdd);
            SaveChanges();
            return toAdd;
        }

        public Model AddModel(Model toAdd, string userId)
        {
            var user = Users.Single(u => u.Id == userId);
            toAdd.UserAddedBy = user;
            toAdd.DateAdded = DateTime.Now;
            Models.Add(toAdd);
            SaveChanges();
            return toAdd;
        }

        public Sale AddSale(Sale toAdd)
        {
            toAdd.Employee = Users.SingleOrDefault(u => u.Id == toAdd.Employee.Id);
            Sales.Add(toAdd);
            SaveChanges();
            return toAdd;
        }

        public Vehicle AddVehicle(Vehicle toAdd)
        {
            Vehicles.Add(toAdd);
            SaveChanges();
            return toAdd;
        }

        public AppUser CreateUser(string userName, string password, string role)
        {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(this));
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(this));

            if (userManager.FindByName(userName) == null)
            {
                var user = new AppUser() { UserName = userName };
                var result = userManager.Create(user, password);

                userManager.AddToRole(user.Id, role);

                return user;
            }
            else
            {
                return null;
            }
        }

        public Vehicle DeleteVehicle(int Id)
        {
            var toRemove = Vehicles.Single(v => v.Id == Id);
            Vehicles.Remove(toRemove);
            SaveChanges();
            return toRemove;
        }

        public AppUser EditUser(string password, string role)
        {
            throw new NotImplementedException();
        }

        public Vehicle EditVehicle(Vehicle editedVehicle)
        {
            var toEdit = Vehicles.Single(v => v.Id == editedVehicle.Id);

            toEdit.IsFeatured = editedVehicle.IsFeatured;
            toEdit.BodyStyle = editedVehicle.BodyStyle;
            toEdit.ConditionType = editedVehicle.ConditionType;
            toEdit.Description = editedVehicle.Description;
            toEdit.ExteriorColor = editedVehicle.ExteriorColor;
            toEdit.InteriorColor = editedVehicle.InteriorColor;
            toEdit.Mileage = editedVehicle.Mileage;
            toEdit.ModelType = editedVehicle.ModelType;
            toEdit.MSRP = editedVehicle.MSRP;
            toEdit.PicturePath = editedVehicle.PicturePath;
            toEdit.SalePrice = editedVehicle.SalePrice;
            toEdit.Trans = editedVehicle.Trans;
            toEdit.VIN = editedVehicle.VIN;
            toEdit.Year = editedVehicle.Year;

            SaveChanges();
            return toEdit;
        }

        public Address GetAddress(int id) => Addresses.Single(a => a.Id == id);

        public List<Address> GetAllAddresses() => Addresses.ToList();

        public List<Style> GetAllBodyStyles() => Styles.ToList();

        public List<Color> GetAllColors() => Colors.ToList();

        public List<Condition> GetAllConditions() => Conditions.ToList();

        public List<Contact> GetAllContactRequests() => ContactRequests.ToList();

        public List<Customer> GetAllCustomers() => Customers.ToList();

        public List<AppUser> GetAllEmployees() => Users.ToList();

        public List<Make> GetAllMakes() => Makes.ToList();

        public List<Model> GetAllModels() => Models.ToList();

        public List<PurchaseType> GetAllPurchaseTypes() => PurchaseTypes.ToList();

        public List<Sale> GetAllSales() => Sales.ToList();

        public List<State> GetAllStates() => States.ToList();

        public List<Transmission> GetAllTransmissions() => Transmissions.ToList();

        public List<Vehicle> GetAllVehicles() => Vehicles.ToList();

        public Color GetColor(int id) => Colors.Single(c => c.Id == id);

        public Condition GetCondition(int id) => Conditions.Single(c => c.Id == id);

        public Contact GetContactRequest(int id) => ContactRequests.Single(c => c.Id == id);

        public Customer GetCustomer(int id) => Customers.Single(c => c.Id == id);

        public Make GetMake(int id) => Makes.ToList().Single(m => m.Id == id);

        public Model GetModel(int id) => Models.Single(m => m.Id == id);

        public PurchaseType GetPurchaseType(int id) => PurchaseTypes.Single(p => p.Id == id);

        public Sale GetSale(int id) => Sales.Single(s => s.Id == id);

        public State GetState(int id) => States.Single(s => s.Id == id);

        public Style GetStyle(int id) => Styles.Single(s => s.Id == id);

        public Transmission GetTransmission(int id) => Transmissions.Single(t => t.Id == id);

        public AppUser GetUser(int id) => Users.Single(u => u.Id == id.ToString());

        public Vehicle GetVehicle(int id) => Vehicles.Single(v => v.Id == id);

        public Vehicle RemoveFromFeatured(Vehicle toRemoveFeatured)
        {
            var edited = Vehicles.Single(v => v.Id == toRemoveFeatured.Id);
            edited.IsFeatured = false;
            SaveChanges();
            return edited;
        }
    }
}
