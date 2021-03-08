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
            user_roles = new HashSet<user_role>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int IdRole { get; set; }
        [Required]
        [StringLength(60)]
        public string nom { get; set; }

        [InverseProperty(nameof(user_role.IdRoleNavigation))]
        public virtual ICollection<user_role> user_roles { get; set; }
    }
}
