using ControleGasto.Dados;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace ControleGasto.Api;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CadastroFatura : ControllerBase
{

    private readonly Dados.DB db;
    private readonly Util _util;
    public CadastroFatura(Dados.DB DB, Util util)
    {
        db = DB;
        _util = util;
    }

    [HttpPost("[action]/{banco}")]
    public async Task<IActionResult> UploadCsv(IFormFile file, int banco)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "Nenhum arquivo foi enviado." });
        }

        try
        {

            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }


            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            // Configura o mapeamento do CSV para a classe Transacao
            csv.Context.RegisterClassMap<TransacaoMap>();

            // Lê os registros do CSV
            var transacoes = csv.GetRecords<Transacao>().ToList();

            // Converte as transações para faturas
            var faturas = new List<Faturas>();

            foreach (var transacao in transacoes)
            {

                faturas.Add(new Faturas
                {
                    Data = transacao.Data,
                    Descricao = transacao.Descricao,
                    Valor = (decimal)Math.Round(transacao.Valor, 2),
                    IdCategoria = null,
                    IdBanco = banco,
                    IdUsuario = usuario
                });
            }

            // Adiciona as faturas no banco
            db.Faturas.AddRange(faturas);
            await db.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao processar o arquivo: {ex.Message}" });
        }
    }

    [HttpGet("[action]/{item}/{categoria}")]
    public IActionResult CadastrarCategoria(int item, int categoria)
    {
        try
        {

            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }


            var localizado = db.Faturas.Where(p => p.Id == item && p.IdUsuario == usuario).FirstOrDefault();

            if (localizado == null)
            {
                return BadRequest(new { message = "Não foi possivel localizar o item." });
            }

            localizado.IdCategoria = categoria;
            db.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"algo deu errado: {ex.Message}" });
        }

    }

    [HttpGet("action")]
    public IActionResult Listar()
    {
        try
        {

            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }


            var localizados = db.Faturas.Where(p => p.IdUsuario == usuario);
            if (localizados == null)
            {
                return BadRequest(new { message = "Não  foi possivel acessar fatura" });
            }
            return Ok(localizados);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    public class TransacaoMap : ClassMap<Transacao>
    {
        public TransacaoMap()
        {
            Map(t => t.Data).Name("date");
            Map(t => t.Descricao).Name("title");
            Map(t => t.Valor).Name("amount");
            Map(t => t.IdCategoria).Name("category_id").Optional();
            Map(t => t.IdBanco).Name("bank_id").Optional();
        }
    }

    public class Transacao
    {
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int? IdCategoria { get; set; } // Opcional
        public int? IdBanco { get; set; } // Opcional
    }
}

