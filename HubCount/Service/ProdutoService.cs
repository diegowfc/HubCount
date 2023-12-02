using HubCount.Data;
using static HubCount.Service.PedidosService;
using System.Runtime.ConstrainedExecution;
using HubCount.Service;
using HubCount.Model;
using System.Globalization;

namespace HubCount.Service
{
    public class ProdutoService
    {
        private readonly DataContext context;
        private readonly ViacepService viacepService;

        public ProdutoService(DataContext context, ViacepService viacepService)
        {
            this.context = context;
            this.viacepService = viacepService;
        }

        public async Task<decimal> CalcularValorTotal(string produto, string cep)
        {
            var regiao = await viacepService.ObterInformacoesCEP(cep);
            var objetoProduto = context.Produtos.FirstOrDefault(p => p.Nome == produto);

            if (regiao == "SP")
            {
                return objetoProduto.PrecoUnitario;
            }

            switch (regiao)
            {
                case "Norte":
                case "Nordeste":
                    return CalcularFrete(0.30, objetoProduto.PrecoUnitario);

                case "Sudeste":
                    return CalcularFrete(0.10, objetoProduto.PrecoUnitario);

                case "Centro-Oeste":
                case "Sul":
                    return CalcularFrete(0.20, objetoProduto.PrecoUnitario);

                default:
                    throw new ArgumentException("Região desconhecida");
            }

            return 1;
        }

        private decimal CalcularFrete(double taxa, decimal precoUnitario)
        {
            var precoTotal = precoUnitario;
            precoTotal += precoUnitario * (decimal)taxa;
            return precoTotal;
        }


    }
}
