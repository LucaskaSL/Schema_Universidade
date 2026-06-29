using Universidade.Models.Enums;

namespace Universidade.Models.DTOs;

public class CursoRequest
{
    public string? Nome { get; set; }

    public string? Campus { get; set; }
    
    public tipo_grau TipoGrau { get; set; }
    
    public tipo_turno? TipoTurno { get; set; }
    
    public tipo_nivel? TipoNivel { get; set; }
}