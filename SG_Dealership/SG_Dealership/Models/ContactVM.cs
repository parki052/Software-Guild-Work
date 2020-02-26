using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SG_Dealership.Models
{
    public class ContactVM
    {
        public string VIN { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Message { get; set; }

        public void EmbedVinToMessage(string vin)
        {
            Message += $"(VIN: {vin})";
        }
    }
}