using MeuLivroDeReceitas.Exceptions;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using Xunit;

namespace WebApi.Test.V1;
public class ControllerBase : IClassFixture<MeuLivroDeReceitasWebApplicationFactory<Program>>
{
    private readonly HttpClient _cliente;

    public ControllerBase(MeuLivroDeReceitasWebApplicationFactory<Program> factory)
    {
        _cliente = factory.CreateClient();
        ResourceMensagensDeErro.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);
        return await _cliente.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }
}
