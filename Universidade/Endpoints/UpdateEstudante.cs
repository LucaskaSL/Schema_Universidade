using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class UpdateEstudante : Endpoint<UpdateEstudanteRequest, EstudanteResponse>
{
    private readonly MeusEstudosContext _context;
    public UpdateEstudante(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("api/estudante/updateEstudante/{matEstudante}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateEstudanteRequest req, CancellationToken ct)
    {
        var estudante = await _context.Estudantes.FindAsync(req.MatEstudante);

        if (estudante is null)
        {
            Send.NotFoundAsync(ct);
            return;
        }
        
        if (req.MatEstudante is not null) estudante.MatEstudante = req.MatEstudante;
        if (req.Mc is not null) estudante.Mc = req.Mc;
        if (req.AnoIngresso is not null) estudante.AnoIngresso = req.AnoIngresso;
        
        await _context.SaveChangesAsync(ct);

        var response = new EstudanteResponse()
        {
            Mensagem = "Estudante atualizado com sucesso",
            MatEstudante =  estudante.MatEstudante,
            Mc = estudante.Mc,
            AnoIngresso = estudante.AnoIngresso,
        };
        
        await Send.OkAsync(response, ct);
    }
}