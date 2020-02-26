using BLL;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_Dealership.Models
{
    public class HomeVM
    {
        public List<Vehicle> FeaturedVehicles { get; set; }

        public void SetFeaturedVehicles(Manager manager)
        {
            var vehicles = manager.GetAllVehicles().Where(v => v.IsFeatured).OrderByDescending(v => v.MSRP).ToList();
            if(vehicles.Count > 8)
            {
                vehicles.Take(8);
            }
            FeaturedVehicles = vehicles;
        }
    }
}