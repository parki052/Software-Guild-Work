using BLL;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_Dealership.Models
{
    public class DetailsVM
    {
        public Vehicle Vehicle { get; set; }

        public void SetVehicle(Manager manager, int id)
        {
            Vehicle = manager.GetVehicle(id);
        }
    }
}