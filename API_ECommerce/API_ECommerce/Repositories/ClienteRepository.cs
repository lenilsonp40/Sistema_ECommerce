using API_ECommerce.Context;
using API_ECommerce.Models;
using API_ECommerce.Pagination;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace API_ECommerce.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClienteModel>> GetClientesAsync()
        {
            var clientes = await _context.cliente.ToListAsync();            
            return clientes;
        }
        public async Task<ClienteModel> GetClienteID(int id)
        {
            return await _context.cliente.FirstOrDefaultAsync(c=> c.ClienteID == id);
             
        }
        public ClienteModel CreateCliente(ClienteModel cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));
            _context.cliente.Add(cliente);
            _context.SaveChanges();
            return cliente;
        }
        public ClienteModel UpdateCliente(ClienteModel cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));
            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();
            return cliente;
        }
        public ClienteModel DeleteCliente(int id)
        {
            var cliente = _context.cliente.Find(id);
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            _context.cliente.Remove(cliente);
            _context.SaveChanges();
            return cliente;


        }
        public async Task<IPagedList<ClienteModel>> GetClientesPagination(ClientesParameters clientesParams)
        {
            
            var clientes = await GetClientesAsync();

            var clientesOrdenados = clientes.OrderBy(p => p.ClienteID).AsQueryable();

            //var resultado = IPagedList<ClienteModel>.ToPagedList(clientesOrdenados,
            //           clientesParams.PageNumber, clientesParams.PageSize);

            var resultado = await clientesOrdenados.ToPagedListAsync(clientesParams.PageNumber, clientesParams.PageSize);

            return resultado;
        }

        public async Task<IPagedList<ClienteModel>> GetClientesFiltroNome(ClientesFiltroNome clientesNomeParams)
        {
            var clientes = await GetClientesAsync();

            if (!string.IsNullOrEmpty(clientesNomeParams.Nome))
            {
                var nomeFiltro = clientesNomeParams.Nome.ToLower();
                clientes = clientes.Where(c => c.Nome.ToLower().Contains(nomeFiltro));
            }

            //var clientesFiltradas = PagedList<ClienteModel>.ToPagedList(clientes.AsQueryable(),
            //                           clientesNomeParams.PageNumber, clientesNomeParams.PageSize);

            var clientesFiltrados = await clientes.ToPagedListAsync(clientesNomeParams.PageNumber, clientesNomeParams.PageSize);

            return clientesFiltrados;
        }

    }
}
