using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;
using Universidade.Models.Enums;

namespace Universidade.Endpoints;

public class CreateCurso : Endpoint<CursoRequest, CursoResponse>
{
    private readonly MeusEstudosContext _context;

    public CreateCurso(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/api/curso/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CursoRequest req, CancellationToken ct)
    {
        var novoCurso = new Curso
        {
            Nome  = req.Nome ?? "Sem nome",
            Campus = req.Campus,
            TipoGrau =  req.TipoGrau,
            TipoTurno =   req.TipoTurno,
            TipoNivel =  req.TipoNivel,
        };
        await _context.Cursos.AddAsync(novoCurso, ct);
        await _context.SaveChangesAsync(ct);

        var response = new CursoResponse
        {
            Mensagem = "Curso criado com sucesso",
            Idcurso = novoCurso.Idcurso,
            Nome  = novoCurso.Nome,
            Campus = novoCurso.Campus,
            TipoGrau = novoCurso.TipoGrau,
            TipoTurno = novoCurso.TipoTurno,
            TipoNivel = novoCurso.TipoNivel,
        };
        
        await Send.OkAsync(response, ct);
    }
}