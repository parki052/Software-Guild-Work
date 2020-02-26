using Microsoft.AspNet.Identity;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Models
{
    public class AddUserVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        public string SelectedRoleId { get; set; }

        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();

        public void SetRoles(RoleManager<AppRole> roleManager)
        {
            foreach(var role in roleManager.Roles)
            {
                AllRoles.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id
                });
            }
        }
    }
}