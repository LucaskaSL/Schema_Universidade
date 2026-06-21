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

// Configuração dos enums no postgres para o C#
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("universidadedb"));
dataSourceBuilder.MapEnum<status_estudante>("universidade.status_estudante");
dataSourceBuilder.MapEnum<tipo_formacao>("universidade.tipo_formacao");
dataSourceBuilder.MapEnum<tipo_grau>("universidade.tipo_grau");
dataSourceBuilder.MapEnum<tipo_jornada>("universidade.tipo_jornada");
dataSourceBuilder.MapEnum<tipo_nivel>("universidade.tipo_nivel");
dataSourceBuilder.MapEnum<tipo_turno>("universidade.tipo_turno");
var universidadedb = dataSourceBuilder.Build();

builder.Services.AddDbContext<MeusEstudosContext>(options =>
    options.UseNpgsql(universidadedb)); 

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints();

//Uso do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
}

app.UseHttpsRedirection();
app.Run();