using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetEstudante : Endpoint<GetEstudanteByMat, EstudanteResponse>
{
    private readonly MeusEstudosContext _context;

    public GetEstudante(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/estudante/{matEstudante}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetEstudanteByMat req, CancellationToken ct)
    {
        var estudante = await _context.Estudantes
            .AsNoTracking()
            .Where(e => e.MatEstudante == req.MatEstudante)
            .Select(e => new EstudanteResponse
            {
                Mensagem = "Select feito com sucesso",
                MatEstudante = e.MatEstudante,
                Mc = e.Mc,
                AnoIngresso =  e.AnoIngresso,
            })
            .FirstOrDefaultAsync(ct);

        if (estudante is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        await Send.OkAsync(estudante, ct);
    }
}