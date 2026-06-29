using Universidade.Models.Enums;

namespace Universidade.Models.DTOs;

public class VinculoRequest
{
    public string? MatEstudante { get; set; }

    public int? Curso { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataSaida { get; set; }
    
    public status_estudante? StatusEstudante { get; set; }
}