using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class GetUsuario : Endpoint<GetUsuarioByCpf, UsuarioResponse>
{
    private readonly MeusEstudosContext _context;

    public GetUsuario(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/api/usuario/{Cpf}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUsuarioByCpf req, CancellationToken ct)
    {
        /* Basicamente um Select, ou seja,
        SELECT (elementos)
        FROM USUARIOS AS u
        WHERE u.cpf = req.cpf
        LIMIT 1
         */
        var usuario = await _context.Usuarios
            .AsNoTracking() // Evita gasto de RAM por simplesmente pegar as variáveis
            .Where(u => Convert.ToDecimal(u.Cpf) == Convert.ToDecimal(req.Cpf))
            .Select(u => new UsuarioResponse
            {
                Mensagem = "Select feito com sucesso",
                Nome =  u.Nome,
                Email = u.Email.FirstOrDefault() ?? "Sem email",
                Login = u.Login,
            })
            .FirstOrDefaultAsync(ct);

        // Verifica se retornou nulo e envia um aviso
        if (usuario is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        // Aviso que deu tudo certo
        await Send.OkAsync(usuario, ct);
    }
    
}