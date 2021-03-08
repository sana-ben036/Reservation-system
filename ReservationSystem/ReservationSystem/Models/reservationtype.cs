using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("reservationtype")]
    public partial class reservationtype
    {
        public reservationtype()
        {
            planings = new HashSet<planing>();
            reservations = new HashSet<reservation>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int IdTypeR { get; set; }
        [Required]
        [StringLength(100)]
        public string nom { get; set; }

        [InverseProperty(nameof(planing.IdTypeRNavigation))]
        public virtual ICollection<planing> planings { get; set; }
        [InverseProperty(nameof(reservation.IdTypeRNavigation))]
        public virtual ICollection<reservation> reservations { get; set; }
    }
}
