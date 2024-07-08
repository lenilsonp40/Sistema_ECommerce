using API_ECommerce.Context;

namespace API_ECommerce.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepo;
        private IClienteRepository? _clienteRepo;

        public AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
                //if (_produtoRepo == null)
                //{
                //    _produtoRepo = new ProdutoRepository(_context);
                //}
                //return _produtoRepo;
            }
        }
        public IClienteRepository ClienteRepository
        {
            get
            {
                return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context);
            }
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose() //Liberar memória
        {
            _context.Dispose();
        }
    }
}
