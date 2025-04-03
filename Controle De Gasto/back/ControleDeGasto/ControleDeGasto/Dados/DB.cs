using Microsoft.EntityFrameworkCore;

namespace ControleGasto.Dados
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options) { }

        public DbSet<ControleGasto.Dados.Bancos> Bancos { get; set; }
        public DbSet<ControleGasto.Dados.Categoria> Categorias { get; set; }
        public DbSet<ControleGasto.Dados.Faturas> Faturas { get; set; }
        public DbSet<ControleGasto.Dados.Usuarios> Usuarios { get; set; }
        public DbSet<ControleGasto.Dados.ConciliacaoPadrao> ConciliacaoPadrao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do tipo de dados para a coluna Valor
            modelBuilder.Entity<Faturas>()
                .Property(f => f.Valor)
                .HasColumnType("decimal(18,2)");  // Configura o tipo decimal com precisão e escala
        }
    }
}
