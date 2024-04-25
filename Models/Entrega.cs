using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SafeGuardPro.Models;

public partial class Entrega
{
    public int? CodEntrega { get; set; }

    public DateOnly DataVal { get; set; }

    public int? CodEpi { get; set; }

    public int? CodCol { get; set; }

    public DateOnly DataEntrega { get; set; }

    [JsonIgnore]
    public virtual Colaborador? CodColNavigation { get; set; } 

    [JsonIgnore]
    public virtual Epi? CodEpiNavigation { get; set; } 
}
