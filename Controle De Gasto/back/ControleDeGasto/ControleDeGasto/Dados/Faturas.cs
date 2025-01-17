using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public int? IdCategoria { get; set; }
        public int? IdBanco { get; set; }

        //[ForeignKey("IdCategoria")]
        //public Categoria? Categoria { get; set; }

        //[ForeignKey("IdBanco")]
        //public Bancos? Banco { get; set; }
    }
}
