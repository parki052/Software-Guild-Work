using BLL;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Models
{
    public class PurchaseVM
    {
        public Vehicle Vehicle { get; set; }

        [Required]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Street1 { get; set; }
        public string Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public int SelectedStateId { get; set; }

        [Required]
        public int SelectedPurchaseTypeId { get; set; }

        public List<SelectListItem> AllStates { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllPurchaseTypes { get; set; } = new List<SelectListItem>();

        public void SetListItems(Manager manager)
        {
            var allStates = manager.GetAllStates();
            foreach(var state in allStates)
            {
                AllStates.Add(new SelectListItem
                {
                    Text = state.StateAbbreviation,
                    Value = state.Id.ToString()
                });
            }

            var purchaseTypes = manager.GetAllPurchaseTypes();
            foreach(var type in purchaseTypes)
            {
                AllPurchaseTypes.Add(new SelectListItem
                {
                    Text = type.Name,
                    Value = type.Id.ToString()
                });
            }
        }
    }
}