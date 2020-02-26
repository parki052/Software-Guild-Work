using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_Dealership.Models
{
    public class SearchVM
    {
        public List<SelectListItem> MinPrices { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MaxPrices { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MinYears { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MaxYears { get; set; } = new List<SelectListItem>();

        public SelectListItem DefaultMin { get; set; } = new SelectListItem
        {
            Text = "No Min",
            Value = "0"
        };

        public SelectListItem DefaultMax { get; set; } = new SelectListItem
        {
            Text = "No Max",
            Value = int.MaxValue.ToString()
        };
        

        public string MinYear { get; set; } = "No Min";
        public string MaxYear { get; set; } = "No Max";
        public string MinPrice { get; set; } = "No Min";
        public string MaxPrice { get; set; } = "No Max";

        public string SearchTerm { get; set; } = "Enter make, model, or year";

        public void SetLists()
        {
            MinPrices.Add(DefaultMin);
            MinPrices.Add(new SelectListItem
            {
                Text = "$500",
                Value = "500"
            });
            MinPrices.Add(new SelectListItem
            {
                Text = "$1,000",
                Value = "1000"               
            });
            MinPrices.Add(new SelectListItem
            {
                Text = "$2,500",
                Value = "2500"
            });
            MinPrices.Add(new SelectListItem
            {
                Text = "$5,000",
                Value = "5000"
            });
            MinPrices.Add(new SelectListItem
            {
                Text = "$7,500",
                Value = "7500"
            });
            MinPrices.Add(new SelectListItem
            {
                Text = "$10,000",
                Value = "10000"
            });
            MinPrices.Add(new SelectListItem
            {
                Text = "$15,000",
                Value = "15000"
            });
            MinPrices.Add(new SelectListItem
            {
                Text = "$20,000",
                Value = "20000"
            });

            MaxPrices.Add(DefaultMax);
            MaxPrices.Add(new SelectListItem
            {
                Text = "$500",
                Value = "500"
            });
            MaxPrices.Add(new SelectListItem
            {
                Text = "$1,000",
                Value = "1000"
            });
            MaxPrices.Add(new SelectListItem
            {
                Text = "$2,500",
                Value = "2500"
            });
            MaxPrices.Add(new SelectListItem
            {
                Text = "$5,000",
                Value = "5000"
            });
            MaxPrices.Add(new SelectListItem
            {
                Text = "$7,500",
                Value = "7500"
            });
            MaxPrices.Add(new SelectListItem
            {
                Text = "$10,000",
                Value = "10000"
            });
            MaxPrices.Add(new SelectListItem
            {
                Text = "$15,000",
                Value = "15000"
            });
            MaxPrices.Add(new SelectListItem
            {
                Text = "$20,000",
                Value = "20000"
            });

            MinYears.Add(DefaultMin);

            for (int year = 2000; year <= DateTime.Now.Year + 1; year++)
            {
                MinYears.Add(new SelectListItem
                {
                    Text = year.ToString(),
                    Value = year.ToString()
                });
            }

            MaxYears.Add(DefaultMax);

            for (int year = 2000; year <= DateTime.Now.Year + 1; year++)
            {
                MaxYears.Add(new SelectListItem
                {
                    Text = year.ToString(),
                    Value = year.ToString()
                });
            }
        }
    }
}