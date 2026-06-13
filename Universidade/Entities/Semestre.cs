namespace Universidade.Entities;

public partial class Semestre
{
    public short Ano { get; set; }

    public short Semestre1 { get; set; }

    public DateOnly? DataInicio { get; set; }

    public DateOnly? DataFom { get; set; }

    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}
