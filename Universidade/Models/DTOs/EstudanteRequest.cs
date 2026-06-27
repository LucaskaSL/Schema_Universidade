namespace Universidade.Models.DTOs;

public class EstudanteRequest
{
    public string MatEstudante { get; set; } 

    public string Cpf { get; set; } 

    public decimal? Mc { get; set; }

    public int? AnoIngresso { get; set; }
    
}