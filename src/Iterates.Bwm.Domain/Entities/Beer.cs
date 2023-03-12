using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class Beer : EntityBase
{
    public string? Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public string? AlcoholContent { get; set; }
    public Guid BrewerId { get; set; }
    public Brewer? Brewer { get; set; }
    public List<WholesalerStock>? WholesalerStocks { get; set; }
}
