using MeuLivroDeReceitas.Application.Services.Criptografia;
using MeuLivroDeReceitas.Application.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuLivroDeReceitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var hashSenha = configuration.GetSection("Configuracoes:ChaveAdicionalSenha");
        var hashToken = configuration.GetSection("Configuracoes:ChaveToken");
        var tempoVidaToken = configuration.GetSection("Configuracoes:TempoVidaToken");

        services.AddScoped(option => new EcriptadorDeSenha(hashSenha.Value));
        services.AddScoped(option => new TokenController(double.Parse(tempoVidaToken.Value), hashToken.Value));
    }
}
