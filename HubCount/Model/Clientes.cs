using System.ComponentModel.DataAnnotations;

namespace HubCount.Model
{
    public class Clientes
    {
        public int Id { get; set; }

        [Key]
        [Required]
        public string Documento { get; set; }

        public List<Pedidos> Pedidos { get; set; }
    }
}
