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
            const string sql = @"
                SELECT * FROM Acceso_Web 
                WHERE Email = @Email";
            
            return await _db.QueryFirstOrDefaultAsync<AccesoWeb>(sql, new { Email = email });
        }

        public async Task<AccesoWeb?> ObtenerPorUserName(string userName)
        {
            const string sql = @"
                SELECT * FROM Acceso_Web 
                WHERE UserName = @UserName";
            
            return await _db.QueryFirstOrDefaultAsync<AccesoWeb>(sql, new { UserName = userName });
        }

        public async Task<bool> ExisteEmail(string email)
        {
            const string sql = @"
                SELECT COUNT(1) FROM Acceso_Web 
                WHERE Email = @Email";
            
            var count = await _db.QuerySingleAsync<int>(sql, new { Email = email });
            return count > 0;
        }

        public async Task<bool> ExisteUserName(string userName)
        {
            const string sql = @"
                SELECT COUNT(1) FROM Acceso_Web 
                WHERE UserName = @UserName";
            
            var count = await _db.QuerySingleAsync<int>(sql, new { UserName = userName });
            return count > 0;
        }

        public async Task<int> Crear(AccesoWeb acceso)
        {
            const string sql = @"
                INSERT INTO Acceso_Web 
                    (Nombre, Paterrno, Materno, UserName, Email, PasswordHash, 
                     IsEmailConfirmed, ConfirmationCode, Role)
                VALUES 
                    (@Nombre, @Paterrno, @Materno, @UserName, @Email, @PasswordHash,
                     @IsEmailConfirmed, @ConfirmationCode, @Role);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            
            return await _db.QuerySingleAsync<int>(sql, acceso);
        }

        public async Task<bool> Actualizar(AccesoWeb acceso)
        {
            const string sql = @"
                UPDATE Acceso_Web 
                SET Nombre = @Nombre,
                    Paterrno = @Paterrno,
                    Materno = @Materno,
                    UserName = @UserName,
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    IsEmailConfirmed = @IsEmailConfirmed,
                    ConfirmationCode = @ConfirmationCode,
                    Role = @Role
                WHERE CveAccesoWeb = @CveAccesoWeb";
            
            var rows = await _db.ExecuteAsync(sql, acceso);
            return rows > 0;
        }

        public async Task<bool> ValidarCredenciales(string email, string passwordHash)
        {
            const string sql = @"
                SELECT COUNT(1) FROM Acceso_Web 
                WHERE Email = @Email AND PasswordHash = @PasswordHash";
            
            var count = await _db.QuerySingleAsync<int>(sql, new { Email = email, PasswordHash = passwordHash });
            return count > 0;
        }

        public async Task<bool> ConfirmarEmail(string email, string confirmationCode)
        {
            const string sql = @"
                UPDATE Acceso_Web 
                SET IsEmailConfirmed = 1, ConfirmationCode = NULL
                WHERE Email = @Email AND ConfirmationCode = @ConfirmationCode";
            
            var rows = await _db.ExecuteAsync(sql, new { Email = email, ConfirmationCode = confirmationCode });
            return rows > 0;
        }

        public async Task<bool> ActualizarContrasena(string email, string nuevaPasswordHash)
        {
            const string sql = @"
                UPDATE Acceso_Web 
                SET PasswordHash = @NuevaPasswordHash
                WHERE Email = @Email";
            
            var rows = await _db.ExecuteAsync(sql, new { Email = email, NuevaPasswordHash = nuevaPasswordHash });
            return rows > 0;
        }
    }
}