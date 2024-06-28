using API_ECommerce.Context;
using API_ECommerce.DTOs;
using API_ECommerce.Models;
using API_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_ECommerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {     
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> Get()
        {
            var clientes = _clienteRepository.GetClientes();

            if (clientes == null)
                return NotFound("Não existem clientes...");

            var clientesDTO = new List<ClienteDTO>();
            foreach (var cliente in clientes)
            {
                var clienteDTO = new ClienteDTO
                {
                    ClienteID = cliente.ClienteID,
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    Email = cliente.Email
                };
                clientesDTO.Add(clienteDTO);
            }
            return Ok(clientes);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<ClienteDTO> Get(int id)
        {
            var cliente = _clienteRepository.GetClienteID(id);
            if (cliente == null) 
            {
                return NotFound($"Cliente com id= {id} não encontrada!");
            }

            var clienteDTO = new ClienteDTO()
            {
                ClienteID = cliente.ClienteID,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Email = cliente.Email
            };
            return Ok(clienteDTO);
        }

        [HttpPost]
        public ActionResult<ClienteDTO> Post(ClienteDTO clienteDTO)
        {
            if (clienteDTO is null)
            {               
                return BadRequest("Dados inválidos");
            }

            var cliente = new ClienteModel()
            {
                ClienteID = clienteDTO.ClienteID,
                Nome = clienteDTO.Nome,
                CPF = clienteDTO.CPF,
                Email = clienteDTO.Email
            };

            var clienteCriado = _clienteRepository.CreateCliente(cliente);

            var novoClienteDTO = new ClienteDTO()
            {
                ClienteID = clienteCriado.ClienteID,
                Nome = clienteCriado.Nome,
                CPF = clienteCriado.CPF,
                Email = clienteCriado.Email
            };

            return new CreatedAtRouteResult("ObterCategoria", new { id = novoClienteDTO.ClienteID }, novoClienteDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ClienteDTO> Put(int id, ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.ClienteID)
            {
                
                return BadRequest("Dados inválidos");
            }

            var cliente = new ClienteModel()
            {
                ClienteID = clienteDTO.ClienteID,
                Nome = clienteDTO.Nome,
                CPF = clienteDTO.CPF,
                Email = clienteDTO.Email
            };

           var clienteUpdate = _clienteRepository.UpdateCliente(cliente);

            var updateClienteDTO = new ClienteDTO()
            {
                ClienteID = clienteUpdate.ClienteID,
                Nome = clienteUpdate.Nome,
                CPF = clienteUpdate.CPF,
                Email = clienteUpdate.Email
            };

            return Ok(updateClienteDTO);

        }

        [HttpDelete("{id:int}")]
        public ActionResult<ClienteDTO> Delete(int id)
        {
            var cliente = _clienteRepository.GetClienteID(id);

            if (cliente is null)
            {                
                return NotFound($"Categoria com id={id} não encontrada...");
            }

            var clienteExcluido = _clienteRepository.DeleteCliente(id);

            var clienteExcluidoDTO = new ClienteDTO()
            {
                ClienteID = clienteExcluido.ClienteID,
                Nome = clienteExcluido.Nome,
                CPF = clienteExcluido.CPF,
                Email = clienteExcluido.Email

            };
            return Ok(clienteExcluidoDTO);

        }
    }
}
