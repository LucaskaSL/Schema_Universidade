using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class DeleteCurso : Endpoint<DeleteCursoRequest>
{
    private readonly MeusEstudosContext _context;

    public DeleteCurso(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/curso/delete/{idCurso}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteCursoRequest req, CancellationToken ct)
    {
        var curso = await _context.Cursos
            .FirstOrDefaultAsync(c => c.Idcurso == Convert.ToInt32(req.idCurso), ct);

        if (curso is null)
        {
            Send.NotFoundAsync(ct);
            return;
        }

        try
        {
            _context.Cursos.Remove(curso);
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