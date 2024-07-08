using API_ECommerce.Models;

namespace API_ECommerce.DTOs.Mappings
{
    public static class ClienteDTOMappingExtensions
    {
        public static ClienteDTO ToClienteDTO(this ClienteModel cliente) 
        {
            if (cliente == null) 
                return null;

            return new ClienteDTO
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Email = cliente.Email
            };
        }

        public static ClienteModel? ToCliente(this ClienteDTO clienteDTO)
        {
            if (clienteDTO is null) return null;

            return new ClienteModel
            {
                ClienteID = clienteDTO.ClienteID,
                Nome = clienteDTO.Nome,
                CPF = clienteDTO.CPF,
                Email = clienteDTO.Email
            };

        }

        public static IEnumerable<ClienteDTO> ToClienteDTOList(this IEnumerable<ClienteModel> clientes)
        {
            if(!clientes.Any() || clientes is null)
            {
                return new List<ClienteDTO>();
            }

            return clientes.Select(cliente => new ClienteDTO
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Email = cliente.Email
            }).ToList();    
        }

    }
}
