using API_ECommerce.Context;
using API_ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace API_ECommerce.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ClienteModel> GetClientes()
        {
            var clientes = _context.cliente.ToList();            
            return clientes;
        }
        public ClienteModel GetClienteID(int id)
        {
            return _context.cliente.FirstOrDefault(c=> c.ClienteID == id);
             
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
    }
}
