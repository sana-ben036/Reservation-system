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
        public int IdRe { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Reservation Date")]
        public string Date_R { get; set; }
        public string Status { get; set; }
        [Required]
        public ReservationType Type { get; set; }
    }
}
