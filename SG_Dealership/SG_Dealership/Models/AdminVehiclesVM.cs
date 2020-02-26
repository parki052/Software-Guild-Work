using BLL;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_Dealership.Models
{
    public class AdminVehiclesVM
    {
        public List<Vehicle> VehiclesForSale { get; set; }

        public void SetVehicles (Manager manager)
        {
            VehiclesForSale = manager.GetVehiclesForSale();
        }
    }
}