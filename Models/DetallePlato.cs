namespace RestoStockDB1.Models
{
    public class DetallePlato
    {
        public int DetallePlatoId { get; set; } // Llave primaria

        // Llaves foráneas
        public int PlatoId { get; set; }
        public Plato Plato { get; set; }

        public int ingredienteId { get; set; }
        public Ingrediente Ingrediente { get; set; }

        public decimal Cantidad { get; set; }
    }
}
