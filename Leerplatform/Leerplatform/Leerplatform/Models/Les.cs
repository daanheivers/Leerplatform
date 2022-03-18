using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class Les
    {
        public int LesId { get; set; }
        public DateTime Tijdstip { get; set; }
        public string VakId { get; set; }
        public Vak Vak { get; set; }
        public string LokaalId { get; set; }
        public Lokaal Lokaal { get; set; }
        public int PlanningId { get; set; }
        public Planning Planning { get; set; }
    }
}
