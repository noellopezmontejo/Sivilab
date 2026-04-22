namespace Sivilab.PortalEmpleo.Models
{
    public class OfertaModel
    {
        public string OfferName { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Subcategoria { get; set; } = string.Empty;
        public string Educacion { get; set; } = string.Empty;
        public string Contratacion { get; set; } = string.Empty;
        public string Horario { get; set; } = string.Empty;
        public string Modalidad { get; set; } = string.Empty;
        public int Salario { get; set; }
        public List<string> Beneficios { get; set; } = new();
        public string Descripcion { get; set; } = string.Empty;
        public List<string> Requisitos { get; set; } = new();
        public List<string> Funciones { get; set; } = new();
        public List<string> Habilidades { get; set; } = new();
    }
}