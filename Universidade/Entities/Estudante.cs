namespace Universidade.Entities;

public partial class Estudante
{
    public string MatEstudante { get; set; } = null!;

    public decimal? Cpf { get; set; }

    public decimal? Mc { get; set; }

    public int? AnoIngresso { get; set; }

    public virtual Usuario? CpfNavigation { get; set; }

    public virtual ICollection<Cursa> Cursas { get; set; } = new List<Cursa>();

    public virtual ICollection<Plano> Planos { get; set; } = new List<Plano>();

    public virtual ICollection<Vinculo> Vinculos { get; set; } = new List<Vinculo>();
}
