using AutoMapper;
using MeuLivroDeReceitas.Application.Services.Criptografia;
using MeuLivroDeReceitas.Application.Services.Token;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Comunicacao.Response;
using MeuLivroDeReceitas.Domain.Repositorios;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;

namespace MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;

public class RegistrarUsuarioUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _repositorioReadOnlyUsuario;
    private readonly IUsuarioWriteOnlyRepositorio _repositorioUsuario;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IMapper _mapper;
    private readonly EcriptadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public RegistrarUsuarioUseCase(IUsuarioWriteOnlyRepositorio repositorioUsuario, IMapper mapper, IUnidadeDeTrabalho unidadeDeTrabalho, EcriptadorDeSenha ecriptadorDeSenha, TokenController tokenController, IUsuarioReadOnlyRepositorio repositorioReadOnlyUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
        _mapper = mapper;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _encriptadorDeSenha = ecriptadorDeSenha;
        _tokenController = tokenController;
        _repositorioReadOnlyUsuario = repositorioReadOnlyUsuario;
    }

    public async Task<ResponseUsuarioRegistradoJSON> Executar(RequestRegistrarUsuarioJSON request)
    {
        await Validar(request);

        var entidade = _mapper.Map<Domain.Entidades.Usuario>(request);
        entidade.Senha = _encriptadorDeSenha.Criptografar(request.Senha);

        await _repositorioUsuario.Adicionar(entidade);
        await _unidadeDeTrabalho.Commit();

        var token = _tokenController.GerarToken(entidade.Email);

        return new ResponseUsuarioRegistradoJSON
        {
            Token = token
        };
    }

    private async Task Validar(RequestRegistrarUsuarioJSON request)
    {
        var validator = new RegistrarUsuarioValidator();
        var resultado = validator.Validate(request);

        var emailExistente = await _repositorioReadOnlyUsuario.ExisteUsuarioComEmail(request.Email);

        if (emailExistente)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMensagensDeErro.EMAIL_CADASTRADO));
        }

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
