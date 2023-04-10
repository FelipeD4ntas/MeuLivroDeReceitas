namespace MeuLivroDeReceitas.Comunicacao.Response;

public class ResponseErrorJSON
{
    public List<string> Mensagens { get; set; } = new List<string>();

    public ResponseErrorJSON(string mensagem)
    {
        Mensagens.Add(mensagem);
    }

    public ResponseErrorJSON(List<string> mensagens)
    {
        Mensagens = mensagens;
    }
}
