using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sivilab.Models.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }=string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }= string.Empty;

        [Required]
        public string Contrasena { get; set; }
        
        [Required]
        public string Rol { get; set; }
        public bool Validado { get; set; }
        public string CodigoValidacion { get; set; } = string.Empty;

    }
}
