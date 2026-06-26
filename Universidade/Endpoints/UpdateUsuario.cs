using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class UpdateUsuario : Endpoint<UpdateUsuarioRequest, UsuarioResponse>
{
    private readonly MeusEstudosContext _context;
    public UpdateUsuario(MeusEstudosContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/api/usuario/updateUsuario/{Cpf}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateUsuarioRequest req, CancellationToken ct)
    {
        // Busca no banco se o usuário da requisição existe
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Cpf == Convert.ToDecimal(req.Cpf), ct);
        
        // Se não existir, manda erro e para a execução
        if (usuario is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        // Seção para realmente atualizar, de forma parcial (Patch)
        if (req.Nome is not null) usuario.Nome = req.Nome;
        if (req.DataNascimento is not null)  usuario.DataNascimento = req.DataNascimento;
        if (req.Email is not null) usuario.Email = new List<string> { req.Email };
        if (req.Telefone is not null) usuario.Telefone = new List<string> { req.Telefone };
        if (req.Login is not null) usuario.Login = req.Login;
        if (req.Senha is not null) usuario.Senha = req.Senha;
        
        await _context.SaveChangesAsync(ct);
        
        // Seção da resposta do DTO
        var response = new UsuarioResponse
        {
            Mensagem = "Usuario atualizado com sucesso",
            Nome = usuario.Nome,
            Email = usuario.Email.FirstOrDefault() ?? "Sem email",
            Login = usuario.Login
        };
        
        
        await Send.OkAsync(response, ct);
    }
}