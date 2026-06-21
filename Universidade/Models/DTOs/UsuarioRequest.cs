namespace Universidade.Models.DTOs;

public class UsuarioRequest
{
    public string CPF  { get; set; }
    public string Nome { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
}