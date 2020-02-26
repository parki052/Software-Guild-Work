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
    public class FileTaxRepo : ITaxRepository
    {
        private static string _path;
        public FileTaxRepo(string path) => _path = path;

        public IEnumerable<StateTax> GetStateTaxes()
        {
            List<StateTax> stateTaxRepo = new List<StateTax>();

            using (StreamReader sr = new StreamReader(_path))
            {
                sr.ReadLine();
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    if(line != null)
                    {
                        string[] row = line.Split('\t');

                        string stateAbbreviation = row[0];
                        string stateName = row[1];
                        decimal taxRate = decimal.Parse(row[2]);

                        StateTax stateTax = new StateTax(stateAbbreviation, stateName, taxRate);
                        stateTaxRepo.Add(stateTax);
                    }
                }
            }
            return stateTaxRepo;
        }
    }
}
