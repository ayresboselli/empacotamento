namespace Empacotamento.Models
{
    public class Caixa
    {
        public string? caixa_id { get; set; }
        public List<string> produtos { get; set; } = new();
        public string? observacao { get; set; }
    }
}
