using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControleGasto.Dados
{
    [Table("Bancos", Schema = "dbo")]
    public record Bancos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        [ForeignKey("Usuarios")]
        [JsonIgnore]
        public int IdUsuario { get; set; }
        public bool Ativo { get; set; }

    }
}
