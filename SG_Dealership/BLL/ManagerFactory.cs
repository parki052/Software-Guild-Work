using Data.Repos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BLL
{
    public static class ManagerFactory
    {

        public static Manager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
            
            switch (mode)
            {
                case "InMem":
                    return new Manager(new InMemRepo());
                case "Entity":
                    return new Manager(new EntityRepo());
                default:
                    throw new Exception("The value in app config for repository mode is invalid. Please contact IT.");
            }
        }
    }
}
