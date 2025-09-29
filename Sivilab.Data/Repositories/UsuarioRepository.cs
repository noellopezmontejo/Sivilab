using Sivilab.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Sivilab.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _db;

        public UsuarioRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            var query = "EXEC spObtenerUsuarioPorId @Id";
            return await _db.QueryFirstOrDefaultAsync<Usuario>(query, new { Id = id });
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodos()
        {
            var query = "EXEC spObtenerTodosUsuarios";
            return await _db.QueryAsync<Usuario>(query);
        }

        public async Task<int> AgregarUsuario(Usuario usuario)
        {
            var query = "EXEC spAgregarUsuario @Nombre, @Email, @Contrasena, @Rol";
            var parameters = new
            {
                usuario.Nombre,
                usuario.Email,
                usuario.Contrasena,
                usuario.Rol
            };

            return await _db.QuerySingleAsync<int>(query, parameters);
        }

        public async Task<int> RegistrarUsuario(Usuario usuario)
        {
            var codigoValidacion = Guid.NewGuid().ToString(); // Código único de validación
            var query = "EXEC spRegistrarUsuario @Nombre, @Email, @Contrasena, @CodigoValidacion";

            var parameters = new
            {
                usuario.Nombre,
                usuario.Email,
                usuario.Contrasena,
                CodigoValidacion = codigoValidacion
            };

            return await _db.QuerySingleAsync<int>(query, parameters);
        }

        public async Task<bool> ValidarCorreo(string codigoValidacion)
        {
            var query = "EXEC spValidarCorreo @CodigoValidacion";
            var result = await _db.ExecuteAsync(query, new { CodigoValidacion = codigoValidacion });
            return result > 0; // Devuelve true si se actualizó algún registro
        }

        public async Task<Usuario> ValidarCredenciales(string email, string contrasena)
        {
            var query = "EXEC spValidarCredenciales @Email, @Contrasena";
            return await _db.QueryFirstOrDefaultAsync<Usuario>(query, new { Email = email, Contrasena = contrasena });
        }

        // NUEVO: Actualizar usuario
        public async Task ActualizarUsuario(Usuario usuario)
        {
            var query = @"UPDATE Usuarios
                          SET Nombre = @Nombre,
                              Email = @Email,
                              Contrasena = @Contrasena,
                              Rol = @Rol,
                              Validado = @Validado
                          WHERE Id = @Id";
            await _db.ExecuteAsync(query, usuario);
        }

        // NUEVO: Eliminar usuario
        public async Task EliminarUsuario(int id)
        {
            var query = "DELETE FROM Usuarios WHERE Id = @Id";
            await _db.ExecuteAsync(query, new { Id = id });
        }


    }
}
