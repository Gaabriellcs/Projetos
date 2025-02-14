using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ControleGasto.Dados
{

    [Table("Faturas", Schema = "dbo")]
    public class Faturas
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        [ForeignKey("Categoria")]
        public int? IdCategoria { get; set; }
        [ForeignKey("Bancos")]
        public int? IdBanco { get; set; }
        [ForeignKey("Usuarios")]
        [JsonIgnore]
        public int IdUsuario { get; set; }
    }
}
