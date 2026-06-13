namespace Universidade.Models.Enums;

using NpgsqlTypes;
using Microsoft.EntityFrameworkCore;
public enum tipo_jornada
{
    [PgName("20h")]
    Horas20,
    
    [PgName("40h")]
    Horas40,
    
    [PgName("DE")]
    DE
}