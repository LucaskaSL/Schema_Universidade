using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class DeleteUsuario : Endpoint<DeleteUsuarioRequest>
{
    // Invejeção do banco de dados
    private readonly MeusEstudosContext _context;
    public DeleteUsuario(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/api/usuario/delete/{Cpf}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteUsuarioRequest req, CancellationToken ct)
    {
        // Busca o usuário no banco que bata com o cpf da requisição
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Cpf == Convert.ToDecimal(req.Cpf), ct);
        
        // Verifica se o usuário existe no banco, se não existir, para a execução
        if (usuario is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        // Deleta, salva as alterações e manda HTTP 204
        try
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync(ct);
            await Send.NoContentAsync(ct);
        }
        // Tratamente de erro sem logger, caso não dê para deletar
        catch (Exception e)
        {
            Console.WriteLine($"[ERRO CRÍTICO AO DELETAR]: {e.Message}");
            await Send.ErrorsAsync(StatusCodes.Status500InternalServerError, ct);
        }
        
        
    }
}