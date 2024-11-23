namespace RestoStockDB1.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; } // Llave primaria

        // Llave foránea
        public int ProovedorId { get; set; }
        public Proovedor Proovedor { get; set; }

        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; } 
    }
}
