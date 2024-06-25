using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_ECommerce.Models
{
    [Table("ECommerce_TB001_Clientes")]
    public class ClienteModel
    {
        [Key]
        public int ClienteID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }



    }
}
