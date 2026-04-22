using System;

namespace Sivilab.Models.Models
{
    public class Vacante
    {
        public int VacanteId { get; set; }
        public string? Empresa { get; set; }
        public string? Puesto { get; set; }
        public string? Descripcion { get; set; }
        public string? Ubicacion { get; set; }
        public string? Municipio { get; set; }
        public decimal? SalarioMin { get; set; }
        public decimal? SalarioMax { get; set; }
        public string? TipoContrato { get; set; }
        public string? Jornada { get; set; }
        public int? VacantesDisponibles { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime? FechaVigencia { get; set; }
        public string? Requisitos { get; set; }
        public string? Beneficios { get; set; }
        public int Estatus { get; set; }
        
        // Propiedades calculadas
        public string SalarioRango => $"${SalarioMin:N0} - ${SalarioMax:N0} MXN/mes";
        public string DiasPublicado => $"{(DateTime.Now - FechaPublicacion).Days} días";
        public bool EsNueva => (DateTime.Now - FechaPublicacion).Days <= 7;

        // Propiedades del nuevo modelo
        public string? FolioSIISNE { get; set; }
        public int FolioSIVILAB { get; set; } // Mapeado de CveVacante
        public string? PuestoOfrecido { get; set; }
        public int? NoPlazas { get; set; }
        public string? Salario { get; set; }
        public int? CveEscolaridadMin { get; set; }
        public string? Escolaridad { get; set; }
        public string? Sexo { get; set; }
        public int? Edad1 { get; set; }
        public int? Edad2 { get; set; }
        public string? Experiencia { get; set; }
        public string? HorarioDe { get; set; }
        public string? HorarioA { get; set; }
        public string? Actividades { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? TipoVacante { get; set; }
        public string? UnidadEnlace { get; set; }
        public string? OtrasPrest { get; set; }
        
        // Nuevo campo para la relación con CatCategorias
        public int? CveCategoria { get; set; }
        public string? NombreCategoria { get; set; }

        // Regla de Negocio Centralizada: Solo se muestra si Estatus es 1 o 2, y su Vigencia no ha expirado
        public bool EsVisibleEnPortal => 
            (Estatus == 1 || Estatus == 2) && 
            (!FechaVigencia.HasValue || FechaVigencia.Value.Date >= DateTime.Today);
      
    }
}