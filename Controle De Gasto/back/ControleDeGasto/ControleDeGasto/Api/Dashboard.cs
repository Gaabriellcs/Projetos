using ControleGasto;
using ControleGasto.Dados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleGasto.Api;

[Route("api/[controller]")]
[ApiController]
public class Dashboard : ControllerBase
{

    private readonly Dados.DB db;
    private readonly Util _util;
    public Dashboard(Dados.DB DB, Util util)
    {
        db = DB;
        _util = util;
    }


    [HttpGet("[action]")]
    public IActionResult TrazDashboard()
    {
        var usuario = _util.BuscaUsuario(User);

        if (usuario == 0)
        {
            return BadRequest(new { Message = "Usuário não encontrado" });
        }

        var chartData = db.Faturas
             .GroupJoin(
                 db.Categorias,
                 fatura => fatura.IdCategoria,
                 categoria => categoria.Id,
                 (fatura, categorias) => new { fatura, categorias }
             )
             .SelectMany(
                 fc => fc.categorias.DefaultIfEmpty(),
                 (fc, categoria) => new
                 {
                     idUsuario = fc.fatura.IdUsuario,
                     CategoriaNome = categoria != null ? categoria.Descricao : "Sem Categoria",
                     Valor = fc.fatura.Valor
                 }
             )
             .Where(f => f.idUsuario == usuario)
             .GroupBy(f => f.CategoriaNome)
             .Select(g => new
             {
                 CategoriaNome = g.Key,
                 TotalValor = g.Sum(f => f.Valor)
             })
             .Where(g => g.CategoriaNome != "Sem Categoria")
             .OrderByDescending(g => g.TotalValor)
             .ToList();


        var resultadoChart = new
        {
            labels = chartData.Select(x => x.CategoriaNome).ToArray(),
            datasets = new[]
            {
        new
        {
            label = "Gastos por Categoria",
           data = chartData
            .Where(x => x.CategoriaNome != "Sem Categoria")
            .Select(x => x.TotalValor)
            .ToArray(),

        backgroundColor = new[] { "#42A5F5", "#66BB6A", "#FFA726", "#FF7043", "#AB47BC" }
        }
}
        };

        return Ok(resultadoChart);

    }
}
