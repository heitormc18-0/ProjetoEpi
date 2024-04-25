using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SafeGuardPro.Models;

public partial class Epi
{
    public int CodEpi { get; set; }

    public string NomeEpi { get; set; } = null!;

    public string Descricao { get; set; } = null!;

[JsonIgnore]
    public virtual ICollection<Entrega>? Entregas { get; } = new List<Entrega>();
}
