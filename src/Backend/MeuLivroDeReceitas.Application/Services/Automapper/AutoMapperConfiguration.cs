using AutoMapper;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Application.Services.Automapper;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration() 
    {
        CreateMap<RequestRegistrarUsuarioJSON, Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());
    }
}
