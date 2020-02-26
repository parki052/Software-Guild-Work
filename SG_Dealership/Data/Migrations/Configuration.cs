namespace Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Identity;
    using Models.VehicleDetails;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Repos.EntityRepo>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.Repos.EntityRepo context)
        {
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

            if (!roleManager.RoleExists("Admin"))
            {

                roleManager.Create(new AppRole() { Name = "Admin" });

                var user = new AppUser()
                {
                    UserName = "testAdmin",
                    FirstName = "Dougie",
                    LastName = "Jones",
                    Email = "d.jones@sgdealer.com"
                    
                };

                userManager.Create(user, "password");

                userManager.AddToRole(user.Id, "Admin");
            }

            if (!roleManager.RoleExists("Sales"))
            {
                roleManager.Create(new AppRole() { Name = "Sales" });

                var user = new AppUser()
                {
                    UserName = "testSalesman",
                    FirstName = "Dale",
                    LastName = "Cooper",
                    Email = "d.cooper@sgdealer.com"
                };

                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "Sales");
            }

            if (!roleManager.RoleExists("Disabled"))
            {
                roleManager.Create(new AppRole() { Name = "Disabled" });

                var user = new AppUser()
                {
                    UserName = "testDisabled",
                    FirstName = "John",
                    LastName = "Bender",
                    Email = "j.bender@sgdealer.com"
                };

                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "Disabled");
            }



            context.PurchaseTypes.Add(new PurchaseType
            {
                Name = "Bank Finance"
            });
            context.PurchaseTypes.Add(new PurchaseType
            {
                Name = "Cash"
            });
            context.PurchaseTypes.Add(new PurchaseType
            {
                Name = "Dealer Finance"
            });

            context.SaveChanges();

            context.Colors.Add(new Color
            {
                Name = "Black"
            });
            context.Colors.Add(new Color
            {
                Name = "White"
            });
            context.Colors.Add(new Color
            {
                Name = "Grey"
            });
            context.Colors.Add(new Color
            {
                Name = "Red"
            });
            context.Colors.Add(new Color
            {
                Name = "Blue"
            });

            context.SaveChanges();

            context.Conditions.Add(new Condition
            {
                Name = "New"
            });
            context.Conditions.Add(new Condition
            {
                Name = "Used"
            });

            context.SaveChanges();

            context.Makes.Add(new Make()
            {
                Name = "Jaguar",
                UserAddedBy = context.Users.Single(u => u.UserName == "testAdmin"),
                DateAdded = DateTime.Now
            });
            context.Makes.Add(new Make()
            {
                Name = "Toyota",
                UserAddedBy = context.Users.Single(u => u.UserName == "testAdmin"),
                DateAdded = DateTime.Now
            });

            context.SaveChanges();

            context.Models.Add(new Model
            {
                Name = "Roadster",
                Maker = context.Makes.Single(m => m.Name == "Jaguar"),
                UserAddedBy = context.Users.Single(u => u.UserName == "testAdmin"),
                DateAdded = DateTime.Now
            });
            context.Models.Add(new Model
            {
                Name = "Corolla",
                Maker = context.Makes.Single(m => m.Name == "Toyota"),
                UserAddedBy = context.Users.Single(u => u.UserName == "testAdmin"),
                DateAdded = DateTime.Now
            });
            context.Models.Add(new Model
            {
                Name = "Camry",
                Maker = context.Makes.Single(m => m.Name == "Toyota"),
                UserAddedBy = context.Users.Single(u => u.UserName == "testAdmin"),
                DateAdded = DateTime.Now
            });

            context.SaveChanges();

            context.Styles.Add(new Style
            {
                Name = "Car"
            });
            context.Styles.Add(new Style
            {
                Name = "SUV"
            });
            context.Styles.Add(new Style
            {
                Name = "Truck"
            });
            context.Styles.Add(new Style
            {
                Name = "Van"
            });

            context.SaveChanges();

            context.Transmissions.Add(new Transmission
            {
                Name = "Automatic"
            });
            context.Transmissions.Add(new Transmission
            {
                Name = "Manual"
            });

            context.SaveChanges();

            context.Vehicles.Add(new Vehicle
            {
                Description = "This is a really great test car.",
                Mileage = 0,
                MSRP = 20000m,
                SalePrice = 19000m,
                VIN = "ABCDE12345",
                Year = 1956,
                BodyStyle = context.Styles.Single(s => s.Name == "Car"),
                ConditionType = context.Conditions.Single(c => c.Name == "New"),
                InteriorColor = context.Colors.Single(c => c.Name == "Black"),
                ExteriorColor = context.Colors.Single(c => c.Name == "Black"),
                ModelType = context.Models.Single(c => c.Name == "Roadster"),
                Trans = context.Transmissions.Single(c => c.Name == "Automatic")
                //set string picturepath                 
            });

            context.Vehicles.Add(new Vehicle
            {
                Description = "My grandma drives one of these.",
                Mileage = 50,
                SalePrice = 12000m,
                MSRP = 12500m,
                VIN = "ABCDE12346",
                Year = 2010,
                BodyStyle = context.Styles.Single(s => s.Name == "Car"),
                ConditionType = context.Conditions.Single(s => s.Name == "New"),
                InteriorColor = context.Colors.Single(s => s.Name == "White"),
                ExteriorColor = context.Colors.Single(s => s.Name == "White"),
                ModelType = context.Models.Single(m => m.Name == "Corolla"),
                Trans = context.Transmissions.Single(t => t.Name == "Automatic")
                //set string picturepath
            });

            context.Vehicles.Add(new Vehicle()
            {
                Description = "That's a lotta car.",
                Mileage = 50,
                SalePrice = 14000m,
                MSRP = 14500m,
                VIN = "ABCDE12347",
                Year = 2015,
                BodyStyle = context.Styles.Single(s => s.Name == "Car"),
                ConditionType = context.Conditions.Single(s => s.Name == "New"),
                InteriorColor = context.Colors.Single(s => s.Name == "Black"),
                ExteriorColor = context.Colors.Single(s => s.Name == "Black"),
                ModelType = context.Models.Single(m => m.Name == "Camry"),
                Trans = context.Transmissions.Single(t => t.Name == "Automatic")
            });

            context.SaveChanges();

            context.States.Add(new State
            {
                StateAbbreviation = "MN"
            });
            context.States.Add(new State
            {
                StateAbbreviation = "TX"
            });
            context.States.Add(new State
            {
                StateAbbreviation = "NY"
            });
            context.States.Add(new State
            {
                StateAbbreviation = "CA"
            });

            context.SaveChanges();

            context.Addresses.Add(new Address
            {
                City = "Champlin",
                CustState = context.States.Single(s => s.StateAbbreviation == "MN"),
                Street1 = "9113 Prairieview Ln. N",
                ZipCode = "55316",
            });

            context.SaveChanges();

            context.Customers.Add(new Customer
            {
                Email = "test@test.com",
                Name = "Kellen Parkinson",
                Phone = "(333)-333-3333",
                CustAddress = context.Addresses.Single(a => a.Id == 1),
                Transactions = new List<Sale>()
                //build transaction object
            });

            context.SaveChanges();

            context.Sales.Add(new Sale
            {
                Price = 20000m,
                SaleDate = new DateTime(2015, 1, 1),
                SaleType = context.PurchaseTypes.Single(t => t.Name == "Cash"),
                Buyer = context.Customers.Single(c => c.Name == "Kellen Parkinson"),
                PurchasedVehicle = context.Vehicles.Single(v => v.Id == 1),
                Employee = context.Users.Single(u => u.UserName == "testAdmin")


            });

            context.SaveChanges();
            //after sales are created, go through each sale and add it to the corresponding customer's list of sales
            foreach (var sale in context.Sales.ToList())
            {
                context.Customers.Single(c => c.Id == sale.Buyer.Id).Transactions.Add(sale);
            }

            context.SaveChanges();
        }
    }
}
