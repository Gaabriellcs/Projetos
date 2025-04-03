using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ControleGasto.Dados
{
    public class ConciliacaoPadrao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }

        //public int IdUsuario { get; set; }
        [ForeignKey("Usuario")]
        public int? IdUsuario { get; set; }
        //public int IdCategoria { get; set; }
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
    }
}
