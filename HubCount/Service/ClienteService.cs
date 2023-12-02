using HubCount.Data;
using HubCount.Model;

namespace HubCount.Service
{
    public class ClienteService
    {
        /// <summary>
        /// Verifica se o documento (chave única) já existe no banco
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public bool VerificaCadastro(string documento, List<Clientes> listaClientes)
        {
            var cpfCadastrado = listaClientes.FirstOrDefault(c => c.Documento == documento);
            if (cpfCadastrado == null) return true; else return false;

        }
    }
}
