using API_ECommerce.DTOs;
using API_ECommerce.DTOs.Mappings;
using API_ECommerce.Migrations;
using API_ECommerce.Models;
using API_ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _uof.ProdutoRepository.GetProdutos().ToList();

            if (produtos is null)
            {
                return NotFound();
            }

            var produtosDTO = produtos.ToProdutoDTOList();
            return Ok(produtosDTO);
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _uof.ProdutoRepository.GetProduto(id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            var produtoDTO = produto.ToProdutoDTO();

            return Ok(produtoDTO);
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
                return BadRequest();

            var produto = produtoDTO.ToProduto();

            var produtonovo = _uof.ProdutoRepository.CreateProduto(produto);

            var novoProdutoDTO = produtonovo.ToProdutoDTO();


            return new CreatedAtRouteResult("ObterProduto",
                new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.ProdutoId)
            {
                return BadRequest();
            }

            var produto = produtoDTO.ToProduto();

            var produtoUpdate = _uof.ProdutoRepository.UpdateProduto(produto);

            var updateProdutoDTO = produtoUpdate.ToProdutoDTO();

            return Ok(updateProdutoDTO);
            
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var deletado = _uof.ProdutoRepository.GetProduto(id);

            if (deletado is null)
            {
                return NotFound($"Produto de id={id} naõ foi encontrado");
            }

            var produtoExcluido = _uof.ProdutoRepository.DeleteProduto(id);

            var produtoExcluidoDTO = produtoExcluido.ToProdutoDTO();
            return Ok(produtoExcluidoDTO);
        }
    }
}
