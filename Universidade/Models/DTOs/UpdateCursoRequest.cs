using Universidade.Models.Enums;

namespace Universidade.Models.DTOs;

public class UpdateCursoRequest
{
    public int Idcurso { get; set; }

    public string Nome { get; set; } = null!;

    public string? Campus { get; set; }
    
    public tipo_grau? TipoGrau { get; set; }
    
    public tipo_turno? TipoTurno { get; set; }
    
    public tipo_nivel? TipoNivel { get; set; }
}