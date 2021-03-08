using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("role")]
    public partial class role
    {
        public role()
        {
            utilisateur_roles = new HashSet<utilisateur_role>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int IdRole { get; set; }
        [Required]
        [StringLength(60)]
        public string nom { get; set; }

        [InverseProperty(nameof(utilisateur_role.IdRoleNavigation))]
        public virtual ICollection<utilisateur_role> utilisateur_roles { get; set; }
    }
}
