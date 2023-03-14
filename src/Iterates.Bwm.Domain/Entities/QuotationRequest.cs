using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class QuotationRequest : EntityBase
{
    public Guid WholesalerId { get; set; }
    public Wholesaler? Wholesaler { get; set; }
    public List<ItemRequest>? Items { get; set; }
}

public class ItemRequest
{
    public Guid BeerId { get; set; }
    public int Quantity { get; set; }
}
