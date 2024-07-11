using API_ECommerce.Migrations;
using API_ECommerce.Models;
using API_ECommerce.Pagination;
using X.PagedList;

namespace API_ECommerce.Repositories
{
    public interface IProdutoRepository
    {
        Task<IPagedList<ProdutoModel>> GetProdutosPagination(ProdutosParameters produtosParams);
        Task<IPagedList<ProdutoModel>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams);
        Task<IEnumerable<ProdutoModel>> GetProdutos();
        Task<ProdutoModel> GetProduto(int id);
        ProdutoModel CreateProduto(ProdutoModel produto);
        ProdutoModel UpdateProduto(ProdutoModel produto);
        ProdutoModel DeleteProduto(int id);
    }
}
