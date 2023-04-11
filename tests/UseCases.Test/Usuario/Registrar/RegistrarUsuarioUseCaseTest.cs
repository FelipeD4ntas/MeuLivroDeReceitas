using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using UtilitarioTestes.EncriptadorDeSenha;
using UtilitarioTestes.Mapper;
using UtilitarioTestes.Repositorios;
using UtilitarioTestes.Requisicoes;
using UtilitarioTestes.Token;
using Xunit;

namespace UseCases.Test.Usuario.Registrar;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        var useCae = CriarUseCase();
        var response = await useCae.Executar(request);

        response.Should().NotBeNull();
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Email_Cadastrado()
    {
        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        var useCae = CriarUseCase(request.Email);

        Func<Task> acao = async () =>
        {
            await useCae.Executar(request);
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(erro => erro.MensagensDeErro.Count == 1 && erro.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_CADASTRADO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        request.Email = string.Empty;

        var useCae = CriarUseCase();

        Func<Task> acao = async () =>
        {
            await useCae.Executar(request);
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(erro => erro.MensagensDeErro.Count == 1 && erro.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));
    }

    private RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var mapper = MapperBuilder.Instancia();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador, token, repositorioReadOnly);
    }
}
