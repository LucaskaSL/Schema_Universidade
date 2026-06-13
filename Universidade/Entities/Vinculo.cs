using Universidade.Models.Enums;

namespace Universidade.Entities;

public partial class Vinculo
{
    public int Idvinculo { get; set; }

    public string? MatEstudante { get; set; }

    public int? Curso { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataSaida { get; set; }

    public virtual Curso? CursoNavigation { get; set; }

    public virtual Estudante? MatEstudanteNavigation { get; set; }
    
    public status_estudante? StatusEstudanteNavigation { get; set; }
}
