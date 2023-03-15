using Iterates.Bwm.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iterates.Bwm.Api.DTOs;

public class AddBeerDTO
{
    //public Guid BrewerId { get; set; }
    public string? Name { get; set; }
    public string? AlcoholContent { get; set; }
    public string? BatchNumber { get; set; }
}
