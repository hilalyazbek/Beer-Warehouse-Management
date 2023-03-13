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
    private readonly IWholesalerService _wholesalerService;
    private readonly IMapper _mapper;

    public BrewerController(IBrewerService brewerService, IWholesalerService wholesalerService, IMapper mapper)
    {
        _brewerService = brewerService;
        _mapper = mapper;
        _wholesalerService = wholesalerService;
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

    [HttpPost("/sales/")]
    public async Task<ActionResult<Sale>> AddSaleToWholesaler(AddSaleDTO sale)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var wholesaler = await _wholesalerService.GetByIdAsync(sale.WholesalerId);
        if (wholesaler == null)
        {
            throw new ArgumentException($"Wholesaler with ID {sale.WholesalerId} not found");
        }

        var beer = await _brewerService.GetBeerAsync(sale.BeerId);
        if (beer == null)
        {
            throw new ArgumentException($"Beer with ID {sale.BeerId} not found");
        }

        var saleToAdd = _mapper.Map<Sale>(sale);
        saleToAdd.Wholesaler = wholesaler;
        saleToAdd.Beer = beer;

        var addedSale = await _brewerService.AddSaleToWholesalerAsync(saleToAdd);
        if(addedSale is null)
        {
            return BadRequest($"Something went wrong when adding the sale of Beer {sale.BeerId} to Wholesaler {sale.WholesalerId}");
        }

        await _wholesalerService.UpdateStockAsync(addedSale.Wholesaler, addedSale.BeerId, addedSale.Stock);

        return Ok(addedSale);

    }
}

