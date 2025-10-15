using System;
using System.ComponentModel.DataAnnotations;

namespace Sivilab.Models.Models.Eventos
{
    public class Evento
    {

        [Key]
        public string CveEvento { get; set; }

        [Required]
        [StringLength(50)]
        public Guid CveGUID { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaEvento { get; set; }

        [StringLength(50)]
        public string CveUnOperativa { get; set; }

        [StringLength(50)]
        public string CveTipoEvento { get; set; }

        [StringLength(50)]
        public string CveResponsable { get; set; }

        [StringLength(50)]
        public string CveEscuela { get; set; }

        [StringLength(1000)]
        public string Observaciones { get; set; }

        [StringLength(50)]
        public string CveUser { get; set; }

        [StringLength(50)]
        public string CveUserUltimaModificacion { get; set; }

        public int? NoEmpresa { get; set; }

        public int? NoOfertas { get; set; }

        public int? NoPlazas { get; set; }


        public int? AtendidosH { get; set; }

        public int? AtendidosM { get; set; }

        public int? ColocadosM { get; set; }

        public int? ColocadosH { get; set; }

        public int? VinculadosH { get; set; }

        public int? VinculadosM { get; set; }

        [StringLength(50)]
        public string CveEstatus { get; set; }
    }
}