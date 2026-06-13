using Universidade.Models.Enums;

namespace Universidade.Entities;

public partial class Professor
{
    public string MatProfessor { get; set; } = null!;

    public decimal? Cpf { get; set; }

    public string? Departamento { get; set; }

    public DateOnly? DataAdmissao { get; set; }

    public double? Salario { get; set; }
    
    public tipo_formacao? TipoFormacao { get; set; }
    
    public tipo_jornada? TipoJornada { get; set; }

    public virtual Usuario? CpfNavigation { get; set; }

    public virtual Departamento? DepartamentoNavigation { get; set; }

    public virtual ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

    public virtual ICollection<Plano> Planos { get; set; } = new List<Plano>();

    public virtual ICollection<Turma> IdTurmas { get; set; } = new List<Turma>();
}
