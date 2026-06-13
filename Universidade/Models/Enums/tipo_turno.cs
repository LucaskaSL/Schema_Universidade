namespace Universidade.Models.Enums;

using NpgsqlTypes;
using Microsoft.EntityFrameworkCore;
public enum tipo_turno
{
    Matutino,
    Vespertino,
    Noturno,
    
    [PgName("Turno Indefinido")]
    TurnoIndefinido
}