using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleGasto.Dados
{
    [Table("Usuarios", Schema = "dbo")]
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public string Nome { get; set; } = string.Empty;


        [InverseProperty("Usuario")]
        public IEnumerable<Categoria>? Categoria { get; set; }

    }
}
