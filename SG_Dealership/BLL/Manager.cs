using Data.Interfaces;
using Models;
using Models.Identity;
using Models.VehicleDetails;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class Manager
    {
        private IRepository _repo;

        public Manager(IRepository repo)
        {
            _repo = repo;
        }

        public Contact AddContactRequest(Contact toAdd)
        {
            _repo.AddContactRequest(toAdd);
            return toAdd;
        }

        public List<Contact> GetAllContactRequests() => _repo.GetAllContactRequests();

        public List<Customer> GetAllCustomers() => _repo.GetAllCustomers();

        public List<Address> GetAllAddresses() => _repo.GetAllAddresses();

        public List<PurchaseType> GetAllPurchaseTypes() => _repo.GetAllPurchaseTypes();

        public List<State> GetAllStates() => _repo.GetAllStates();

        public List<Sale> GetAllSales() => _repo.GetAllSales();

        public List<Color> GetAllColors() => _repo.GetAllColors();

        public List<Condition> GetAllConditions() => _repo.GetAllConditions();

        public List<Style> GetAllBodyStyles() => _repo.GetAllBodyStyles();

        public State GetState(int id) => _repo.GetState(id);

        public Customer GetCustomer(int id) => _repo.GetCustomer(id);

        public PurchaseType GetPurchaseType(int id) => _repo.GetPurchaseType(id);

        public Style GetBodyStyle(int id) => _repo.GetStyle(id);

        public Condition GetCondition(int id) => _repo.GetCondition(id);

        public Color GetColor(int id) => _repo.GetColor(id);

        public Model GetModel(int id) => _repo.GetModel(id);

        public Transmission GetTransmission(int id) => _repo.GetTransmission(id);

        public List<Transmission> GetAllTransmissions() => _repo.GetAllTransmissions();

        public List<Model> GetAllModels() => _repo.GetAllModels();

        public List<Model> GetModelsByMake(int Id) => _repo.GetAllModels().Where(m => m.Maker.Id == Id).ToList();

        public List<Make> GetAllMakes() => _repo.GetAllMakes();

        public Make GetMake(int id) => _repo.GetMake(id);

        public List<Vehicle> GetAllVehicles() => _repo.GetAllVehicles();

        public Vehicle GetVehicle(int id) => _repo.GetVehicle(id);

        public Vehicle RemoveFromFeatured(Vehicle vehicle) => _repo.RemoveFromFeatured(vehicle);

        public List<Vehicle> GetVehiclesForSale()
        {
            var toReturn = GetAllVehicles();
            foreach(var sale in GetAllSales())
            {
                if(toReturn.Contains(sale.PurchasedVehicle))
                {
                    toReturn.Remove(sale.PurchasedVehicle);
                }
            }
            return toReturn;
        }

        public List<AppUser> GetAllEmployees() => _repo.GetAllEmployees();

        public Model AddModel(Model toAdd, string userId)
        {
            _repo.AddModel(toAdd, userId);
            return toAdd;
        }

        public Make AddMake(Make toAdd, string userId)
        {
            _repo.AddMake(toAdd, userId);
            return toAdd;
        }

        public Vehicle AddVehicle(Vehicle toAdd)
        {
            _repo.AddVehicle(toAdd);
            return toAdd;
        }

        public Vehicle EditVehicle(Vehicle editedVehicle)
        {
            var edited = _repo.EditVehicle(editedVehicle);
            return edited;
        }

        public Vehicle DeleteVehicle(int Id)
        {
            return _repo.DeleteVehicle(Id);
        }

        public Sale AddSale(Sale toAdd)
        {
            _repo.AddSale(toAdd);
            return toAdd;
        }

        public AppUser CreateUser(string userName, string password, string role)
        {
            var user = _repo.CreateUser(userName, password, role);
            return user;
        }

        public AppUser EditUser(string password, string role)
        {
            var edited = _repo.EditUser(password, role);
            return edited;
        }
    }
}
