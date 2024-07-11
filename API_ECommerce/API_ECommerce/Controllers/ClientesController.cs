using API_ECommerce.Context;
using API_ECommerce.DTOs;
using API_ECommerce.DTOs.Mappings;
using API_ECommerce.Models;
using API_ECommerce.Pagination;
using API_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

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

        private ActionResult<IEnumerable<ClienteDTO>> ObterClientes(IPagedList<ClienteModel> clientes)
        {
            var metadata = new
            {
                clientes.Count,
                clientes.PageSize,
                clientes.PageCount,
                clientes.TotalItemCount,
                clientes.HasNextPage,
                clientes.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var clientesDTO = clientes.ToClienteDTOList();
            return Ok(clientesDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
        {
            var clientes = await _uof.ClienteRepository.GetClientesAsync();

            if (clientes == null)
                return NotFound("Não existem clientes...");

           

            var clientesDTO = clientes.ToClienteDTOList();
            return Ok(clientesDTO);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get([FromQuery] ClientesParameters clientesParameters)
        {
            var clientes = await _uof.ClienteRepository.GetClientesPagination(clientesParameters);

            return ObterClientes(clientes);

        }

        [HttpGet("filter/nome/pagination")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetNomesFiltrados([FromQuery] ClientesFiltroNome clientesFiltro)
        {
            var clientesFiltrados = await _uof.ClienteRepository.GetClientesFiltroNome(clientesFiltro);

            return ObterClientes(clientesFiltrados);

        }       

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {

            var cliente = await _uof.ClienteRepository.GetClienteID(id);
            if (cliente == null) 
            {
                return NotFound($"Cliente com id= {id} não encontrada!");
            }

           

            var clienteDTO = cliente.ToClienteDTO();
            return Ok(clienteDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Post(ClienteDTO clienteDTO)
        {
            if (clienteDTO is null)
            {               
                return BadRequest("Dados inválidos");
            }            

            var cliente = clienteDTO.ToCliente();

            var clienteCriado = _uof.ClienteRepository.CreateCliente(cliente);
             await _uof.Commit();
            

            var novoClienteDTO = clienteCriado.ToClienteDTO();

            return new CreatedAtRouteResult("ObterCategoria", new { id = novoClienteDTO.ClienteID }, novoClienteDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> Put(int id, ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.ClienteID)
            {
                
                return BadRequest("Dados inválidos");
            }
            

            var cliente = clienteDTO.ToCliente();

            var clienteUpdate = _uof.ClienteRepository.UpdateCliente(cliente);
            await _uof.Commit();


            var updateClienteDTO = clienteUpdate.ToClienteDTO();

            return Ok(updateClienteDTO);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> Delete(int id)
        {
            var cliente =  await _uof.ClienteRepository.GetClienteID(id);


            if (cliente is null)
            {                
                return NotFound($"Categoria com id={id} não encontrada...");
            }

            var clienteExcluido = _uof.ClienteRepository.DeleteCliente(id);
            await _uof.Commit();


            var clienteExcluidoDTO = clienteExcluido.ToClienteDTO();
            return Ok(clienteExcluidoDTO);

        }
    }
}
