using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class Vak
    {
        public string VakId { get; set; }
        public string Titel { get; set; }
        public int Studiepunten { get; set; }
        public List<Les> Lessen { get; set; }
    }
}
