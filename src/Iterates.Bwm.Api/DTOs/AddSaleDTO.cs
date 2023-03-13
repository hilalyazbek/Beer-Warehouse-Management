using Iterates.Bwm.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iterates.Bwm.Api.DTOs;

public class AddSaleDTO
{
    public Guid WholesalerId { get; set; }
    public Guid BeerId { get; set; }
    public string? OrderNumber { get; set; }
    public int Stock { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public bool Delivery { get; set; }
}
