using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Api.DTOs;

public class AddSaleDTO
{
    [Required(ErrorMessage = "{0} is required")]
    public Guid WholesalerId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public Guid BeerId { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string? OrderNumber { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    
    public decimal Price { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    
    public bool Delivery { get; set; }
}
