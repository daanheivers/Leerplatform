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
        public virtual Vak Vak { get; set; }
        public virtual Lokaal Lokaal { get; set; }
    }
}
