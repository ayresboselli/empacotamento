namespace Empacotamento.Models
{
    public class Dimensao()
    {
        public int largura { get; set; }
        public int altura { get; set; }
        public int comprimento { get; set; }
        public int Cubagem { get { return largura * altura * comprimento; } }
    }
}
