using System.Text.Json.Serialization;

namespace API_ECommerce.DTOs
{
    public class ClienteDTO
    {
        
        public int ClienteID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
    }
}
