using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetVinculos : EndpointWithoutRequest
{
    private readonly MeusEstudosContext _context;

    public GetVinculos(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/vinculos");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var vinculos = await _context.Vinculos
            .AsNoTracking()
            .ToListAsync(ct);

        if (!vinculos.Any())
        {
            await Send.NoContentAsync(ct);
            return;
        }
        
        await Send.OkAsync(vinculos, ct);
    }
}