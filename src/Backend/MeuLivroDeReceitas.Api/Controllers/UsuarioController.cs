using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUsuarioRegistradoJSON), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarUsuario(
        [FromServices] IRegistrarUsuarioUseCase useCase,
        [FromBody] RequestRegistrarUsuarioJSON request)
    {
        var response = await useCase.Executar(request);

        return Created(string.Empty, response);
    }
}
