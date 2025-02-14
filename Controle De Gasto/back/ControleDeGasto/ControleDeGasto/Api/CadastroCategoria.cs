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
                return BadRequest(new { message = "Usuario nao encontrado" });
            }

            if (db.Categorias.Any(p => p.Descricao == descricao && p.IdUsuario == usuario))
            {
                return BadRequest(new { message = "Categoria já cadastrada." });
            }

            Categoria categoria = new()
            {
                Descricao = descricao
            };

            db.Categorias.Add(categoria);

            db.SaveChanges();

            return Ok("Categoria cadastrada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(new { message = "Ocorreu um erro ao cadastrar a categoria." });
        }
    }



}