namespace Sivilab.Models.Models
{
    public class MetaCategoria
    {
        public int IdMeta { get; set; }
        
        // Relación con tu tabla de Categorías (CatCategorias)
        public int CveCategoria { get; set; }
        
        // Oficina o Unidad Operativa
        public int CveOficina { get; set; }
        
        // Temporalidad de la meta
        public int Anio { get; set; }
        public int Mes { get; set; }
        
        // Cantidad o valor de la meta definida para esa Categoría, en el Mes/Año y Oficina específica
        public int MetaPuestos { get; set; }
    }
}
