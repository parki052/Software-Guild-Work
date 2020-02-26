using BLL;
using Microsoft.AspNet.Identity;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_Dealership.Models
{
    public class UsersVM
    {
        public List<UserDisplayModel> AllUsers { get; set; } = new List<UserDisplayModel>();

        public void SetUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var allUsers = userManager.Users.ToList();
            foreach (var user in allUsers)
            {
                var userModel = new UserDisplayModel
                {
                    UserId = user.Id,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    RoleName = roleManager.FindById(user.Roles.SingleOrDefault().RoleId).Name
                };
                AllUsers.Add(userModel);
            }
            AllUsers = AllUsers.OrderBy(u => u.LastName).ToList();
        }
    }
}