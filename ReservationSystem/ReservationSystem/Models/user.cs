using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("user")]
    public partial class user 
    {
        public user()
        {
            reservations = new HashSet<reservation>();
            user_roles = new HashSet<user_role>();
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
        [InverseProperty(nameof(user_role.IdUserNavigation))]
        public virtual ICollection<user_role> user_roles { get; set; }
    }
}
