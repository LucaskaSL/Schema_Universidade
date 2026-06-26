using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class DeleteEstudante : Endpoint<DeleteEstudanteRequest>
{
    private readonly MeusEstudosContext _context;
    public DeleteEstudante(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/estudante/delete/{matEstudante}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteEstudanteRequest req, CancellationToken ct)
    {
        var estudante = await _context.Estudantes.FindAsync(req.MatEstudante);

        if (estudante is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        try
        {
            _context.Estudantes.Remove(estudante);
            await _context.SaveChangesAsync(ct);
            await Send.NoContentAsync(ct);
        }
        catch (Exception e)
        {
            Console.WriteLine("$\"[ERRO CRÍTICO AO DELETAR]: {e.Message}");
            await Send.ErrorsAsync(StatusCodes.Status500InternalServerError, ct);
        }
    }
}