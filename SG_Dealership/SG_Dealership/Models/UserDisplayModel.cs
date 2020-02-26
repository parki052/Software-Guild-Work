using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_Dealership.Models
{
    public class UserDisplayModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}