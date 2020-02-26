using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SGFlooring.Models;
using SGFlooring.Data;

namespace SGFlooring.BLL
{
    public static class ManagerFactory
    {
        private const string _liveOrderDir = @"C:\Data\SGFlooring\Data\Orders";
        private const string _liveProductFile = @"C:\Data\SGFlooring\Data\Products.txt";
        private const string _liveStateTaxFile = @"C:\Data\SGFlooring\Data\Taxes.txt";


        public static Manager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch(mode)
            {
                //case "Live":
                    //return new Manager(new FileOrderRepo(_liveOrderDir),new FileMaterialRepo(_liveProductFile),new FileTaxRepo(_liveStateTaxFile));
                case "InMemory":
                    return new Manager(new InMemoryOrderRepo(), new InMemoryMaterialRepo(), new InMemoryTaxRepo());
                default:
                    throw new Exception("Mode value in app config is invalid.");
            }
        }
    }
}
