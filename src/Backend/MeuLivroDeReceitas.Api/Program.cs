using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Infra;
using MeuLivroDeReceitas.Infra.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositorio(builder.Configuration);


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