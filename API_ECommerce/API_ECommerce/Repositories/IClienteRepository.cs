using API_ECommerce.Models;
using API_ECommerce.Pagination;
using X.PagedList;

namespace API_ECommerce.Repositories
{
    public interface IClienteRepository
    {
        Task<IPagedList<ClienteModel>> GetClientesPagination(ClientesParameters clientesParams);
        Task<IPagedList<ClienteModel>> GetClientesFiltroNome(ClientesFiltroNome clientesNomeParams);
        Task< IEnumerable<ClienteModel>> GetClientesAsync();
        Task<ClienteModel> GetClienteID(int id);
        ClienteModel CreateCliente(ClienteModel cliente);
        ClienteModel UpdateCliente(ClienteModel cliente);
        ClienteModel DeleteCliente(int id);


    }
}
