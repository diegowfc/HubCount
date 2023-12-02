using HubCount.Data;
using HubCount.Model;
using System.Globalization;

namespace HubCount.Service
{
    public class ExcelService
    {
        private readonly DataContext context;

        public ExcelService(DataContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Formata a string data para DateTime para inserir no banco.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public DateTime? FormataData(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                if (DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    throw new FormatException("Data inválida.");
                }
            }
            return null;
        }

        /// <summary>
        /// Método para formatar o documento, deixando apenas números.
        /// </summary>
        /// <param name="documento">String do documento</param>
        /// <returns></returns>
        public string FormatarDocumento(string documento)
        {
            return documento?.Replace(".", "").Replace("-", "");
        }

        /// <summary>
        /// Recupera o ID do produto
        /// </summary>
        /// <param name="produto">Nome do produto</param>
        /// <returns>Id do produto/returns>
        public int GetProdutoId(string produto)
        {
            var objetoProduto = context.Produtos.FirstOrDefault(p => p.Nome == produto);

            return objetoProduto.Id;
        }
    }
}
