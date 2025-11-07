using Microsoft.AspNetCore.Mvc;
using Sivilab.Data.Repositories;
using Sivilab.Models.Models;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatoCrpController : ControllerBase
    {
        private readonly ICandidatoCrpRepository _repository;

        public CandidatoCrpController(ICandidatoCrpRepository repository)
        {
            _repository = repository;
        }

        // GET: api/CandidatoCrp
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.ObtenerTodos();
            return Ok(items);
        }

        // GET: api/CandidatoCrp/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.ObtenerPorId(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // GET: api/CandidatoCrp/curp/{curp}
        [HttpGet("curp/{curp}")]
        public async Task<IActionResult> GetByCurp(string curp)
        {
            if (string.IsNullOrWhiteSpace(curp)) return BadRequest("CURP inválida.");

            var item = await _repository.ObtenerPorCurp(curp);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/CandidatoCrp
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CandidatoCrp model)
        {
            if (model == null) return BadRequest("Modelo inválido.");

            var newId = await _repository.AgregarCandidato(model);

            // Devuelve Created con la ruta al GET por Id
            return CreatedAtAction(nameof(GetById), new { id = newId }, model);
        }

        // PUT: api/CandidatoCrp/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CandidatoCrp model)
        {
            if (model == null) return BadRequest("Modelo inválido.");
            if (model.CandidatoId != id) return BadRequest("El id de la ruta debe coincidir con el id del modelo.");

            var updated = await _repository.ActualizarCandidato(model);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/CandidatoCrp/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.EliminarCandidato(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}