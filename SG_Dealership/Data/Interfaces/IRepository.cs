using Models;
using Models.Identity;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IRepository
    {
        Contact AddContactRequest(Contact toAdd);

        List<Contact> GetAllContactRequests();
        Contact GetContactRequest(int id);

        List<Customer> GetAllCustomers();
        Customer GetCustomer(int id);

        List<Address> GetAllAddresses();
        Address GetAddress(int id);

        List<PurchaseType> GetAllPurchaseTypes();
        PurchaseType GetPurchaseType(int id);

        List<State> GetAllStates();
        State GetState(int id);

        List<Sale> GetAllSales();
        Sale GetSale(int id);

        List<Color> GetAllColors();
        Color GetColor(int id);

        List<Condition> GetAllConditions();
        Condition GetCondition(int id);

        List<Style> GetAllBodyStyles();
        Style GetStyle(int id);

        List<Transmission> GetAllTransmissions();
        Transmission GetTransmission(int id);

        List<Model> GetAllModels();
        Model GetModel(int id);

        List<Make> GetAllMakes();
        Make GetMake(int id);

        List<Vehicle> GetAllVehicles();
        Vehicle GetVehicle(int id);

        List<AppUser> GetAllEmployees();
        AppUser GetUser(int id);

        Model AddModel(Model toAdd, string userId);
        Make AddMake(Make toAdd, string userId);
        Vehicle AddVehicle(Vehicle toAdd);
        Vehicle EditVehicle(Vehicle editedVehicle);
        Vehicle DeleteVehicle(int Id);
        Vehicle RemoveFromFeatured(Vehicle toRemoveFeatured);

        Sale AddSale(Sale toAdd);

        AppUser CreateUser(string userName, string password, string role);
        AppUser EditUser(string password, string role);
    }
}
