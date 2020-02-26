using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class InMemoryMaterialRepo : IMaterialRepository
    {
        private static List<Material> _materials = new List<Material>
        {
            new Material("Tile", 3.50m, 4.15m),
            new Material("Carpet", 2.25m, 2.10m),
            new Material("Laminate", 1.75m, 2.10m),
            new Material("Wood", 5.15m, 4.75m)

        };

        public IEnumerable<Material> GetMaterials() => _materials;

    }
}
