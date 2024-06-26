using API_ECommerce.Context;
using API_ECommerce.Models;

namespace API_ECommerce.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }      

        public IQueryable<ProdutoModel> GetProdutos()
        {
            return _context.produto;
        }

        public ProdutoModel GetProduto(int id)
        {
            var produto = _context.produto.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
                throw new InvalidOperationException("Produto é null");

            return produto;
        }

        public ProdutoModel Create(ProdutoModel produto)
        {
            if (produto is null)
                throw new InvalidOperationException("Produto é null");

            _context.produto.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public bool Update(ProdutoModel produto)
        {
            if (produto is null)
                throw new InvalidOperationException("Produto é null");

            if (_context.produto.Any(p => p.ProdutoId == produto.ProdutoId))
            {
                _context.produto.Update(produto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var produto = _context.produto.Find(id);

            if (produto is not null)
            {
                _context.produto.Remove(produto);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
