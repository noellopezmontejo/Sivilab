using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sivilab.API.Controllers
{
    public class UsuarioCrp
    {
        public string Curp { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string EstadoNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string EstadoResidencia { get; set; }
        public string MunicipioResidencia { get; set; }
        public string CodigoPostal { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Ocupacion { get; set; }
        public string EstadoCivil { get; set; }
        public string RFC { get; set; }
        public string NumeroSeguroSocial { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaActualizacion { get; set; }
        public string UsuarioRegistro { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioCurpController : ControllerBase
    {
        [HttpGet("{curp}")]
        public ActionResult<UsuarioCrp> GetUsuarioByCurp(string curp)
        {
            // Aquí iría la lógica para obtener el usuario por CURP
            // Por ahora, retornamos un ejemplo estático
            var usuario = new UsuarioCrp
            {
                Curp = "1234567890",
                Nombre = "Juan",
                ApellidoPaterno = "Perez",
                ApellidoMaterno = "Lopez",
                FechaNacimiento = new DateTime(1990, 1, 1),
                Genero = "Masculino",
                EstadoNacimiento = "Jalisco",
                Nacionalidad = "Mexicana",
                EstadoResidencia = "Jalisco",
                MunicipioResidencia = "Guadalajara",
                CodigoPostal = "44100",
                Calle = "Avenida Siempre Viva",
                NumeroExterior = "123",
                NumeroInterior = "A",
                Colonia = "Centro",
                Telefono = "3333333333",
                CorreoElectronico = "JuanLopez@gmail.com",

            };
            return Ok(usuario);
        }
    } 
}
