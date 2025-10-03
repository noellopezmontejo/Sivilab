using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Models.Models.Eventos
{
    public class Inscripcion
    {
        public int InscripcionId { get; set; }
        public int CandidatoId { get; set; }
        public int EventId { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}
