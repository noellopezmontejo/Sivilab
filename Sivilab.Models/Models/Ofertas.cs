using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Models.Models
{
    public class OfertaModel
    {
        public required string  OfferName { get; set; }
        public required string  Description { get; set; }
        public required string  ImageURL { get; set; }
        public int              Experience { get; set; }
        public double           Salary { get; set; }
    }
}