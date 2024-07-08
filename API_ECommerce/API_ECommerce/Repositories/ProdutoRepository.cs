using API_ECommerce.Context;
using API_ECommerce.Models;
using Microsoft.EntityFrameworkCore;

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

        public ProdutoModel CreateProduto(ProdutoModel produto)
        {
            if (produto is null)
                throw new InvalidOperationException("Produto é null");

            _context.produto.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public ProdutoModel UpdateProduto(ProdutoModel produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();
            return produto;


            
        }

        public ProdutoModel DeleteProduto(int id)
        {
            var produto = _context.produto.Find(id);
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            _context.produto.Remove(produto);
            _context.SaveChanges();
            return produto;
            
        }
    }
}
