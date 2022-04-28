using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leerplatform.Models
{
    public class SigningUp
    {
        public int SigningUpId { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Naam is verplicht")]
        [Display(Name = "Naam")]
        public string LastName { get; set; }

        public int Age { get; set; }

        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

    }
}
