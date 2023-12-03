using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HubCount.Model
{
    public class Pedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string NumeroPedido { get; set; }

        public DateTime Data { get; set; }

        public decimal ValorTotal { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataEntrega { get; set; }

        public string RazaoSocial { get; set; }

        public string CEP { get; set; }
        
        public string Regiao { get; set; }

        [ForeignKey("Cliente")]
        public string Documento { get; set; }

        [ForeignKey("ProdutoId")]
        public int ProdutoId { get; set; }

        public string ProdutoDescricao { get; set; }

        public Produtos Produto { get; set; }
    }
}
