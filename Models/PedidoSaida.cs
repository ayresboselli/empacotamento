namespace Empacotamento.Models
{
    public class PedidoSaida
    {
        public int pedido_id { get; set; }
        public List<Caixa>? caixas { get; set; }
    }
}
