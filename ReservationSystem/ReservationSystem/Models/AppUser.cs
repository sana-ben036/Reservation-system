using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class AppUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual IList<Reservation> Reservations { get; set; }
    }
}
