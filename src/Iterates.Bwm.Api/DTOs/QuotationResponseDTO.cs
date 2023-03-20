using Iterates.Bwm.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iterates.Bwm.Api.DTOs;

public class QuotationResponseDTO
{
    public string? Description { get; set; }
    public List<ItemResponseDTO>? Items{ get; set; }
}

public record ItemResponseDTO
{
    public Guid BeerId { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }
    public decimal TotalBeforeDiscount { get; set; }
    public string? Discount { get; set; }
    public decimal TotalAfterDiscount { get; set; }
}