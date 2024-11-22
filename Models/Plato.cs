namespace RestoStockDB1.Models
{
    public class Plato
    {
        public int PlatoId { get; set; } // Llave primaria
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? Descripcion { get; set; }

        // Relación muchos-a-muchos con Ingrediente a través de DetallePlato
        public ICollection<DetallePlato> DetallesPlatos { get; set; }
    }
}
