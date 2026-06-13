using System;
using System.Collections.Generic;

namespace Universidade.Entities;

public partial class Cursa
{
    public string MatEstudante { get; set; } = null!;

    public int IdTurma { get; set; }

    public float? Nota { get; set; }

    public virtual Turma IdTurmaNavigation { get; set; } = null!;

    public virtual Estudante MatEstudanteNavigation { get; set; } = null!;
}
