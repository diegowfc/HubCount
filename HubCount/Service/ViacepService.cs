using HubCount.Model;
using Newtonsoft.Json;

namespace HubCount.Service
{
    public class ViacepService
    {
        private readonly HttpClient _httpClient;

        public ViacepService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> ObterInformacoesCEP(string cep)
        {
            try
            {
                string apiUrl = $"https://viacep.com.br/ws/{cep}/json/";

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    Localidade localidade = JsonConvert.DeserializeObject<Localidade>(jsonContent);
                    PegarRegiaoUF(localidade.Uf);

                    return PegarRegiaoUF(localidade.Uf);
                }
                else
                {
                    throw new Exception($"Erro: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Falha ao localizar o cep. Verifique as informações!", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao processar a requisição.", ex);
            }
        }

        private string PegarRegiaoUF(string uf)
        {
            string UF = uf.ToUpper();

            switch (UF)
            {
                case "AC":
                case "AM":
                case "AP":
                case "PA":
                case "RO":
                case "RR":
                case "TO":
                    return "Norte";

                case "AL":
                case "BA":
                case "CE":
                case "MA":
                case "PB":
                case "PE":
                case "PI":
                case "RN":
                case "SE":
                    return "Nordeste";

                case "DF":
                case "GO":
                case "MT":
                case "MS":
                    return "Centro-Oeste";

                case "ES":
                case "MG":
                case "RJ":
                    return "Sudeste";

                case "PR":
                case "RS":
                case "SC":
                    return "Sul";

                case "SP":
                    return "SP";

                default:
                    return "Unknown";
            }
        }
    }

}
