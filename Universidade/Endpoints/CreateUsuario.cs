using Universidade.Models.DTOs;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Universidade.Data;
using Universidade.Entities;

namespace Universidade.Endpoints;

public class CreateUsuario : Endpoint<UsuarioRequest, UsuarioResponse>
{
    // Invejeção do banco de dados
    private readonly MeusEstudosContext _context;
    public CreateUsuario(MeusEstudosContext context)
    {
        _context = context;
    }
    
    public override void Configure()
    {
        Post("/api/usuario/create");
        AllowAnonymous();
    }

    // Função principal
    public override async Task HandleAsync(UsuarioRequest req, CancellationToken ct)
    {
        // Cria a parte da request
        var novoUsuario = new Usuario
        {
            Cpf = Convert.ToDecimal(req.CPF),
            Nome = req.Nome,
            DataNascimento = req.DataNascimento,
            Email = new List<string>{req.Email},
            Telefone = new List<string>{req.Telefone},
            Login = req.Login,
            Senha = req.Senha
        };
        
        // Faz a preparação para um "insert"
        _context.Usuarios.Add(novoUsuario);
        await _context.SaveChangesAsync(ct);

        // Cria a parte do response
        var response = new UsuarioResponse
        {
            Mensagem = "Novo Usuário Cadastrado",
            Nome = novoUsuario.Nome,
            Email = novoUsuario.Email.FirstOrDefault() ?? "Sem email",
            Login = novoUsuario.Login,
        };
        
        // Devolve um sinal de ok
        await Send.OkAsync(response, ct);
    }
}