namespace Sivilab.Portal.Models
{
    public class Evento
    {
        public int vigente { get; set; }
        public string titulo { get; set; } = string.Empty;
        public string imagen { get; set; } = string.Empty;
        public string fecha { get; set; } = string.Empty;
        public string lugar { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public string liga { get; set; } = string.Empty;
    }
}