using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class Beer : EntityBase
{
    public string? Name { get; set; }
    public string? AlcoholContent { get; set; }
    public virtual Brewer? Brewer { get; set; }
    public virtual ICollection<Wholesaler>? Wholesalers { get; set; }
}
