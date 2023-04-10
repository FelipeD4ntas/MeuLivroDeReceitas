using System.Security.Cryptography;
using System.Text;

namespace MeuLivroDeReceitas.Application.Services.Criptografia;

public class EcriptadorDeSenha
{
    private readonly string _chaveEcriptacao;

    public EcriptadorDeSenha(string chaveEcriptacao)
    {
        _chaveEcriptacao = chaveEcriptacao;
    }

    public string Criptografar(string senha)
    {
        var senhaComChaveEncriptacao = $"{senha}{_chaveEcriptacao}";

        var bytes = Encoding.UTF8.GetBytes(senhaComChaveEncriptacao);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);

        return StringBytes(hashBytes);
    }

    private string StringBytes(byte[] hashBytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
