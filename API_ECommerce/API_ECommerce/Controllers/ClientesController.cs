using API_ECommerce.Context;
using API_ECommerce.DTOs;
using API_ECommerce.DTOs.Mappings;
using API_ECommerce.Models;
using API_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_ECommerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
       

        public ClientesController(IUnitOfWork uof)
        {

           _uof = uof;
        }       

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> Get()
        {
            var clientes = _uof.ClienteRepository.GetClientes();

            if (clientes == null)
                return NotFound("Não existem clientes...");

           

            var clientesDTO = clientes.ToClienteDTOList();
            return Ok(clientesDTO);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<ClienteDTO> Get(int id)
        {

            var cliente = _uof.ClienteRepository.GetClienteID(id);
            if (cliente == null) 
            {
                return NotFound($"Cliente com id= {id} não encontrada!");
            }

           

            var clienteDTO = cliente.ToClienteDTO();
            return Ok(clienteDTO);
        }

        [HttpPost]
        public ActionResult<ClienteDTO> Post(ClienteDTO clienteDTO)
        {
            if (clienteDTO is null)
            {               
                return BadRequest("Dados inválidos");
            }            

            var cliente = clienteDTO.ToCliente();

            var clienteCriado = _uof.ClienteRepository.CreateCliente(cliente);
            _uof.Commit();
            

            var novoClienteDTO = clienteCriado.ToClienteDTO();

            return new CreatedAtRouteResult("ObterCategoria", new { id = novoClienteDTO.ClienteID }, novoClienteDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ClienteDTO> Put(int id, ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.ClienteID)
            {
                
                return BadRequest("Dados inválidos");
            }
            

            var cliente = clienteDTO.ToCliente();

            var clienteUpdate = _uof.ClienteRepository.UpdateCliente(cliente);
            _uof.Commit();


            var updateClienteDTO = clienteUpdate.ToClienteDTO();

            return Ok(updateClienteDTO);

        }

        [HttpDelete("{id:int}")]
        public ActionResult<ClienteDTO> Delete(int id)
        {
            var cliente = _uof.ClienteRepository.GetClienteID(id);


            if (cliente is null)
            {                
                return NotFound($"Categoria com id={id} não encontrada...");
            }

            var clienteExcluido = _uof.ClienteRepository.DeleteCliente(id);
            _uof.Commit();


            var clienteExcluidoDTO = clienteExcluido.ToClienteDTO();
            return Ok(clienteExcluidoDTO);

        }
    }
}
