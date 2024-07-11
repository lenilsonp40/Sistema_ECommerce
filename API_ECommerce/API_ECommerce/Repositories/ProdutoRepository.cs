using API_ECommerce.Context;
using API_ECommerce.Migrations;
using API_ECommerce.Models;
using API_ECommerce.Pagination;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace API_ECommerce.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;
        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }      

        public async Task<IEnumerable<ProdutoModel>> GetProdutos()
        {
            var produtos = await _context.produto.ToListAsync();
            return produtos;
        }

        public async Task<ProdutoModel> GetProduto(int id)
        {
            var produto = await _context.produto.FirstOrDefaultAsync(p => p.ProdutoId == id);

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

        //public IEnumerable<ProdutoModel> GetProdutosPagination(ProdutosParameters produtosParams)
        //{
        //    return GetProdutos()
        //      .OrderBy(p => p.Nome)
        //      .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
        //      .Take(produtosParams.PageSize).ToList();

        //}

        public async Task<IPagedList<ProdutoModel>> GetProdutosPagination(ProdutosParameters produtosParams)
        {
            //var produtos = GetProdutos().OrderBy(p => p.ProdutoId).AsQueryable();
            var produtos = await GetProdutos();

            var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();


            var resultados = await produtosOrdenados.ToPagedListAsync(produtosParams.PageNumber, produtosParams.PageSize);

            return resultados;
        }

        public async Task<IPagedList<ProdutoModel>> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
        {
            var produtos = await GetProdutos();

            if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
            {
                if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
            }
            var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

            return produtosFiltrados;
        }
    }
}
