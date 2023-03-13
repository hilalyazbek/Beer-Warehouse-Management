using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Iterates.Bwm.Api.DTOs;
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
    private readonly IMapper _mapper;

    public BrewerController(IBrewerService brewerService, IMapper mapper)
    {
        _brewerService = brewerService;
        _mapper = mapper;
    }

    [HttpPost("beers")]
    public async Task<ActionResult<Beer>> AddBeerAsync(AddBeerDTO beer)
    {
        var brewer = await _brewerService.GetBrewerAsync(beer.BrewerId);
        if (brewer == null)
        {
            return NotFound($"Brewer with ID {beer.BrewerId} not found");
        }

        var beerToAdd = _mapper.Map<Beer>(beer);

        var addedBeer = await _brewerService.AddBeerAsync(beerToAdd);

        return Ok(addedBeer);
    }

    [HttpDelete("beers/{id}")]
    public async Task<IActionResult> DeleteBeerAsync(Guid id)
    {
        var beerToDelete = await _brewerService.GetBeerAsync(id);
        if (beerToDelete == null)
        {
            return NotFound($"Beer with ID {id} not found");
        }

        var deleted = await _brewerService.DeleteBeerAsync(beerToDelete);
        if (deleted)
        {
            return Ok($"Beer with ID {id} deleted");
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

