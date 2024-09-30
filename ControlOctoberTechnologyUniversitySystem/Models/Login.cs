﻿using System.ComponentModel.DataAnnotations;

namespace ControlOctoberTechnologyUniversitySystem.Models
{
    public class Login
    {
        [Required(ErrorMessage = "User Name is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}
