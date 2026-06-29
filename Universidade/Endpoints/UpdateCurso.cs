using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class UpdateCurso : Endpoint<UpdateCursoRequest, CursoResponse>
{
    private readonly MeusEstudosContext _context;

    public UpdateCurso(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/api/curso/update/{idCurso}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCursoRequest req, CancellationToken ct)
    {
        var curso = await _context.Cursos
            .FirstOrDefaultAsync(c => c.Idcurso == Convert.ToInt32(req.Idcurso), ct);

        if (curso is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        if (req.Nome is not null) curso.Nome = req.Nome;
        if (req.Campus is not null) curso.Campus = req.Campus;
        if (req.TipoGrau  is not null) curso.TipoGrau = req.TipoGrau;
        if (req.TipoNivel is not null)  curso.TipoNivel = req.TipoNivel;
        if (req.TipoTurno is not null)  curso.TipoTurno = req.TipoTurno;

        await _context.SaveChangesAsync(ct);
        
        var response = new CursoResponse
        {
            Mensagem = "Curso atualizado com sucesso!",
            Nome = curso.Nome,
            Campus = curso.Campus,
            TipoGrau = curso.TipoGrau,
            TipoNivel = curso.TipoNivel,
            TipoTurno = curso.TipoTurno
        };
        
        await Send.OkAsync(response, ct);
    }
}