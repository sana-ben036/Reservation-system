using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ReservationSystem.Models
{
    [Table("utilisateur_role")]
    [Index(nameof(IdRole), Name = "IdRole")]
    public partial class utilisateur_role
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int IdUser { get; set; }
        [Key]
        [Column(TypeName = "int(11)")]
        public int IdRole { get; set; }

        [ForeignKey(nameof(IdRole))]
        [InverseProperty(nameof(role.utilisateur_roles))]
        public virtual role IdRoleNavigation { get; set; }
        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(utilisateur.utilisateur_roles))]
        public virtual utilisateur IdUserNavigation { get; set; }
    }
}
