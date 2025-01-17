using ControleGasto.Dados;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ControleGasto.Api;

[Route("api/[controller]")]
[ApiController]
public class CadastroFatura : ControllerBase
{

    private readonly Dados.DB db;
    public CadastroFatura(Dados.DB DB)
    {
        db = DB;
    }

    [HttpPost("[action]/{banco}")]
    public async Task<IActionResult> UploadCsv(IFormFile file, int banco)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado.");
        }

        try
        {
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
                    IdBanco = banco
                });
            }

            // Adiciona as faturas no banco
            db.Faturas.AddRange(faturas);
            await db.SaveChangesAsync();

            return Ok("Arquivo importado com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao processar o arquivo: {ex.Message}");
        }
    }

    [HttpGet("[action]/{item}/{categoria}")]
    public IActionResult CadastrarCategoria(int item, int categoria)
    {
        try
        {
            var localizado = db.Faturas.Where(p => p.Id == item).FirstOrDefault();

            if (localizado == null)
            {
                return BadRequest("Nao foi possivel localizar o item.");
            }

            localizado.IdCategoria = categoria;
            db.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"algo deu errado: {ex.Message}");
        }

    }

    [HttpGet("action")]
    public IActionResult Listar()
    {
        try
        {
            var localizados = db.Faturas.ToList();
            if (localizados == null)
            {
                return BadRequest("Nao foi possivel acessar fatura");
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

