using Iterates.Bwm.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iterates.Bwm.Api.DTOs;

public class AddBeerDTO
{
    public Guid BrewerId { get; set; }
    public string? Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public string? AlcoholContent { get; set; }
}
