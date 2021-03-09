using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class ReservationType
    {
        [Key]
        public int IdType { get; set; }

        [Required(ErrorMessage = "The Type name field is required !")]
        [Display(Name = "Enter The Name")]
        public string TypeName { get; set; }
        public int AccessNbr { get; set; }
    }
}
