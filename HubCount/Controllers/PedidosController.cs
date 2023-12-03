using HubCount.Data;
using HubCount.Model;
using HubCount.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HubCount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : Controller
    {
        private readonly DataContext context;

        public PedidosController(DataContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("ListarVendaPorProduto")]
        public async Task<ActionResult<IEnumerable<object>>> ListarVendaPorProduto()
        {
            var vendaProduto = await context.Pedidos
                .GroupBy(p => p.ProdutoDescricao)
                .Select(g => new
                {
                    ProdutoID = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(vendaProduto);
        }

        [HttpGet("ListarVendaPorRegiao")]
        public async Task<ActionResult<IEnumerable<object>>> ListarVendaPorRegiao()
        {
            var vendaRegiao = await context.Pedidos
                .GroupBy(p => p.Regiao)
                .Select(g => new
                {
                    Regiao = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(vendaRegiao);
        }

        [HttpGet("ListarVendas")]
        public async Task<ActionResult<IEnumerable<object>>> ListarVendas()
        {
            var listaVendas = await context.Pedidos
                .Select(g => new
                {
                    Nome = g.RazaoSocial,
                    Produto = g.ProdutoDescricao,
                    Valor = g.ValorTotal,
                    Data= g.DataEntrega
                })
                .ToListAsync();

            return Ok(listaVendas);
        }

    }
}
