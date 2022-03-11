using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class Planning
    {
        public int PlanningId { get; set; }
        public virtual ICollection<Les> Lessen { get; set; }
    }
}
