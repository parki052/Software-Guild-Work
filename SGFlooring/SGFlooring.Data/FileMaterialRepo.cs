using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class FileMaterialRepo : IMaterialRepository
    {
        private static string _path;
        public FileMaterialRepo(string path) => _path = path;

        public IEnumerable<Material> GetMaterials()
        {
            List<Material> materialRepo = new List<Material>();

            using (StreamReader sr = new StreamReader(_path))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if(line != null)
                    {
                        string[] row = line.Split('\t');

                        string productType = row[0];
                        decimal costPerSquareFoot = decimal.Parse(row[1]);
                        decimal laborCostPerSquareFoot = decimal.Parse(row[2]);

                        Material material = new Material(productType, costPerSquareFoot, laborCostPerSquareFoot);
                        materialRepo.Add(material);
                    }
                }
            }
            return materialRepo;            
        }
    }
}
