namespace API_ECommerce.Repositories
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        IClienteRepository ClienteRepository { get; }
        void Commit();
    }
}
