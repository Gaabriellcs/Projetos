using ControleGasto.Dados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleGasto.Api;

[Route("api/[controller]")]
[ApiController]
public class CadastroCategoria : ControllerBase
{
    private readonly Dados.DB db;
    public CadastroCategoria(Dados.DB DB)
    {
        db = DB;
    }

    [HttpGet("[action]/{descricao}")]
    public IActionResult Cadastrar(string descricao)
    {
        try
        {
            if (db.Categorias.Any(p => p.Descricao == descricao))
            {
                return BadRequest("Categoria já cadastrada.");
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
            return BadRequest("Ocorreu um erro ao cadastrar a categoria.");
        }
    }



}