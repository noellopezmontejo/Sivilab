﻿using System.ComponentModel.DataAnnotations;

namespace Sivilab.Web.Components.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        public string Contrasena { get; set; } 
        public string Rol { get; set; } 
    }
}
