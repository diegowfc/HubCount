using HubCount.Data;
using HubCount.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HubCount.Service;

namespace HubCount.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DataContext context;
        private readonly ExcelService excelService;

        public ClienteController(DataContext context, ExcelService excelService)
        {
            this.context = context;
            this.excelService = excelService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para coletar as informações referente ao CLIENTE no arquivo do excel.
        /// </summary>
        /// <param name="excelFile">Instância do arquivo de excel importado</param>
        /// <returns>Mensagem de erro ou sucesso para o usuário</returns>
        [HttpPost("ImportarExcel")]
        public async Task<IActionResult> ImportarExcel(IFormFile excelFile)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var listaClientes = new List<Clientes>();
                var listaPedidos = new List<Pedidos>();
                var listaProdutos = new List<Produtos>();
  

                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var documento = excelService.FormatarDocumento(worksheet.Cells[row, 1].Value?.ToString()?.Trim());
                            var razaoSocial = worksheet.Cells[row, 2].Value?.ToString()?.Trim();
                            var cep = worksheet.Cells[row, 3].Value?.ToString()?.Trim();
                            var produto = worksheet.Cells[row, 4].Value?.ToString()?.Trim();
                            var numPedido = worksheet.Cells[row, 5].Value?.ToString()?.Trim();
                            var data = worksheet.Cells[row, 6].Text;

                            if (string.IsNullOrEmpty(documento) && string.IsNullOrEmpty(razaoSocial) && string.IsNullOrEmpty(cep))
                                break;

                            var parsedDate = excelService.FormataData(data);
                            var produtoID = excelService.GetProdutoId(produto);

                            var cpfCadastrado = listaClientes.FirstOrDefault(c => c.Documento == documento);

                            if (cpfCadastrado == null)
                            {
                                listaClientes.Add(new Clientes
                                {
                                    Documento = documento
                                });
                            }

                            listaPedidos.Add(new Pedidos
                            {
                                NumeroPedido = numPedido,
                                Data = (DateTime)parsedDate,
                                CEP = cep,
                                RazaoSocial = razaoSocial,
                                Documento = documento,
                                ProdutoId = produtoID,
                                DataEntrega = DateTime.MinValue,
                                ValorTotal = 150
                            });
                        }

                    }
                }

                context.Clientes.AddRange(listaClientes);
                context.Pedidos.AddRange(listaPedidos);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
