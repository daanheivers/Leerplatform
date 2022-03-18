using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class VakPlanningLokaalVM
    {
        public Vak Vak { get; set; }
        public List<Planning> Planningen { get; set; }
        public List<Lokaal> Lokalen { get; set; }
    }
}
