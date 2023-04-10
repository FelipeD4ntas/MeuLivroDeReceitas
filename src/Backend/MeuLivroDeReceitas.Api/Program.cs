using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Application.Services.Automapper;
using MeuLivroDeReceitas.Application.UseCases.Usuario.Registrar;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Infra;
using MeuLivroDeReceitas.Infra.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
}).CreateMapper()); 
builder.Services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

AtualizarBaseDeDados();

app.Run();

void AtualizarBaseDeDados()
{
    var connectionString = builder.Configuration.GetConnectionString("Conexao");
    var nomeDatabase = builder.Configuration.GetNomeDatabase();

    Database.CriarDatabase(connectionString, nomeDatabase);
    app.MigrateBancoDeDados();
}