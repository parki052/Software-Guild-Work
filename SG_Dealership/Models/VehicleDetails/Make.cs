using Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.VehicleDetails
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Model> Models { get; set; }
        public DateTime? DateAdded { get; set; }
        public virtual AppUser UserAddedBy { get; set; }
    }
}
