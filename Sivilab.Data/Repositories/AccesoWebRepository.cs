using Dapper;
using System.Data;
using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Data.Repositories
{
    public class AccesoWebRepository : IAccesoWebRepository
    {
        private readonly IDbConnection _db;

        public AccesoWebRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<AccesoWeb?> ObtenerPorEmail(string email)
        {
            return await _db.QueryFirstOrDefaultAsync<AccesoWeb>(
                "sp_AccesoWeb_ObtenerPorEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<AccesoWeb?> ObtenerPorUserName(string userName)
        {
            return await _db.QueryFirstOrDefaultAsync<AccesoWeb>(
                "sp_AccesoWeb_ObtenerPorUserName",
                new { UserName = userName },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> ExisteEmail(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@Existe", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(
                "sp_AccesoWeb_ExisteEmail",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<bool>("@Existe");
        }

        public async Task<bool> ExisteUserName(string userName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", userName);
            parameters.Add("@Existe", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(
                "sp_AccesoWeb_ExisteUserName",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<bool>("@Existe");
        }

        public async Task<int> Crear(AccesoWeb acceso)
        {
            var resultado = await _db.QuerySingleAsync<int>(
                "sp_AccesoWeb_Crear",
                new
                {
                    acceso.Curp,
                    acceso.Nombre,
                    acceso.Paterrno,
                    acceso.Materno,
                    acceso.UserName,
                    acceso.Email,
                    acceso.PasswordHash,
                    acceso.IsEmailConfirmed,
                    acceso.ConfirmationCode,
                    acceso.Role
                },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<bool> Actualizar(AccesoWeb acceso)
        {
            var resultado = await _db.QuerySingleAsync<int>(
                "sp_AccesoWeb_Actualizar",
                new
                {
                    acceso.CveAccesoWeb,
                    acceso.Nombre,
                    acceso.Paterrno,
                    acceso.Materno,
                    acceso.UserName,
                    acceso.Email,
                    acceso.PasswordHash,
                    acceso.IsEmailConfirmed,
                    acceso.ConfirmationCode,
                    acceso.Role
                },
                commandType: CommandType.StoredProcedure
            );

            return resultado > 0;
        }

        public async Task<bool> ValidarCredenciales(string email, string passwordHash)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            parameters.Add("@PasswordHash", passwordHash);
            parameters.Add("@EsValido", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(
                "sp_AccesoWeb_ValidarCredenciales",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<bool>("@EsValido");
        }

        public async Task<bool> ConfirmarEmail(string email, string confirmationCode)
        {
            var resultado = await _db.QuerySingleAsync<int>(
                "sp_AccesoWeb_ConfirmarEmail",
                new { Email = email, ConfirmationCode = confirmationCode },
                commandType: CommandType.StoredProcedure
            );

            return resultado > 0;
        }

        public async Task<bool> ActualizarContrasena(string email, string nuevaPasswordHash)
        {
            var resultado = await _db.QuerySingleAsync<int>(
                "sp_AccesoWeb_ActualizarContrasena",
                new { Email = email, NuevaPasswordHash = nuevaPasswordHash },
                commandType: CommandType.StoredProcedure
            );

            return resultado > 0;
        }
    }
}