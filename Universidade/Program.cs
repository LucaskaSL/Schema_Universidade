using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;
using Npgsql;
using FastEndpoints;
using FastEndpoints.Swagger;
using Universidade.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints()
                .SwaggerDocument();

builder.Services.AddAuthorization();

// Conexão com o banco de dados
builder.Services.AddDbContext<MeusEstudosContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("universidadedb")));

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints(c => {
    // API lê e responde Enums como textos ao invés de números
    c.Serializer.Options.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

//Uso do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.UseHttpsRedirection();
app.Run();