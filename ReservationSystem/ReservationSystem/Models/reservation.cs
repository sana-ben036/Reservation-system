using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        
        [Required (ErrorMessage = "The Date field is required !")]
        [Display(Name = "Date of Reservation")]
        public DateTime Date_R { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }

        [Required(ErrorMessage = "The Type field is required !")]
        [Display(Name = "Type")]
        public int TypeId { get; set; }
        public virtual ReservationType Type { get; set; }



        
    }
}
