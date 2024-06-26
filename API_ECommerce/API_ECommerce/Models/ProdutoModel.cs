using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_ECommerce.Models
{
    [Table("ECommerce_TB002_Produtos")]
    public class ProdutoModel
    {
        [Key]
        public int ProdutoId { get; set; }       
        public string? Nome { get; set; }        
        public string? Descricao { get; set; }        
        public decimal Preco { get; set; }       
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
