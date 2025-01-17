using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGasto.Dados;

[Table("Categoria", Schema = "dbo")]
public record Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
}
