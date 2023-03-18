using Iterates.Bwm.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iterates.Bwm.Api.DTOs;

public class QuotationRequestDTO
{
    [Required(ErrorMessage = "{0} is required")]
    public Guid WholesalerId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public List<ItemRequestDTO>? Items { get; set; }
}

public record ItemRequestDTO
{
    public Guid BeerId { get; set; }
    public int Quantity { get; set; }
}