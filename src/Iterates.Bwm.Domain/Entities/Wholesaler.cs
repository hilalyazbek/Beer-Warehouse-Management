using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class Wholesaler : EntityBase
{
    public string? Name { get; set; }
    public virtual ICollection<Beer>? Beers { get; set; }
    public virtual ICollection<BeerStock>? BeerStocks { get; set; }
}
