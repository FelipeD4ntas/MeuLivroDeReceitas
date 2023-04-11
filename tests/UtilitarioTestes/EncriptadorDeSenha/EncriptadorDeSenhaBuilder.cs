using MeuLivroDeReceitas.Application.Services.Criptografia;

namespace UtilitarioTestes.EncriptadorDeSenha;

public class EncriptadorDeSenhaBuilder
{
    public static EcriptadorDeSenha Instancia()
    {
        return new EcriptadorDeSenha("abcd2345");
    }
}
