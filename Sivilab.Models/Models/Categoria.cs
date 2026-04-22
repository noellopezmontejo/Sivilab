namespace Sivilab.Models.Models
{
    public class Categoria
    {
        public int CveCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public string ImagenUrl { get; set; } = string.Empty;
    }
}
