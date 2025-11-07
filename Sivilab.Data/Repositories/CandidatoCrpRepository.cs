using Dapper;
using System.Data;
using Sivilab.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sivilab.Data.Repositories
{
    public class CandidatoCrpRepository : ICandidatoCrpRepository
    {
        private readonly IDbConnection _db;

        public CandidatoCrpRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CandidatoCrp>> ObtenerTodos()
        {
            // Usa el stored procedure que devuelva todos los registros
            return await _db.QueryAsync<CandidatoCrp>(
                "spObtenerTodosCandidatosCrp",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<CandidatoCrp?> ObtenerPorId(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<CandidatoCrp>(
                "spObtenerCandidatoCrpPorId",
                new { CandidatoId = id },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<CandidatoCrp?> ObtenerPorCurp(string curp)
        {
            return await _db.QueryFirstOrDefaultAsync<CandidatoCrp>(
                "spObtenerCandidatoCrpPorCurp",
                new { Curp = curp },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<int> AgregarCandidato(CandidatoCrp candidato)
        {
            // Asume que el SP inserta y devuelve el nuevo Id (int)
            // Dapper mapeará las propiedades del objeto a los parámetros del SP
            return await _db.QuerySingleAsync<int>(
                "spAgregarCandidatoCrp",
                candidato,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> ActualizarCandidato(CandidatoCrp candidato)
        {
            var rows = await _db.ExecuteAsync(
                "spActualizarCandidatoCrp",
                candidato,
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }

        public async Task<bool> EliminarCandidato(int id)
        {
            var rows = await _db.ExecuteAsync(
                "spEliminarCandidatoCrp",
                new { CandidatoId = id },
                commandType: CommandType.StoredProcedure
            );

            return rows > 0;
        }
    }
}