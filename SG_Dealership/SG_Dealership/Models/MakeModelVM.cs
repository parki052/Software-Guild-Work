using BLL;
using Models.VehicleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Models
{
    public class MakeModelVM
    {
        public List<Make> AllMakes { get; set; }
        public List<Model> AllModels { get; set; }
        public List<SelectListItem> Makes { get; set; } = new List<SelectListItem>();
        public string UserId { get; set; }

        public string SubmittedMake { get; set; }
        public string SubmittedModel { get; set; }

        public int SubmittedMakeId { get; set; }

        public void SetMakesAndModels(Manager manager)
        {
            AllMakes = manager.GetAllMakes();
            AllModels = manager.GetAllModels();

            foreach(var make in AllMakes)
            {
                Makes.Add(new SelectListItem
                {
                    Text = make.Name,
                    Value = make.Id.ToString()
                });
            }
        }
    }
}