using API_ECommerce.Context;
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
        public ActionResult<IEnumerable<ClienteModel>> Get()
        {
            var clientes = _clienteRepository.GetClientes();
            return Ok(clientes);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<ClienteModel> Get(int id)
        {
            var cliente = _clienteRepository.GetClienteID(id);
            if (cliente == null) 
            {
                return NotFound($"Cliente com id= {id} não encontrada!");
            }
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult Post(ClienteModel cliente)
        {
            if (cliente is null)
            {               
                return BadRequest("Dados inválidos");
            }

            var clienteCriado = _clienteRepository.CreateCliente(cliente);

            return new CreatedAtRouteResult("ObterCategoria", new { id = clienteCriado.ClienteID }, clienteCriado);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ClienteModel cliente)
        {
            if (id != cliente.ClienteID)
            {
                
                return BadRequest("Dados inválidos");
            }

            _clienteRepository.UpdateCliente(cliente);
            return Ok(cliente);

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _clienteRepository.DeleteCliente(id);

            if (categoria is null)
            {                
                return NotFound($"Categoria com id={id} não encontrada...");
            }

            var clienteExcluido = _clienteRepository.DeleteCliente(id);
            return Ok(clienteExcluido);

        }
    }
}
