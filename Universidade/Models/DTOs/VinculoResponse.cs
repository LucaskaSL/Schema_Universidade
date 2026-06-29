using Universidade.Models.Enums;

namespace Universidade.Models.DTOs;

public class VinculoResponse
{
    public string Mensagem { get; set; }
    
    public int Idvinculo { get; set; }
    
    public string? MatEstudante { get; set; }

    public int? Curso { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataSaida { get; set; }
    
    public status_estudante? StatusEstudante{ get; set; }
}