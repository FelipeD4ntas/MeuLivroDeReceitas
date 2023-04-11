using AutoMapper;
using MeuLivroDeReceitas.Application.Services.Automapper;

namespace UtilitarioTestes.Mapper;
public class MapperBuilder
{
    public static IMapper Instancia()
    {
        var configuracao = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperConfiguration>();
        });

        return configuracao.CreateMapper();
    }
}
