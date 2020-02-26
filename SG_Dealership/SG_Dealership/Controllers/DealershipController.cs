using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Models.VehicleDetails;
using System.Data.SqlClient;

namespace SG_Dealership.Controllers
{
    public class DealershipController : ApiController
    {
        //      localhost/models/get/12/
        [Route("models/get/{makeId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult AllModelsByMake(int makeId)
        {
            var manager = ManagerFactory.Create();
            var modelsByMake = manager.GetModelsByMake(makeId);

            if (modelsByMake.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(modelsByMake);
            }
        }

        [Route("inventory/search/{searchTerm}/{minPrice}/{maxPrice}/{minYear}/{maxYear}/{searchType}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult VehiclesBySearch(string searchTerm, decimal minPrice, decimal maxPrice, int minYear, int maxYear, string searchType)
        {
            var manager = ManagerFactory.Create();
            var searchResult = new List<Vehicle>();

            if (searchTerm == "Enter make, model, or year")
            {
                searchTerm = "";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"Server=localhost;Database=SG_Dealership;Trusted_Connection=yes;";
                var cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT Vehicles.PicturePath, Vehicles.Id VehicleId, Vehicles.Mileage, Vehicles.VIN, Vehicles.SalePrice, Vehicles.MSRP, Models.Id ModelId, Vehicles.Year VehicleYear, c.Id ExtColorId, ic.Id IntColorId, Styles.Id BodyStyleId, Transmissions.Id TransId, Conditions.Id ConditionId" +
                    " FROM Vehicles" +
                    " INNER JOIN Colors c ON Vehicles.ExteriorColor_Id = c.Id" +
                    " INNER JOIN Colors ic ON Vehicles.InteriorColor_Id = ic.Id" +
                    " INNER JOIN Transmissions ON Vehicles.Trans_Id = Transmissions.Id" +
                    " INNER JOIN Styles ON Vehicles.BodyStyle_Id = Styles.Id" +
                    " INNER JOIN Models ON Vehicles.ModelType_Id = Models.Id" +
                    " INNER JOIN Makes ON Makes.Id = Models.Maker_Id" +
                    " INNER JOIN Conditions ON Vehicles.ConditionType_Id = Conditions.Id" +
                    " WHERE (Vehicles.SalePrice > @minPrice AND Vehicles.SalePrice < @maxPrice)" +
                    " AND (Vehicles.Year >= @minYear AND Vehicles.Year <= @maxYear)" +
                    " AND (Makes.Name LIKE Concat('%', @searchTerm, '%') OR Models.Name LIKE Concat('%', @searchTerm, '%') OR Vehicles.Year LIKE Concat('%', @searchTerm, '%'))";
                cmd.Parameters.AddWithValue("@minPrice", minPrice);
                cmd.Parameters.AddWithValue("@maxPrice", maxPrice);
                cmd.Parameters.AddWithValue("@minYear", minYear);
                cmd.Parameters.AddWithValue("@maxYear", maxYear);
                cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                conn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Vehicle v = new Vehicle();


                        v.Id = (int)dr["VehicleId"];
                        v.Mileage = (int)dr["Mileage"];
                        v.VIN = dr["VIN"].ToString();
                        v.SalePrice = (decimal)dr["SalePrice"];
                        v.MSRP = (decimal)dr["MSRP"];
                        v.ModelType = manager.GetModel((int)dr["ModelId"]);
                        v.Year = (int)dr["VehicleYear"];
                        v.ExteriorColor = manager.GetColor((int)dr["ExtColorId"]);
                        v.InteriorColor = manager.GetColor((int)dr["IntColorId"]);
                        v.BodyStyle = manager.GetBodyStyle((int)dr["BodyStyleId"]);
                        v.ConditionType = manager.GetCondition((int)dr["ConditionId"]);
                        v.Trans = manager.GetTransmission((int)dr["TransId"]);
                        v.PicturePath = dr["PicturePath"].ToString();
                        if (!manager.GetAllSales().Any(s => s.PurchasedVehicle.Id == v.Id))
                        {
                            switch (searchType)
                            {
                                case "New":
                                    switch (v.ConditionType.Name)
                                    {
                                        case "New":
                                            searchResult.Add(v);
                                            break;

                                        default: break;
                                    }
                                    break;
                                case "Used":
                                    switch (v.ConditionType.Name)
                                    {
                                        case "Used":
                                            searchResult.Add(v);
                                            break;

                                        default: break;
                                    }
                                    break;
                                case "NewUsed":
                                    searchResult.Add(v);
                                    break;

                                default: break;
                            }
                        }



                    }
                }
            }
            return Ok(searchResult);
        }
    }
}