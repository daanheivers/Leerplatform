using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class Planning
    {
        public int PlanningId { get; set; }
        public string Naam { get; set; }
        public List<Les> Lessen { get; set; }
        public List<User> Studenten { get; set; }
    }
}
