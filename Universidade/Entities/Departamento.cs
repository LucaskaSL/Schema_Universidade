namespace Universidade.Entities;

public partial class Departamento
{
    public string CodDepto { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? Chefe { get; set; }

    public float? Orcamento { get; set; }

    public double? Comissal { get; set; }

    public virtual Professor? ChefeNavigation { get; set; }

    public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();

    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();
}
