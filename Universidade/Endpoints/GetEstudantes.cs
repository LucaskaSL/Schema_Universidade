using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetEstudantes : EndpointWithoutRequest
{
    private readonly MeusEstudosContext _context;
    public GetEstudantes(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/estudantes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var estudantes = await  _context.Estudantes
            .AsNoTracking()
            .ToListAsync(ct);

        if (!estudantes.Any())
        {
            await Send.NoContentAsync(ct);
            return;
        }
        
        await Send.OkAsync(estudantes, ct);
    }
}