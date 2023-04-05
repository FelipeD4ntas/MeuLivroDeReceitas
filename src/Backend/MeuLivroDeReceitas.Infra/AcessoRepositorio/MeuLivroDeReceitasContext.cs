using MeuLivroDeReceitas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infra.AcessoRepositorio;
public class MeuLivroDeReceitasContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    public MeuLivroDeReceitasContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuLivroDeReceitasContext).Assembly);
    }
}
