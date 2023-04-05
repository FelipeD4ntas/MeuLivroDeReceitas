using Microsoft.Extensions.Configuration;

namespace MeuLivroDeReceitas.Domain.Extension;

public static class RepositorioExtension
{
    public static string GetNomeDatabase(this IConfiguration configuration)
    {

        return configuration.GetConnectionString("NomeDatabase");
    }

    public static string GetConexaoCompleta(this IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Conexao");
        var nomeDatabase = configuration.GetNomeDatabase();

        return $"{connectionString}Database={nomeDatabase}";
    }
}
