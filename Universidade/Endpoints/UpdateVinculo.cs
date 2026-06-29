using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class UpdateVinculo : Endpoint<UpdateVinculoRequest, VinculoResponse>
{
    private readonly MeusEstudosContext _context;
    public UpdateVinculo(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/api/vinculo/update/{idvinculo}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateVinculoRequest req, CancellationToken ct)
    {
        var vinculo = await _context.Vinculos.FindAsync(req.Idvinculo);

        if (vinculo is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        if (req.MatEstudante is not null) vinculo.MatEstudante = req.MatEstudante;
        if (req.Curso is not null) vinculo.Curso = req.Curso;
        if (req.DataEntrada  is not null) vinculo.DataEntrada = req.DataEntrada;
        if (req.DataSaida is not null) vinculo.DataSaida = req.DataSaida;
        if (req.StatusEstudante is not null) vinculo.StatusEstudante = req.StatusEstudante;
        
        await _context.SaveChangesAsync(ct);

        var response = new VinculoResponse
        {
            Mensagem = "Vinculo atualizado com sucesso",
            Idvinculo =  vinculo.Idvinculo,
            MatEstudante =  vinculo.MatEstudante,
            Curso =  vinculo.Curso,
            DataEntrada =  vinculo.DataEntrada,
            DataSaida =  vinculo.DataSaida,
            StatusEstudante =  vinculo.StatusEstudante,
        };
        
        await Send.OkAsync(response, ct);
    }
}