using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class QuotationResponse : EntityBase
{
    public Guid WholesalerId { get; set; }
    public Wholesaler? Wholesaler { get; set; }
    public List<ItemResponse>? Items { get; set; }
}

public class ItemResponse
{
    public Guid BeerId { get; set; }
    public int Quantity { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal PriceBeforeDiscount { get; set; }
    public string? Discount { get; set; }
    public decimal PriceAfterDiscount { get; set; }
}