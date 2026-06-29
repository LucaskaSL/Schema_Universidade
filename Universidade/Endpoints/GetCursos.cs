using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetCursos : EndpointWithoutRequest
{
    private readonly MeusEstudosContext _context;

    public GetCursos(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/cursos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var cursos = await _context.Cursos
            .AsNoTracking()
            .ToListAsync(ct);

        if (!cursos.Any())
        {
            await Send.NoContentAsync(ct);
            return;
        }
        
        await Send.OkAsync(cursos, ct);
    }
}