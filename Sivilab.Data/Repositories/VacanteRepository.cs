using Dapper;
using System.Data;
using Sivilab.Models.Models;

namespace Sivilab.Data.Repositories
{
    public class VacanteRepository : IVacanteRepository
    {
        private readonly IDbConnection _db;

        public VacanteRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Vacante>> ObtenerVacantesVigentes()
        {
            try
            {
                var parameters = new DynamicParameters();
                // 0 para traer de todas las unidades operativas
                parameters.Add("@CveUnOperativa", 0); 
                // Formato varchar(20) esperado por el SP (YYYY-MM-DD es seguro para SQL Server)
                parameters.Add("@FechaVigencia", DateTime.Now.ToString("yyyy-MM-dd")); 

                // IMPORTANTE: Usar el nombre completo con esquema si es necesario
                var vacantes = await _db.QueryAsync<Vacante>(
                    "[dbempleos].[SP_Vacantes_Vigentes_Web]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return vacantes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerVacantesVigentes: {ex.Message}");
                // Retornar lista vacía en lugar de null para evitar excepciones en la vista
                return new List<Vacante>();
            }
        }

        public async Task<Vacante?> ObtenerPorId(int id)
        {
            // Este método necesitará su propio SP o consulta directa, ya que 
            // SP_Vacantes_Vigentes_Web devuelve una lista y no filtra por ID de vacante específicamente
            // de manera eficiente para un solo registro, aunque podríamos filtrar en memoria si son pocas.
            try
            {
                // Opción A: Si existe un SP para obtener una vacante
                /*
                return await _db.QueryFirstOrDefaultAsync<Vacante>(
                    "sp_Vacante_ObtenerPorId",
                    new { VacanteId = id },
                    commandType: CommandType.StoredProcedure
                );
                */

                // Opción B: Filtrar de la lista vigente (menos eficiente pero funcional si no hay otro SP)
                var vacantes = await ObtenerVacantesVigentes();
                return vacantes.FirstOrDefault(v => v.FolioSIVILAB == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerPorId: {ex.Message}");
                return null;
            }
        }
    }
}