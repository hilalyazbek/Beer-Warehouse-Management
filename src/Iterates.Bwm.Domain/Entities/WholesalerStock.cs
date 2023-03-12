using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class WholesalerStock : EntityBase
{
    public Guid WholesalerId { get; set; }
    public Guid BeerId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int StockLevel { get; set; }
    public virtual Wholesaler? Wholesaler { get; set; }
    public virtual Beer? Beer { get; set; }
}
