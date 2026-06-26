using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class CreateEstudante : Endpoint<EstudanteRequest, EstudanteResponse>
{
    private readonly MeusEstudosContext _context;
    public CreateEstudante(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/api/estudante/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EstudanteRequest req, CancellationToken ct)
    {
        var novoEstudante = new Estudante
        {
            MatEstudante = req.MatEstudante,
            Cpf = Convert.ToDecimal(req.Cpf),
            Mc = req.Mc,
            AnoIngresso = req.AnoIngresso,
        };
        
        await _context.Estudantes.AddAsync(novoEstudante, ct);
        await _context.SaveChangesAsync(ct);

        var response = new EstudanteResponse
        {
            Mensagem = "Estudante cadastrado com sucesso",
            MatEstudante =  novoEstudante.MatEstudante,
            Mc  = novoEstudante.Mc,
            AnoIngresso = novoEstudante.AnoIngresso
        };
        
        await Send.OkAsync(response, ct);
    }
}