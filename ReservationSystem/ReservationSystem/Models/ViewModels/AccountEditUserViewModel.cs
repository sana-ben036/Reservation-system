using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReservationSystem.Models.ViewModels
{
    public class AccountEditUserViewModel
    {
        public AccountEditUserViewModel()
        {
            Roles = new List<string>();
        }


        public string Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }





        public IList<string> Roles { get; set; }























    }
}
