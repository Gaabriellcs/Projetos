using ControleGasto.Dados;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace ControleGasto.Api;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CadastroBanco : ControllerBase
{
    private readonly Dados.DB db;
    private readonly Util _util;
    public CadastroBanco(Dados.DB DB, Util util)
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

            if (db.Bancos.Any(p => p.Nome == descricao))
            {
                return BadRequest(new { message = "Um banco com essa descrição já existe." });
            }

            Bancos banco = new()
            {
                Ativo = true,
                Nome = descricao,
                IdUsuario = usuario
            };

            db.Bancos.Add(banco);
            db.SaveChanges();


            return Ok(new { Message = "Banco cadastrado com sucesso.", BancoId = banco.Id });
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            return BadRequest(new { message = "Ocorreu um erro ao cadastrar o banco." });
        }
    }

    [HttpGet("[action]")]
    public IActionResult ListaBanco()
    {
        try
        {
            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuario nao encontrado" });
            }

            var bancos = db.Bancos.Where(p => p.IdUsuario == usuario);

            return Ok(bancos);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

            return BadRequest(new { message = "Ocorreu um erro ao obter os bancos." });
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
                return BadRequest(new { message = "Usuario nao encontrado" });
            }

            var banco = db.Bancos.FirstOrDefault(p => p.Id == id && p.IdUsuario == usuario);

            if (banco == null)
            {
                return NotFound(new { message = "Banco não encontrado." });
            }

            banco.Ativo = false;

            db.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(new { message = "Ocorreu um erro ao inativar o banco." });
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
                return BadRequest(new { message = "Usuario nao encontrado" });
            }

            var banco = db.Bancos.FirstOrDefault(p => p.Id == id && p.IdUsuario == usuario);

            if (banco == null)
            {
                return NotFound(new { message = "Banco não encontrado." });
            }

            banco.Ativo = true;

            db.SaveChanges();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Ocorreu um erro ao ativar o banco." });
        }
    }




}
