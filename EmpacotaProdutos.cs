
using Empacotamento.Models;

namespace Empacotamento
{
    public class EmpacotaProdutos
    {
        public static List<Caixa> Empacotar(Pedido pedido)
        {
            var caixas = new List<Dimensao>
            {
                new() { largura = 30, altura = 40, comprimento = 80 },
                new() { largura = 80, altura = 50, comprimento = 40 },
                new() { largura = 50, altura = 80, comprimento = 60 },
            };

            List<Caixa> container = new();
            var caixa_tmp = new List<Produto>();
            int cnt = 0;
            while (pedido.produtos?.Count > 0)
            {
                if(pedido.produtos[cnt].dimensoes.Cubagem > caixas.Last().Cubagem)
                {
                    container.Add(
                        new () {
                            caixa_id = null,
                            produtos = [pedido.produtos[cnt].produto_id],
                            observacao = "Produto não cabe em nenhuma caixa disponível."
                        }
                    );
                    pedido.produtos.RemoveAt(cnt);
                    continue;
                }

                // verifica se o produto cabe na maior caixa
                if (pedido.produtos.Count > cnt && TamanhoCaixa(caixa_tmp) + pedido.produtos[cnt].dimensoes.Cubagem <= caixas.Last().Cubagem)
                {
                    caixa_tmp.Add(pedido.produtos[cnt]);
                    pedido.produtos.RemoveAt(cnt);
                    cnt--;
                }

                cnt++;

                // Verifica se chegou ao fim da lista de produtos
                if (cnt >= pedido.produtos.Count)
                {
                    // procura a menor caixa que caiba os produtos
                    for (int i = 0; i < caixas.Count; i++)
                    {
                        if (caixas[i].Cubagem > TamanhoCaixa(caixa_tmp))
                        {
                            var caixa = new Caixa { caixa_id = $"Caixa {i + 1}" };
                            for(int j = 0; j < caixa_tmp.Count; j++)
                            {
                                caixa.produtos.Add(caixa_tmp[j].produto_id);
                            }
                            caixa_tmp.Clear();

                            container.Add(caixa);

                            break;
                        }
                    }

                    cnt = 0;
                }
            }

            return container;
        }

        static int TamanhoCaixa(List<Produto> caixa)
        {
            int tamanho = 0;
            for (int i = 0; i < caixa.Count; i++)
            {
                tamanho += caixa[i].dimensoes.Cubagem;
            }

            return tamanho;
        }
    }
}
