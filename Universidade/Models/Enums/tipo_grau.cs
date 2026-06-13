namespace Universidade.Models.Enums;

using NpgsqlTypes;
using Microsoft.EntityFrameworkCore;
public enum tipo_grau
{
    Bacharelado,
    
    [PgName("Licenciatura Plena")]
    LicenciaturaPlena
}