using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControleGasto.Dados;

[Table("Categoria", Schema = "dbo")]
public record Categoria
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    [ForeignKey("Usuarios")]
    [JsonIgnore]
    public int IdUsuario { get; set; }
}
