using API_ECommerce.Models;

namespace API_ECommerce.Repositories
{
    public interface IProdutoRepository
    {
        IQueryable<ProdutoModel> GetProdutos();
        ProdutoModel GetProduto(int id);
        ProdutoModel CreateProduto(ProdutoModel produto);
        ProdutoModel UpdateProduto(ProdutoModel produto);
        ProdutoModel DeleteProduto(int id);
    }
}
