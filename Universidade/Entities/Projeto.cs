namespace Universidade.Entities;

public partial class Projeto
{
    public int IdProjeto { get; set; }

    public string? Descricao { get; set; }

    public virtual ICollection<Plano> Planos { get; set; } = new List<Plano>();
}
