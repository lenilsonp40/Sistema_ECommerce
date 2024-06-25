using API_ECommerce.Models;

namespace API_ECommerce.Repositories
{
    public interface IClienteRepository
    {
        IEnumerable<ClienteModel> GetClientes();
        ClienteModel GetClienteID(int id);
        ClienteModel CreateCliente(ClienteModel cliente);
        ClienteModel UpdateCliente(ClienteModel cliente);
        ClienteModel DeleteCliente(int id);


    }
}
