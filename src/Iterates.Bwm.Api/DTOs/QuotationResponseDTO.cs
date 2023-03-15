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
    public int Quantity { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal PriceBeforeDiscount { get; set; }
    public string? Discount { get; set; }
    public decimal PriceAfterDiscount { get; set; }
}