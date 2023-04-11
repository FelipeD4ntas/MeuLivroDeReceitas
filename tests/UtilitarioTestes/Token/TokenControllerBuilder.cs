using MeuLivroDeReceitas.Application.Services.Token;

namespace UtilitarioTestes.Token;
public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "YWJjZHk0NTY1NjU2ZGZ2aG5zc2Rmc2Rmc2ZzZmUsZmRmZjEyMzQ=");
    }
}
