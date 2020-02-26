using System;
using System.Collections.Generic;
using System.Text;

namespace Models.VehicleDetails
{
    public class Vehicle
    {
        public int Id { get; set; }
        public bool IsFeatured { get; set; }
        public virtual Model ModelType { get; set; }
        public virtual Condition ConditionType { get; set; }
        public virtual Style BodyStyle { get; set; }
        public int Year { get; set; }
        public virtual Transmission Trans { get; set; }
        public virtual Color ExteriorColor { get; set; }
        public virtual Color InteriorColor { get; set; }
        public int Mileage { get; set; }
        public string VIN { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
    }
}
