using Iterates.Bwm.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iterates.Bwm.Api.DTOs;

public class QuotationRequestDTO
{
    public Guid WholesalerId { get; set; }
    public List<ItemRequestDTO>? Items { get; set; }
}

public record ItemRequestDTO
{
    public Guid BeerId { get; set; }
    public int Quantity { get; set; }
}