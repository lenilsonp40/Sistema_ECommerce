using API_ECommerce.Migrations;
using API_ECommerce.Models;

namespace API_ECommerce.DTOs.Mappings
{
    public static class ProdutoDTOMappingExtensions
    {
        public static IEnumerable<ProdutoDTO> ToProdutoDTOList(this IEnumerable<ProdutoModel> produtos)
        {
            if (!produtos.Any() || produtos is null)
            {
                return new List<ProdutoDTO>();
            }

            return produtos.Select(produtos => new ProdutoDTO
            {
                ProdutoId = produtos.ProdutoId,
                Nome = produtos.Nome,
                Descricao = produtos.Descricao,
                Preco = produtos.Preco,
                ImagemUrl = produtos.ImagemUrl,
                Estoque = produtos.Estoque,
                DataCadastro = produtos.DataCadastro
                
            }).ToList();
        }
        public static ProdutoDTO ToProdutoDTO(this ProdutoModel produto)
        {
            if (produto == null)
                return null;

            return new ProdutoDTO
            {
                ProdutoId = produto.ProdutoId,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque,
                DataCadastro = produto.DataCadastro
            };
        }

        public static ProdutoModel? ToProduto(this ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null) return null;

            return new ProdutoModel
            {
                ProdutoId = produtoDTO.ProdutoId,
                Nome = produtoDTO.Nome,
                Descricao = produtoDTO.Descricao,
                Preco = produtoDTO.Preco,
                ImagemUrl = produtoDTO.ImagemUrl,
                Estoque = produtoDTO.Estoque,
                DataCadastro = produtoDTO.DataCadastro
            };

        }
    }
}
