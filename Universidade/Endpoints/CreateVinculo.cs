using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class CreateVinculo : Endpoint<VinculoRequest, VinculoResponse>
{
    private readonly MeusEstudosContext _context;

    public CreateVinculo(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/api/vinculo/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(VinculoRequest req, CancellationToken ct)
    {
        var novoVinculo = new Vinculo
        {
            MatEstudante =  req.MatEstudante,
            Curso =  req.Curso,
            DataEntrada =  req.DataEntrada,
            DataSaida =  req.DataSaida,
            StatusEstudante = req.StatusEstudante,
        };
        
        await  _context.Vinculos.AddAsync(novoVinculo, ct);
        await _context.SaveChangesAsync(ct);

        var response = new VinculoResponse
        {
            Mensagem = "Vinculo criado com sucesso!",
            Idvinculo = novoVinculo.Idvinculo,
            MatEstudante =  novoVinculo.MatEstudante,
            Curso = novoVinculo.Curso,
            DataEntrada = novoVinculo.DataEntrada,
            DataSaida = novoVinculo.DataSaida,
            StatusEstudante = novoVinculo.StatusEstudante,
        };
        
        await Send.OkAsync(response, ct);
    }
}