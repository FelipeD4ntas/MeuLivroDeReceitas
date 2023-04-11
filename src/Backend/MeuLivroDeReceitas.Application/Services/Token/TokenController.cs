using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeuLivroDeReceitas.Application.Services.Token;

public class TokenController
{
    private const string EMAIL_ALIAS = "eml";
    private readonly double _tempoDeVidaTokenEmMinutos;
    private readonly string _chaveDeSeguranca;

    public TokenController(double tempoDeVidaTokenEmMinutos, string chaveDeSeguranca)
    {
        _tempoDeVidaTokenEmMinutos = tempoDeVidaTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }

    public string GerarToken(string emailDoUsuario)
    {
        var claims = new List<Claim>
        {
            new Claim(EMAIL_ALIAS, emailDoUsuario)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeVidaTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SymmetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public void ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametrosValidacao = new TokenValidationParameters
        {
            IssuerSigningKey = SymmetricKey(),
            ValidateLifetime = true,
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = true,
            ValidateAudience = true
        };

        tokenHandler.ValidateToken(token, parametrosValidacao, out _);
    }

    private SymmetricSecurityKey SymmetricKey()
    {
        var symmetricKeyValue = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetricKeyValue);
    }
}
