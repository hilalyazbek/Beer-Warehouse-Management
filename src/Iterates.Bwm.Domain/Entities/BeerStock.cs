using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class BeerStock : EntityBase
{
    public Guid WholesalerId { get; set; }
    public Guid BeerId { get; set; }
    public decimal Price { get; set; }
    public int StockLevel { get; set; }
    public virtual Wholesaler? Wholesaler { get; set; }
    public virtual Beer? Beer { get; set; }
}
