using FluentMigrator.Runner;
using MeuLivroDeReceitas.Domain.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeuLivroDeReceitas.Infra;

public static class Bootstrapper
{
    public static void AddRepositorio(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringCompleta = configuration.GetConexaoCompleta();

        services.AddFluentMigratorCore().ConfigureRunner(c => 
            c.AddMySql5()
                .WithGlobalConnectionString(connectionStringCompleta)
                .ScanIn(Assembly.Load("MeuLivroDeReceitas.Infra")).For.All());
    }
}
