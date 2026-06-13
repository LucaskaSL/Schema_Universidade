namespace Universidade.Entities;

public partial class Turma
{
    public int IdTurma { get; set; }

    public string CodDisc { get; set; } = null!;

    public int? Numero { get; set; }

    public short? Ano { get; set; }

    public short? Semestre { get; set; }

    public virtual Disciplina CodDiscNavigation { get; set; } = null!;

    public virtual ICollection<Cursa> Cursas { get; set; } = new List<Cursa>();

    public virtual Semestre? SemestreNavigation { get; set; }

    public virtual ICollection<Professor> MatProfessors { get; set; } = new List<Professor>();
}
