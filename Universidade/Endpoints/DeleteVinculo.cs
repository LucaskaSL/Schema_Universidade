using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class DeleteVinculo : Endpoint<DeleteVInculoRequest>
{
    private readonly MeusEstudosContext _context;

    public DeleteVinculo(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/vinculo/delete/{idvinculo}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteVInculoRequest req, CancellationToken ct)
    {
        var vinculo = await _context.Vinculos.FindAsync(req.Idvinculo);

        if (vinculo is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        try
        {
            _context.Vinculos.Remove(vinculo);
            await  _context.SaveChangesAsync(ct);
            await Send.NoContentAsync(ct);
        }
        catch (Exception e)
        {
            Console.WriteLine("$\"[ERRO CRÍTICO AO DELETAR]: {e.Message}");
            await Send.ErrorsAsync(StatusCodes.Status500InternalServerError, ct);
        }
    }
}