using ControleGasto.Dados;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using CsvHelper.TypeConversion;
using System.Text.RegularExpressions;

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

           

            var codigoBanco = db.Bancos.Where(p => p.Id == banco && p.IdUsuario == usuario).FirstOrDefault();

            if (codigoBanco?.CodigoBanco == 77)
            {
                csv.Context.RegisterClassMap<TransacaoMapInter>();

            }
            else
            {
                csv.Context.RegisterClassMap<TransacaoMap>();

            }


           

            var transacoes = csv.GetRecords<Transacao>().ToList();

            
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

    [HttpGet("[action]/{dataInicio}/{dataFim}")]
    public IActionResult Listar(DateTime dataInicio, DateTime dataFim)
    {
        try
        {

            var usuario = _util.BuscaUsuario(User);

            if (usuario == 0)
            {
                return BadRequest(new { message = "Usuário não encontrado" });
            }

            if (dataInicio > dataFim)
            {
                return BadRequest(new { message = "A data de início não pode ser maior que a data de fim." });
            }


            var localizados = db.Faturas
                .GroupJoin(
                    db.Categorias,
                    fatura => fatura.IdCategoria,
                    categoria => categoria.Id,
                    (fatura, categorias) => new { fatura, categorias }
                )
                .SelectMany(
                    fc => fc.categorias.DefaultIfEmpty(),
                    (fc, categoria) => new { fc.fatura, categoria }
                )
                .GroupJoin(
                    db.Bancos,
                    f => f.fatura.IdBanco,
                    banco => banco.Id,
                    (f, bancos) => new { f, bancos }
                )
                .SelectMany(
                    fb => fb.bancos.DefaultIfEmpty(),
                    (fb, banco) => new
                    {
                        fb.f.fatura.Id,
                        fb.f.fatura.Descricao,
                        fb.f.fatura.Valor,
                        fb.f.fatura.Data,
                        idUsuario = fb.f.fatura.IdUsuario,
                        idCategoria = fb.f.categoria != null ? fb.f.categoria.Id : (int?)null,
                        CategoriaNome = fb.f.categoria != null ? fb.f.categoria.Descricao : "Sem Categoria",
                        BancoNome = banco != null ? banco.Nome : "Sem Banco"
                    }
                )
                .Where(f => f.idUsuario == usuario && f.Data >= dataInicio && f.Data <= dataFim && f.Descricao != "Pagamento recebido")
                .ToList();



            //var localizados = db.Faturas.Where(p => p.IdUsuario == usuario);
            if (localizados == null)
            {
                return BadRequest(new { message = "Não  foi possivel acessar fatura" });
            }
            return Ok(localizados);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
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

    public class TransacaoMapInter : ClassMap<Transacao>
    {
        public TransacaoMapInter()
        {
            Map(t => t.Data).Name("Data").TypeConverterOption.Format("dd/MM/yyyy");
            Map(t => t.Descricao).Name("Lançamento");
            Map(t => t.Valor).Name("Valor")
            .TypeConverterOption.NumberStyles(NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands)
            .TypeConverterOption.CultureInfo(new CultureInfo("pt-BR"))
            .TypeConverter(new ValorConverter()); ;
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

    public class ValorConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0m; // Retorna 0 se o valor for nulo ou vazio

            // Remove "R$", espaços extras e converte para decimal
            string valorLimpo = Regex.Replace(text, @"[^\d,]", "");

            return decimal.Parse(valorLimpo, new CultureInfo("pt-BR"));
        }
    }
}

