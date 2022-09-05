using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class PlanningStudentVM
    {
        public User Student { get; set; }
        public List<Planning> Planningen { get; set; }
    }
}
