using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetVinculo : Endpoint<GetVinculoById, VinculoResponse>
{
    private readonly MeusEstudosContext _context;
    public GetVinculo(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/vinculo/{idvinculo}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetVinculoById req, CancellationToken ct)
    {
        var vinculo = await  _context.Vinculos
            .AsNoTracking()
            .Where(v => v.Idvinculo == req.IdVinculo)
            .Select(v => new VinculoResponse
            {
                Mensagem = "Select feito com sucesso",
                MatEstudante = v.MatEstudante,
                Curso =  v.Curso,
                DataEntrada = v.DataEntrada,
                DataSaida = v.DataSaida,
                StatusEstudante = v.StatusEstudante,
            })
            .FirstOrDefaultAsync(ct);

        if (vinculo is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(vinculo, ct);
    }
}