namespace RestoStockDB1.Models
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; } // Llave primaria
        public string Nombre { get; set; }
        public decimal CantidadDisponible { get; set; }
        public string UnidadMedida { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Relación muchos-a-muchos con Plato a través de DetallePlato
        public ICollection<DetallePlato> DetallesPlatos { get; set; }
    }
}
