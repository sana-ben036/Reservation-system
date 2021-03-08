using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("reservation")]
    [Index(nameof(IdPlan), Name = "IdPlan")]
    [Index(nameof(IdTypeR), Name = "IdTypeR")]
    [Index(nameof(IdUser), Name = "IdUser")]
    public partial class reservation
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int IdRe { get; set; }
        [Column(TypeName = "int(11)")]
        public int IdTypeR { get; set; }
        [Column(TypeName = "int(11)")]
        public int IdPlan { get; set; }
        [Column(TypeName = "int(11)")]
        public int IdUser { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date_demande { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date_reseration { get; set; }
        [Required]
        [StringLength(60)]
        public string Status { get; set; }

        [ForeignKey(nameof(IdPlan))]
        [InverseProperty(nameof(planing.reservations))]
        public virtual planing IdPlanNavigation { get; set; }
        [ForeignKey(nameof(IdTypeR))]
        [InverseProperty(nameof(typereservation.reservations))]
        public virtual typereservation IdTypeRNavigation { get; set; }
        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(utilisateur.reservations))]
        public virtual utilisateur IdUserNavigation { get; set; }
    }
}
