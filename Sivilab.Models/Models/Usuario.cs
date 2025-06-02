﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Models.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public bool Validado { get; set; }
        public string CodigoValidacion { get; set; }

    }
}
