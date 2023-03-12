using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Iterates.Bwm.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class BrewerController : Controller
{
    private readonly IBrewerService _brewerService;

    public BrewerController(IBrewerService brewerService)
    {
        _brewerService = brewerService;
    }

    [HttpPost("beers")]
    public async Task<ActionResult<Beer>> AddBeerAsync(Beer beer)
    {
        var addedBeer = await _brewerService.AddBeerAsync(beer);
        return Ok(addedBeer);
    }

    [HttpDelete("beers/{id}")]
    public async Task<IActionResult> DeleteBeerAsync(Guid id)
    {
        var beerToDelete = new Beer { Id = id };
        var deleted = await _brewerService.DeleteBeerAsync(beerToDelete);
        if (deleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("sales")]
    public async Task<ActionResult<WholesalerStock>> AddSaleToWholesalerAsync(WholesalerStock wholesalerStock)
    {
        var addedSale = await _brewerService.AddSaleToWholesalerAsync(wholesalerStock);
        return Ok(addedSale);
    }
}

