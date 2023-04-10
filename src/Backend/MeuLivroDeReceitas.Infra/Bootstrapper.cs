using FluentMigrator.Runner;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Infra.AcessoRepositorio;
using MeuLivroDeReceitas.Infra.AcessoRepositorio.Repositorios;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infra;

public static class Bootstrapper
{
    public static void AddRepositorio(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);

        AddRepositorios(services);
        AddContext(services, configuration);
        AddUnidadeDeTrabalho(services);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringCompleta = configuration.GetConexaoCompleta();

        services.AddFluentMigratorCore().ConfigureRunner(c => 
            c.AddMySql5()
                .WithGlobalConnectionString(connectionStringCompleta)
                .ScanIn(Assembly.Load("MeuLivroDeReceitas.Infra")).For.All());
    }

    private static void AddContext(IServiceCollection services, IConfiguration configuration)
    {
        var versaoServidor = new MySqlServerVersion(new Version(8, 0, 32));
        var connectionString = configuration.GetConexaoCompleta();

        services.AddDbContext<MeuLivroDeReceitasContext>(options =>
        {
            options.UseMySql(connectionString, versaoServidor);
        });
    }

    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
    }

    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioWriteOnlyRepositorio, UsuarioRepositorio>();
        services.AddScoped<IUsuarioReadOnlyRepositorio, UsuarioRepositorio>();

    }
}
