using FluentAssertions;
using System.Net;
using System.Text.Json;
using UtilitarioTestes.Requisicoes;
using WebApi.Test.V1;
using Xunit;

namespace WebApi.Test.Usuario.Registrar;

public class RegistrarUsuarioTeste : ControllerBase
{
    private const string METODO = "usuario";

    public RegistrarUsuarioTeste(MeuLivroDeReceitasWebApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequisicaoRegistrarUsuarioBuilder.Construir();

        var response = await PostRequest(METODO, request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        //responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }
}
