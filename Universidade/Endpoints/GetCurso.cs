using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetCurso : Endpoint<GetCursoById, CursoResponse>
{
    private readonly MeusEstudosContext  _context;

    public GetCurso(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/curso/{IdCurso}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetCursoById req, CancellationToken ct)
    {
        var curso = await _context.Cursos
            .AsNoTracking()
            .Where(c => Convert.ToInt32(c.Idcurso) == Convert.ToInt32(req.IdCurso))
            .Select(c => new CursoResponse
            {
                Mensagem = "Select feito com sucesso",
                Nome = c.Nome,
                Campus =  c.Campus,
                TipoGrau =  c.TipoGrau,
                TipoNivel =   c.TipoNivel,
                TipoTurno =  c.TipoTurno,
            })
            .FirstOrDefaultAsync(ct);
        
        if (curso is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(curso, ct);
    }
}