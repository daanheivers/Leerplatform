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
        public Vak Vak { get; set; }
        public Lokaal Lokaal { get; set; }
    }
}
