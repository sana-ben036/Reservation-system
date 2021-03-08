using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("planing")]
    [Index(nameof(IdTypeR), Name = "IdTypeR")]
    public partial class planing
    {
        public planing()
        {
            reservations = new HashSet<reservation>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int IdPlan { get; set; }
        [Column(TypeName = "int(11)")]
        public int IdTypeR { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date_Debut { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date_Fin { get; set; }
        [Column(TypeName = "int(11)")]
        public int Nombre_A { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey(nameof(IdTypeR))]
        [InverseProperty(nameof(typereservation.planings))]
        public virtual typereservation IdTypeRNavigation { get; set; }
        [InverseProperty(nameof(reservation.IdPlanNavigation))]
        public virtual ICollection<reservation> reservations { get; set; }
    }
}
