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
public class BrewersController : Controller
{
    private readonly IBrewerService _brewerService;
    private readonly IWholesalerService _wholesalerService;
    private readonly IMapper _mapper;

    public BrewersController(IBrewerService brewerService, IWholesalerService wholesalerService, IMapper mapper)
    {
        _brewerService = brewerService;
        _mapper = mapper;
        _wholesalerService = wholesalerService;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Brewer>>> GetBrewers()
    {
        var brewers = await _brewerService.GetAllBrewersAsync();
        if (brewers == null)
        {
            return NotFound($"No brewers added");
        }

        return Ok(brewers);
    }

    [HttpGet("{brewerId}/beers/")]
    public async Task<ActionResult<IEnumerable<Beer>>> GetBeersByBrewerId(Guid brewerId)
    {
        var beers = await _brewerService.GetBeersByBrewerIdAsync(brewerId);
        if (beers == null)
        {
            return NotFound($"No beers found for brewer {brewerId}");
        }

        return Ok(beers);
    }

    [HttpPost("{brewerId}/beers/")]
    public async Task<ActionResult<Beer>> AddBeerAsync(Guid brewerId, AddBeerDTO beer)
    {
        var brewer = await _brewerService.GetBrewerAsync(brewerId);
        if (brewer == null)
        {
            return NotFound($"Brewer with ID {brewerId} not found");
        }

        var beerToAdd = _mapper.Map<Beer>(beer);
        beerToAdd.BrewerId = brewerId;

        var addedBeer = await _brewerService.AddBeerAsync(beerToAdd);

        return Ok(addedBeer);
    }

    [HttpDelete("{brewerId}/beers/{id}")]
    public async Task<IActionResult> DeleteBeerAsync(Guid brewerId, Guid id)
    {
        var brewer = await _brewerService.GetBrewerAsync(brewerId);
        if(brewer is null)
        {
            return NotFound($"Brewer with id {brewerId} wnot found");
        }

        var beerToDelete = await _brewerService.GetBeerAsync(brewerId, id);
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

    [HttpPost("{brewerId}/sales/")]
    public async Task<ActionResult<Sale>> AddSaleToWholesaler(Guid brewerId, AddSaleDTO sale)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var brewer = await _brewerService.GetBrewerAsync(brewerId);
        if(brewer is null)
        {
            return NotFound($"Brewer with id {brewerId} was not found");
        }

        var wholesaler = await _wholesalerService.GetByIdAsync(sale.WholesalerId);
        if (wholesaler == null)
        {
            return NotFound($"Wholesaler with ID {sale.WholesalerId} not found");
        }

        var beer = await _brewerService.GetBeerAsync(brewerId, sale.BeerId);
        if (beer == null)
        {
            return NotFound($"Beer with ID {sale.BeerId} not found");
        }

        var saleToAdd = _mapper.Map<Sale>(sale);
        saleToAdd.Wholesaler = wholesaler;
        saleToAdd.Beer = beer;
        saleToAdd.BrewerId = brewerId;

        var addedSale = await _brewerService.AddSaleToWholesalerAsync(saleToAdd);
        if(addedSale is null)
        {
            return BadRequest($"Something went wrong when adding the sale of Beer {sale.BeerId} to Wholesaler {sale.WholesalerId}");
        }

        await _wholesalerService.UpdateStockAsync(addedSale.Wholesaler, addedSale.BeerId, addedSale.Stock);

        return Ok(addedSale);

    }
}

