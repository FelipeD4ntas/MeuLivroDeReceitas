using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Exceptions;
using UtilitarioTestes.Requisicoes;
using Xunit;

namespace Validators.Test.Usuario.Registrar;

public class RegistrarUsuarioValidatorTeste
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir();

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_Erro_Nome_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        request.Nome = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.NOME_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        request.Email = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        request.Telefone = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Senha_Vazia()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        request.Senha = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EM_BRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        request.Email = "we";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir();
        request.Telefone = "99 9 9";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalida(int tamanhoSenha)
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequisicaoRegistrarUsuarioBuilder.Construir(tamanhoSenha);

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_INVALIDA));
    }
}
