using Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.VehicleDetails
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Make Maker { get; set; }
        public DateTime? DateAdded { get; set; }
        public virtual AppUser UserAddedBy { get; set; }
    }
}
