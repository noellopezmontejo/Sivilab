using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Models.Models
{
    public class EventoModel
    {
        public required string EventName { get; set; }
        public required string Description { get; set; }
        public required String EventDate { get; set; }
        //public required string Location { get; set; }
        public required string ImageURL { get; set; }
    }
}