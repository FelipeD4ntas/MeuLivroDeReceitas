using Bogus;
using MeuLivroDeReceitas.Comunicacao.Request;

namespace UtilitarioTestes.Requisicoes;

public static class RequisicaoRegistrarUsuarioBuilder
{
    public static RequestRegistrarUsuarioJSON Construir(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarUsuarioJSON>()
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"));
    }
}
