using Data.Interfaces;
using Models;
using Models.Identity;
using Models.VehicleDetails;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Data.Repos
{
    public class InMemRepo : IRepository
    {
        private static List<AppUser> _employees;
        private static List<Contact> _contactRequests;

        //customer details
        private static List<Customer> _customers;
        private static List<Address> _addresses;
        private static List<PurchaseType> _purchaseTypes;
        private static List<State> _states;
        private static List<Sale> _sales;

        //VehicleDetails sets
        private static List<Color> _colors;
        private static List<Condition> _conditions;
        private static List<Make> _makes;
        private static List<Model> _models;
        private static List<Style> _styles;
        private static List<Transmission> _transmissions;
        private static List<Vehicle> _vehicles;

        //instantiate mock data
        static InMemRepo()
        {
            _purchaseTypes = new List<PurchaseType>
            {
                new PurchaseType()
                {
                    Id = 1,
                    Name = "Bank Finance"
                },
                new PurchaseType()
                {
                    Id = 2,
                    Name = "Cash"
                },
                new PurchaseType
                {
                    Id = 3,
                    Name = "Dealer Finance"
                }
            };

            _colors = new List<Color>
            {
                new Color()
                {
                    Id = 1,
                    Name = "Black"
                },
                new Color
                {
                    Id = 2,
                    Name = "White"
                },
                new Color()
                {
                    Id = 3,
                    Name = "Grey"
                },
                new Color()
                {
                    Id = 4,
                    Name = "Red"
                },
                new Color()
                {
                    Id = 5,
                    Name = "Blue"
                }
            };

            _conditions = new List<Condition>
            {
                new Condition()
                {
                    Id = 1,
                    Name = "New"
                },
                new Condition()
                {
                    Id = 2,
                    Name = "Used"
                }
            };

            _makes = new List<Make>
            {
                new Make()
                {
                    Id = 1,
                    Name = "Jaguar",
                    Models = new List<Model>()                   
                    //set the list of models after models have been instantiated                   
                },
                new Make()
                {
                    Id = 2,
                    Name = "Toyota",
                    Models = new List<Model>()
                }
            };

            _models = new List<Model>
            {
                new Model()
                {
                    Id = 1,
                    Name = "Roadster",
                    Maker = _makes.Single(m => m.Name == "Jaguar")
                },
                new Model()
                {
                    Id = 2,
                    Name = "Corolla",
                    Maker = _makes.Single(m => m.Name == "Toyota")
                },
                new Model()
                {
                    Id = 3,
                    Name = "Camry",
                    Maker = _makes.Single(m => m.Name == "Toyota")
                }

            };

            //after models are created, go through each model and add it to the maker's list of models
            foreach (var model in _models)
            {
                _makes.Single(m => m.Name == model.Maker.Name).Models.Add(model);

            }

            _styles = new List<Style>
            {
                new Style()
                {
                    Id = 1,
                    Name = "Car"
                },
                new Style()
                {
                    Id = 2,
                    Name = "SUV"
                },
                new Style()
                {
                    Id = 3,
                    Name = "Truck"
                },
                new Style()
                {
                    Id = 4,
                    Name = "Van"
                }
            };

            _transmissions = new List<Transmission>
            {
                new Transmission()
                {
                    Id = 1,
                    Name = "Automatic"
                },
                new Transmission()
                {
                    Id = 2,
                    Name = "Manual"
                }
            };

            _vehicles = new List<Vehicle>
            {
                new Vehicle()
                {
                    Id = 1,
                    Description = "This is a really great test car.",
                    Mileage = 0,
                    MSRP = 20000m,
                    SalePrice = 19000m,
                    VIN = "ABCDE12345",
                    Year = 1956,
                    BodyStyle = _styles.Single(s => s.Name == "Car"),
                    ConditionType = _conditions.Single(c => c.Name == "New"),
                    InteriorColor = _colors.Single(c => c.Name == "Black"),
                    ExteriorColor = _colors.Single(c => c.Name == "Black"),
                    ModelType = _models.Single(c => c.Name == "Roadster"),
                    Trans = _transmissions.Single(c => c.Name == "Automatic")
                    //set string picturepath                 
                },

                new Vehicle()
                {
                    Id = 2,
                    Description = "My grandma drives one of these.",
                    Mileage = 50,
                    SalePrice = 12000m,
                    MSRP = 12500m,
                    VIN = "ABCDE12346",
                    Year = 2010,
                    BodyStyle = _styles.Single(s => s.Name == "Car"),
                    ConditionType = _conditions.Single(s => s.Name == "New"),
                    InteriorColor = _colors.Single(s => s.Name == "White"),
                    ExteriorColor = _colors.Single(s => s.Name == "White"),
                    ModelType = _models.Single(m => m.Name == "Corolla"),
                    Trans = _transmissions.Single(t => t.Name == "Automatic")
                    //set string picturepath
                },

                new Vehicle()
                {
                    Id = 3,
                    Description = "That's a lotta car.",
                    Mileage = 50,
                    SalePrice = 14000m,
                    MSRP = 14500m,
                    VIN = "ABCDE12347",
                    Year = 2015,
                    BodyStyle = _styles.Single(s => s.Name == "Car"),
                    ConditionType = _conditions.Single(s => s.Name == "New"),
                    InteriorColor = _colors.Single(s => s.Name == "Black"),
                    ExteriorColor = _colors.Single(s => s.Name == "Black"),
                    ModelType = _models.Single(m => m.Name == "Camry"),
                    Trans = _transmissions.Single(t => t.Name == "Automatic")
                }
            };

            _states = new List<State>
            {
                new State
                {
                    Id = 1,
                    StateAbbreviation = "MN"
                },
                new State
                {
                    Id = 2,
                    StateAbbreviation = "TX"
                },
                new State
                {
                    Id = 3,
                    StateAbbreviation = "NY"
                },
                new State
                {
                    Id = 4,
                    StateAbbreviation = "CA"
                }
            };

            _addresses = new List<Address>
            {
                new Address
                {
                    Id = 1,
                    City = "Champlin",
                    CustState = _states.Single(s => s.StateAbbreviation == "MN"),
                    Street1 = "9113 Prairieview Ln. N",
                    ZipCode = "55316",
                }
            };

            _customers = new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    Email = "test@test.com",
                    Name = "Kellen Parkinson",
                    Phone = "(333)-333-3333",
                    CustAddress = _addresses.Single(a => a.Id == 1),
                    Transactions = new List<Sale>()
                    //build transaction object
                }
            };

            _sales = new List<Sale>
            {
                new Sale()
                {
                    Id = 1,
                    Price = 20000m,
                    SaleDate = new DateTime(2015, 1, 1),
                    SaleType = _purchaseTypes.Single(t => t.Name == "Cash"),
                    Buyer = _customers.Single(c => c.Name == "Kellen Parkinson"),
                    PurchasedVehicle = _vehicles.Single(v => v.Id == 1)
                    //build salesperson object
                                     
                }
            };

            //after sales are created, go through each sale and add it to the corresponding customer's list of sales
            foreach (var sale in _sales)
            {
                _customers.Single(c => c.Id == sale.Buyer.Id).Transactions.Add(sale);
            }
        }

        public Contact AddContactRequest(Contact toAdd)
        {
            _contactRequests.Add(toAdd);
            return toAdd;
        }

        public Make AddMake(Make toAdd, string userId)
        {
            _makes.Add(toAdd);
            return toAdd;
        }

        public Model AddModel(Model toAdd, string userId)
        {
            _models.Add(toAdd);
            return toAdd;
        }

        public Sale AddSale(Sale toAdd)
        {
            _sales.Add(toAdd);
            return toAdd;
        }

        public Vehicle AddVehicle(Vehicle toAdd)
        {
            toAdd.Id = _vehicles.Max(v => v.Id) + 1;
            _vehicles.Add(toAdd);
            return toAdd;
        }
        
        //todo
        public AppUser CreateUser(string userName, string password, string role)
        {
            throw new NotImplementedException();
        }

        //todo
        public AppUser EditUser(string password, string role)
        {
            throw new NotImplementedException();
        }

        public Vehicle EditVehicle(Vehicle editedVehicle)
        {
            var toEdit = _vehicles.Single(v => v.Id == editedVehicle.Id);

            toEdit.BodyStyle = editedVehicle.BodyStyle;
            toEdit.ConditionType = editedVehicle.ConditionType;
            toEdit.Description = editedVehicle.Description;
            toEdit.ExteriorColor = editedVehicle.ExteriorColor;
            toEdit.InteriorColor = editedVehicle.InteriorColor;
            toEdit.IsFeatured = editedVehicle.IsFeatured;
            toEdit.Mileage = editedVehicle.Mileage;
            toEdit.ModelType = editedVehicle.ModelType;
            toEdit.MSRP = editedVehicle.MSRP;
            toEdit.PicturePath = editedVehicle.PicturePath;
            toEdit.SalePrice = editedVehicle.SalePrice;
            toEdit.Trans = editedVehicle.Trans;
            toEdit.VIN = editedVehicle.VIN;
            toEdit.Year = editedVehicle.Year;
            
            return toEdit;
        }

        public Vehicle DeleteVehicle(int Id) => throw new NotImplementedException();

        public Address GetAddress(int id) => _addresses.Single(a => a.Id == id);

        public List<Address> GetAllAddresses() => _addresses;

        public List<Style> GetAllBodyStyles() => _styles;

        public List<Color> GetAllColors() => _colors;

        public List<Condition> GetAllConditions() => _conditions;

        public List<Contact> GetAllContactRequests() => _contactRequests;

        public List<Customer> GetAllCustomers() => _customers;

        public List<AppUser> GetAllEmployees() => _employees;

        public List<Make> GetAllMakes() => _makes;

        public List<Model> GetAllModels() => _models;

        public List<PurchaseType> GetAllPurchaseTypes() => _purchaseTypes;

        public List<Sale> GetAllSales() => _sales;

        public List<State> GetAllStates() => _states;

        public List<Transmission> GetAllTransmissions() => _transmissions;

        public List<Vehicle> GetAllVehicles() => _vehicles;

        public Color GetColor(int id) => _colors.Single(a => a.Id == id);

        public Condition GetCondition(int id) => _conditions.Single(a => a.Id == id);

        public Contact GetContactRequest(int id) => _contactRequests.Single(a => a.Id == id);

        public Customer GetCustomer(int id) => _customers.Single(a => a.Id == id);

        public Make GetMake(int id) => _makes.Single(a => a.Id == id);

        public Model GetModel(int id) => _models.Single(a => a.Id == id);

        public PurchaseType GetPurchaseType(int id) => _purchaseTypes.Single(a => a.Id == id);

        public Sale GetSale(int id) => _sales.Single(a => a.Id == id);

        public State GetState(int id) => _states.Single(a => a.Id == id);

        public Style GetStyle(int id) => _styles.Single(a => a.Id == id);

        public Transmission GetTransmission(int id) => _transmissions.Single(a => a.Id == id);

        public AppUser GetUser(int id) => throw new NotImplementedException();

        public Vehicle GetVehicle(int id) => _vehicles.Single(a => a.Id == id);

        public Vehicle RemoveFromFeatured(Vehicle toRemoveFeatured)
        {
            throw new NotImplementedException();
        }
    }
}
