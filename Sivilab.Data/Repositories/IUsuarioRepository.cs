﻿using Sivilab.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task<IEnumerable<Usuario>> ObtenerTodos();
        Task<int> RegistrarUsuario(Usuario usuario);
        Task<int> AgregarUsuario(Usuario usuario);
        Task<bool> ValidarCorreo(string codigoValidacion);
        Task<Usuario> ValidarCredenciales(string email, string contrasena);

        // Métodos para completar el CRUD:
        Task ActualizarUsuario(Usuario usuario);
        Task EliminarUsuario(int id);
    }
}
