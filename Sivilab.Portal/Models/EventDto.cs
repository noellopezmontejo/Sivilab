using System;

namespace Sivilab.Portal.Models
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Lugar { get; set; }
        public bool Estado { get; set; }
    }
}