using Iterates.Bwm.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iterates.Bwm.Api.DTOs;

public class AddBeerDTO
{
    [Required(ErrorMessage = "{0} is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string? AlcoholContent { get; set; }

    [Required(ErrorMessage = "{0} is required")]
    public string? BatchNumber { get; set; }
}
