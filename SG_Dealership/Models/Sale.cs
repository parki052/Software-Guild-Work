using Models.Identity;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Sale
    {
        public int Id { get; set; }
        public virtual Vehicle PurchasedVehicle { get; set; }
        public decimal Price { get; set; }
        public virtual Customer Buyer { get; set; }
        public virtual AppUser Employee { get; set; }
        public DateTime? SaleDate { get; set; }
        public virtual PurchaseType SaleType { get; set; }

    }
}
