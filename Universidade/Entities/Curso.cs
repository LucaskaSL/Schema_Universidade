using System;
using System.Collections.Generic;
using Universidade.Models.Enums;

namespace Universidade.Entities;

public partial class Curso
{
    public int Idcurso { get; set; }

    public string Nome { get; set; } = null!;

    public string? Campus { get; set; }

    public virtual ICollection<Vinculo> Vinculos { get; set; } = new List<Vinculo>();
    
    public tipo_grau? TipoGrau { get; set; }
    
    public tipo_turno? TipoTurno { get; set; }
    
    public tipo_nivel? TipoNivel { get; set; }
}
