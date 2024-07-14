using API_ECommerce.DTOs;
using API_ECommerce.DTOs.Mappings;
using API_ECommerce.Migrations;
using API_ECommerce.Models;
using API_ECommerce.Pagination;
using API_ECommerce.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace API_ECommerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtos = await _uof.ProdutoRepository.GetProdutos();

            if (produtos is null)
            {
                return NotFound();
            }

            var produtosDTO = produtos.ToProdutoDTOList();
            return Ok(produtosDTO);
        }


        [HttpGet("{id}", Name = "ObterProduto")]       
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _uof.ProdutoRepository.GetProduto(id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            var produtoDTO = produto.ToProdutoDTO();

            return Ok(produtoDTO);
        }

        //[HttpGet("pagination")]
        //public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery]
        //                               ProdutosParameters produtosParameters)
        //{
        //    var produtos = _uof.ProdutoRepository.GetProdutosPagination(produtosParameters);


        //    var produtosDTO = produtos.ToProdutoDTOList();

        //    return Ok(produtosDTO);
        //}

        private ActionResult<IEnumerable<ProdutoDTO>> ObterPodutos(IPagedList<ProdutoModel> produtos)
        {
            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.PageCount,
                produtos.TotalItemCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

            var produtosDTO = produtos.ToProdutoDTOList();
            return Ok(produtosDTO);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosPagination(produtosParameters);
            return ObterPodutos(produtos);
        }

       

        [HttpGet("filter/preco/pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco
                                                                                    produtosFilterParameters)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosFiltroPreco(produtosFilterParameters);
            return ObterPodutos(produtos);
        }


        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Post(ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
                return BadRequest();

            var produto = produtoDTO.ToProduto();

            var produtonovo = _uof.ProdutoRepository.CreateProduto(produto);
            await _uof.Commit();


            var novoProdutoDTO = produtonovo.ToProdutoDTO();


            return new CreatedAtRouteResult("ObterProduto",
                new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.ProdutoId)
            {
                return BadRequest();
            }

            var produto = produtoDTO.ToProduto();

            var produtoUpdate = _uof.ProdutoRepository.UpdateProduto(produto);
            await _uof.Commit();


            var updateProdutoDTO = produtoUpdate.ToProdutoDTO();

            return Ok(updateProdutoDTO);
            
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletado = await _uof.ProdutoRepository.GetProduto(id);

            if (deletado is null)
            {
                return NotFound($"Produto de id={id} naõ foi encontrado");
            }

            var produtoExcluido = _uof.ProdutoRepository.DeleteProduto(id);
            await _uof.Commit();

            var produtoExcluidoDTO = produtoExcluido.ToProdutoDTO();
            return Ok(produtoExcluidoDTO);
        }
    }
}
