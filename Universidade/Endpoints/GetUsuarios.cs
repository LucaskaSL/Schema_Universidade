using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetUsuarios : EndpointWithoutRequest
{
    private readonly MeusEstudosContext _context;

    public GetUsuarios(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/usuarios");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        // Retorna uma lista de todos os usuários
        var usuarios = await  _context.Usuarios
            .AsNoTracking() 
            .ToListAsync(ct);
        
        // Verifica se a lista está vazia
        if (!usuarios.Any())
        {
            await Send.NoContentAsync(ct);
            return;
        }
        
        await Send.OkAsync(usuarios, ct);
    }
}