using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "R-nummer")]
        public string RNummer { get; set; }
        public List<Planning> Planningen { get; set; }
    }
}
