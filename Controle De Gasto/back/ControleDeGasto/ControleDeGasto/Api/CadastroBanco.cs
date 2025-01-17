using ControleGasto.Dados;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ControleGasto.Api;

[Route("api/[controller]")]
[ApiController]
public class CadastroBanco : ControllerBase
{
    private readonly Dados.DB db;

    public CadastroBanco(Dados.DB DB)
    {
        db = DB;
    }


    [HttpGet("[action]/{descricao}")]
    public IActionResult Cadastrar(string descricao)
    {
        try
        {

            if (db.Bancos.Any(p => p.Nome == descricao))
            {
                return BadRequest("Um banco com essa descrição já existe.");
            }

            Bancos banco = new()
            {
                Ativo = true,
                Nome = descricao
            };

            db.Bancos.Add(banco);
            db.SaveChanges();


            return Ok(new { Message = "Banco cadastrado com sucesso.", BancoId = banco.Id });
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            return BadRequest("Ocorreu um erro ao cadastrar o banco.");
        }
    }

    [HttpGet("[action]")]
    public IActionResult ListaBanco()
    {
        try
        {

            var bancos = db.Bancos.ToList();

            return Ok(bancos);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            return BadRequest("Ocorreu um erro ao obter os bancos.");
        }
    }

    [HttpGet("[action]/{id}")]
    public IActionResult Inativar(int id)
    {
        try
        {

            var banco = db.Bancos.FirstOrDefault(p => p.Id == id);

            if (banco == null)
            {
                return NotFound("Banco não encontrado.");
            }

            banco.Ativo = false;

            db.SaveChanges();

            return Ok("Banco inativado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest("Ocorreu um erro ao inativar o banco.");
        }
    }

    [HttpGet("[action]/{id}")]
    public IActionResult Ativar(int id)
    {
        try
        {
            var banco = db.Bancos.FirstOrDefault(p => p.Id == id);

            if (banco == null)
            {
                return NotFound("Banco não encontrado.");
            }

            banco.Ativo = true;

            db.SaveChanges();

            return Ok("Banco ativado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest("Ocorreu um erro ao ativar o banco.");
        }
    }


}
