using ControleGasto.Dados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleGasto.Api;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CadastroCategoria : ControllerBase
{
    private readonly Dados.DB db;
    private readonly Util _util;
    public CadastroCategoria(Dados.DB DB, Util util)
    {
        db = DB;
        _util = util;
    }

    [HttpGet("[action]/{descricao}")]
    public IActionResult Cadastrar(string descricao)
    {
        try
        {

            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }

            if (db.Categorias.Any(p => p.Descricao == descricao && p.IdUsuario == usuario))
            {
                return BadRequest(new { message = "Categoria já cadastrada." });
            }

            Categoria categoria = new()
            {
                Descricao = descricao,
                IdUsuario = usuario,
                Ativo = true                
            };

            db.Categorias.Add(categoria);

            db.SaveChanges();

            return Ok();
        }
        catch (Exception)
        {

            return BadRequest(new { message = "Ocorreu um erro ao cadastrar a categoria." });
        }
    }

    [HttpGet("[action]")]
    public IActionResult Listar()
    {

        try
        {
            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }

            var categorias = db.Categorias.Where(p => p.IdUsuario == usuario).ToList();

            return Ok(categorias);

        }
        catch (Exception)
        {
            return BadRequest(new { message = "Ocorreu um erro ao Listar a categoria." });
        }

    }


    [HttpGet("[action]/{id}")]
    public IActionResult Ativar(int id)
    {
        try
        {
            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }

            var categoria = db.Categorias.Where(p => p.Id == id && p.IdUsuario == usuario).FirstOrDefault();

            if (categoria == null)
            {
                return NotFound(new { message = "Categoria não encontrado." });
            }

            categoria!.Ativo = true;

            db.SaveChanges();

            return Ok();

        }
        catch (Exception)
        {
            return BadRequest(new { message = "Ocorreu um erro ao Ativar a categoria." });
        }
    }

    [HttpGet("[action]/{id}")]
    public IActionResult Inativar(int id)
    {

        try
        {

            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }

            var categoria = db.Categorias.Where(p => p.Id == id && p.IdUsuario == usuario).FirstOrDefault();


            if (categoria == null)
            {
                return NotFound(new { message = "Categoria não encontrado." });
            }


            categoria.Ativo = false;

            db.SaveChanges();

            return Ok();
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Ocorreu um erro ao Ativar a categoria." });
        }



    }


}
