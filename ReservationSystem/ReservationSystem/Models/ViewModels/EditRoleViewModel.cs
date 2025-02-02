﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models.ViewModels
{
    public class EditRoleViewModel 
    {

        
        [Display(Name = "ID:")]
        public string Id { get; set; }

        [Required(ErrorMessage = "The role name field is required !")]
        [Display(Name = "Enter The Name:")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
