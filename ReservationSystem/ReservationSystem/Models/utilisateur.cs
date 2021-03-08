using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("utilisateur")]
    public partial class utilisateur
    {
        public utilisateur()
        {
            reservations = new HashSet<reservation>();
            utilisateur_roles = new HashSet<utilisateur_role>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int IdUser { get; set; }
        [Column(TypeName = "int(11)")]
        public int? NRV { get; set; }
        [StringLength(60)]
        public string Nom { get; set; }
        [StringLength(60)]
        public string Prenom { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [InverseProperty(nameof(reservation.IdUserNavigation))]
        public virtual ICollection<reservation> reservations { get; set; }
        [InverseProperty(nameof(utilisateur_role.IdUserNavigation))]
        public virtual ICollection<utilisateur_role> utilisateur_roles { get; set; }
    }
}
