using System.ComponentModel.DataAnnotations.Schema;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Api.DTOs;

public class WholesalerDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}
