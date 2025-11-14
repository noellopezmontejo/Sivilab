using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Models.Models
{
    public class CandidatoCrp
    {
        public int CandidatoId { get; set; }
        public int FolioSIISNE { get; set; }
        public string? Curp { get; set; }
        public string? Nombre { get; set; }
        public string? Paterno { get; set; }
        public string? Materno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? CveGenero { get; set; }
        public int CveNacionalidad { get; set; }
        public int CveEstado { get; set; }
        public int CveMunicipio { get; set; }
        public int CveColonia { get; set; }
        /*
        public string? EstadoNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
        public string? EstadoResidencia { get; set; }
        public string? MunicipioResidencia { get; set; }*/
        public string? CodigoPostal { get; set; }
        public string? Calle { get; set; }
        public string Localizacion { get; set; }
        public string? NumeroExterior { get; set; }
        public string? NumeroInterior { get; set; }
        //public string? Colonia { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Ocupacion { get; set; }
        public int CveOcupacion { get; set; }
        public int CveSubOcupacion { get; set; }
        public string? EstadoCivil { get; set; }
        public string? RFC { get; set; }
        public string? NumeroSeguroSocial { get; set; }
        public string? FechaRegistro { get; set; }
        public string? FechaActualizacion { get; set; }
        public string? UsuarioRegistro { get; set; }


        // Propiedad calculada que concatena ApellidoPaterno, ApellidoMaterno y Nombre.
        // Es de solo lectura para que Dapper/ORMs no intenten mapearla contra una columna de la BD.
        public string NombreCompleto
        {
            get
            {
                var partes = new[] { Paterno, Materno, Nombre }
                             .Where(p => !string.IsNullOrWhiteSpace(p));
                return string.Join(" ", partes);
            }
        }
    }


}
