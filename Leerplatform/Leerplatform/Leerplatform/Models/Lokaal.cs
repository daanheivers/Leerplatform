using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class Lokaal
    {
        public string LokaalId { get; set; }
        public string Naam { get; set; }
        public string Plaats { get; set; }
        public int Capaciteit { get; set; }
        public virtual ICollection<Middel> Middelen { get; set; }
    }
}
