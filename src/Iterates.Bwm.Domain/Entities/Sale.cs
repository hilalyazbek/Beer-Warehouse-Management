using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class Sale : EntityBase
{
    public Guid WholesalerId { get; set; }
    public Wholesaler? Wholesaler { get; set; }
    public Guid BeerId { get; set; }
    public Beer? Beer { get; set; }
    public string? OrderNumber { get; set; }
    public int Stock { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public bool Delivery { get; set; }
}
