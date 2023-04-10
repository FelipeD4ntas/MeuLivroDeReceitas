using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    public Task<ResponseUsuarioRegistradoJSON> Executar(RequestRegistrarUsuarioJSON request);
}
