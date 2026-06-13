namespace Universidade.Entities;

public partial class Disciplina
{
    public string CodDisc { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? PreReq { get; set; }

    public short? Creditos { get; set; }

    public string? DeptoResponsavel { get; set; }

    public virtual Departamento? DeptoResponsavelNavigation { get; set; }

    public virtual ICollection<Disciplina> InversePreReqNavigation { get; set; } = new List<Disciplina>();

    public virtual Disciplina? PreReqNavigation { get; set; }

    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}
