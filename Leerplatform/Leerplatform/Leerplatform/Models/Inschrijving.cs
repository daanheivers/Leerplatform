using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class Inschrijving
    {
        public int InschrijvingId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string VakId { get; set; }
        public Vak Vak { get; set; }
        public bool Aanvaard { get; set; }
    }
}
