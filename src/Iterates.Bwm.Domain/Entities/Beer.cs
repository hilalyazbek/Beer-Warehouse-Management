using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Domain.Entities;

public class Beer : EntityBase
{
    public Guid BrewerId { get; set; }
    public string? Name { get; set; }
    public string? AlcoholContent { get; set; }
    public string? BatchNumber { get;set; }
}
