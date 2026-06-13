namespace Universidade.Entities;

public partial class Usuario
{
    public decimal Cpf { get; set; }

    public string Nome { get; set; } = null!;

    public DateOnly? DataNascimento { get; set; }

    public List<string>? Email { get; set; }

    public List<string>? Telefone { get; set; }

    public string? Login { get; set; }

    public string? Senha { get; set; }

    public virtual Estudante? Estudante { get; set; }

    public virtual Professor? Professor { get; set; }
}
