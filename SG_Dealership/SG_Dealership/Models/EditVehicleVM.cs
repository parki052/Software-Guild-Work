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
    public class EditVehicleVM
    {
        public List<SelectListItem> AllMakes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllModels { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllTransmissions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MakeModels { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllConditions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllStyles { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllColors { get; set; } = new List<SelectListItem>();

        public int VehicleId { get; set; }
        public bool IsFeatured { get; set; }
        public int SelectedMakeId { get; set; }
        public int SelectedModelId { get; set; }
        public int SelectedConditionId { get; set; }
        public int SelectedStyleId { get; set; }
        public int SelectedColorId { get; set; }
        public int SelectedInteriorColorId { get; set; }
        public int SelectedTransId { get; set; }


        [Required]
        public int Year { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public string VIN { get; set; }
        [Required]
        public decimal MSRP { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        [Required]
        public string Description { get; set; }


        public HttpPostedFileBase Picture { get; set; }
        public string PicturePath { get; set; }

        public void SetAllLists(Manager manager)
        {

            if (SelectedModelId != 0)
            {
                var selectedModel = manager.GetModel(SelectedModelId);
                AllModels.Add(new SelectListItem { Value = selectedModel.Id.ToString(), Text = selectedModel.Name });
            }
            //makes
            foreach (var make in manager.GetAllMakes())
            {
                AllMakes.Add(new SelectListItem { Value = make.Id.ToString(), Text = make.Name });
            }
            //models

            //conditions
            foreach (var condition in manager.GetAllConditions())
            {
                AllConditions.Add(new SelectListItem { Value = condition.Id.ToString(), Text = condition.Name });
            }

            //styles
            foreach (var style in manager.GetAllBodyStyles())
            {
                AllStyles.Add(new SelectListItem { Value = style.Id.ToString(), Text = style.Name });
            }

            //colors
            foreach (var color in manager.GetAllColors())
            {
                AllColors.Add(new SelectListItem { Value = color.Id.ToString(), Text = color.Name });
            }

            //transmissions
            foreach (var trans in manager.GetAllTransmissions())
            {
                AllTransmissions.Add(new SelectListItem { Value = trans.Id.ToString(), Text = trans.Name });
            }
        }
        public void SetSelectionsForEdit(Vehicle toEdit)
        {
            SelectedMakeId = toEdit.ModelType.Maker.Id;
            SelectedModelId = toEdit.ModelType.Id;
            SelectedConditionId = toEdit.ConditionType.Id;
            SelectedStyleId = toEdit.BodyStyle.Id;
            SelectedColorId = toEdit.ExteriorColor.Id;
            SelectedInteriorColorId = toEdit.InteriorColor.Id;
            SelectedTransId = toEdit.Trans.Id;
            IsFeatured = toEdit.IsFeatured;

            if (toEdit.PicturePath != null)
            {
                PicturePath = toEdit.PicturePath;
            }

            Year = toEdit.Year;
            Mileage = toEdit.Mileage;
            VIN = toEdit.VIN;
            MSRP = toEdit.MSRP;
            SalePrice = toEdit.SalePrice;

            if (toEdit.Description != null)
            {
                Description = toEdit.Description;
            }
            else
            {
                Description = "-";
            }
        }
    }
}