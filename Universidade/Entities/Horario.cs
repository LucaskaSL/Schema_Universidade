namespace Universidade.Entities;

public partial class Horario
{
    public int IdHorario { get; set; }

    public string Dia { get; set; } = null!;

    public short Slot { get; set; }
}
