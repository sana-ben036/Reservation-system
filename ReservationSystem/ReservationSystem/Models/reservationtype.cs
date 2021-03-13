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
        public int Id { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The Type name field is required !")]
        [Display(Name = "Enter The Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The number of students field is required !")]
        [Display(Name = "Number of Students ")]
        public int NumberA { get; set; }
        public virtual IList<Reservation> Reservations { get; set; }
    }
}
