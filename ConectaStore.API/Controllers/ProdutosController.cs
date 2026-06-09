using System.Data.SqlTypes;
using ConectaStore.API.Data;
using ConectaStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConectaStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = _context.Produtos
            .Include(p => p.Categoria)
            .ToList();
        return Ok(produtos);
    }

    //GET: api/Produtos/5
    [HttpGet("{id}")]
    public ActionResult<Produto> GetProduto(int id)
    {
        var produto = _context.Produtos
            .Where(p => p.Id == id)
            .Include(p => p.Categoria)
            .FirstOrDefault();
        if (produto == null) return NotFound("Não tem o fudido");
        return Ok();
    }


    [HttpGet("categoria/{categoriaId}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int categoriaId)
    {
        var produtos = _context.Produtos
            .Where(p => p.CategoriaId == categoriaId)
            .Include(P => P.Categoria)
            .ToList();
        return Ok(produtos);

    }

    [HttpGet("destaques/{destaqueId}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosDestaque()
    {
        var produtos = _context.Produtos
            .Where(p => p.Destaque) 
            .Include(p => p.Categoria)
            .ToList();

        if (produtos.Count == 0) 
        return NotFound("Não tem nenhum fudido em destaque"); 
        return Ok(produtos);
    }

    [HttpPost]
    public ActionResult<Produto> PostProduto([FromBody] Produto produto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Confira os dados enviados");
        
        if(!_context.Categorias.Any(c => c.Id == produto.CategoriaId))
            return BadRequest("A categoria informada nao existe");

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return CreatedAtAction("GetProduto", new {id = produto.Id}, produto);
    }

    [HttpPut("{id}")]
    public ActionResult PutProduto(int id, [FromBody] Produto produto)
    {
        if (!ModelState.IsValid || id != produto.Id)
            return BadRequest("Confira os dados enviados");

        var oldProduto = _context.Produtos.Find(id);
        if (oldProduto == null)
            return NotFound("Produto não localizado"); 

        if(!CategoriaExistente(produto.CategoriaId))
            return BadRequest("Categoria não localizada");

        if(produto.Nome != null) oldProduto.Nome = produto.Nome;
        if(produto.Descricao != null) oldProduto.Descricao = produto.Descricao;
        oldProduto.Qtde = produto.Qtde;
        oldProduto.ValorCusto = produto.ValorCusto;
        oldProduto.Destaque = produto.Destaque;
        oldProduto.Categoria = produto.Categoria;

        _context.Entry(oldProduto).State= EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduto(int id)
    {
        var produto = _context.Produtos.Find(id);
        if(produto == null) return NotFound("Produto nao localizado");

        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }

    private bool CategoriaExistente(int id)
    {
        return _context.Categorias.Any(c => c.Id == id);
    }

}
