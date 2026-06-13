namespace Universidade.Entities;

public partial class Plano
{
    public int? IdProjeto { get; set; }

    public string? MatProfessor { get; set; }

    public string MatEstudante { get; set; } = null!;

    public int Ano { get; set; }

    public virtual Projeto? IdProjetoNavigation { get; set; }

    public virtual Estudante MatEstudanteNavigation { get; set; } = null!;

    public virtual Professor? MatProfessorNavigation { get; set; }
}
