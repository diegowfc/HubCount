using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using HubCount.Service;
using HubCount.Data;
using HubCount.Model;

namespace HubCount.Service
{
    public class PedidosService
    {
        private readonly ViacepService viacepService;

        public PedidosService(ViacepService viacepService)
        {
            this.viacepService = viacepService;
        }

        public async Task<DateTime> CalcularTempoEntrega(string cep, string data)
        {
            var regiao = await viacepService.ObterInformacoesCEP(cep);

            if (regiao == "SP")
            {
                return DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            switch (regiao)
            {
                case "Norte":
                case "Nordeste":
                    return CalcularDataEntrega(data, 10);

                case "Sudeste":
                    return CalcularDataEntrega(data, 1);

                case "Centro-Oeste":
                case "Sul":
                    return CalcularDataEntrega(data, 5);

                default:
                    throw new ArgumentException("Região desconhecida");
            }
        }

        private DateTime CalcularDataEntrega(string data, int diasAdicionais)
        {
            if (DateTime.TryParse(data, out DateTime dataEntrega))
            {
                // Add specified number of days to the original date
                dataEntrega = dataEntrega.AddDays(diasAdicionais);

                return dataEntrega;
            }
            else
            {
                // Handle invalid date format
                throw new ArgumentException("Formato inválido");
            }
        }
    }
}
