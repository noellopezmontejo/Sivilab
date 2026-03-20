using Dapper;
using System.Data;
using Sivilab.Models.Models;
using System.Threading.Tasks;

namespace Sivilab.Data.Repositories
{
    public class OfertaRepository : IOfertaRepository
    {
        private readonly IDbConnection _db;

        public OfertaRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CrearOferta(OfertaEmpleo oferta)
        {
            // Assuming a stored procedure exists
            return await _db.QuerySingleAsync<int>(
                "spCrearOfertaEmpleo", // Placeholder SP name
                oferta,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
