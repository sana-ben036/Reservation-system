using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("user_role")]
    [Index(nameof(IdRole), Name = "IdRole")]
    public partial class user_role
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int IdUser { get; set; }
        [Key]
        [Column(TypeName = "int(11)")]
        public int IdRole { get; set; }

        [ForeignKey(nameof(IdRole))]
        [InverseProperty(nameof(role.user_roles))]
        public virtual role IdRoleNavigation { get; set; }
        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(user.user_roles))]
        public virtual user IdUserNavigation { get; set; }
    }
}
