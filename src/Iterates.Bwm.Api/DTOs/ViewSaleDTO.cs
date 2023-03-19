using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Api.DTOs;

public class ViewSaleDTO
{
    public Guid WholesalerId { get; set; }

    public Guid BeerId { get; set; }

    public string? OrderNumber { get; set; }

    public int Stock { get; set; }
    
    public decimal Price { get; set; }
    
    public bool Delivery { get; set; }
}
