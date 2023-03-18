using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Iterates.Bwm.Api.DTOs;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces.Logging;
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
    private readonly ILoggerManager _logger;

    public BrewersController(IBrewerService brewerService,
        IWholesalerService wholesalerService,
        IMapper mapper,
        ILoggerManager logger)
    {
        _brewerService = brewerService;
        _wholesalerService = wholesalerService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<BrewerDTO>>> GetBrewers()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var brewers = await _brewerService.GetAllBrewersAsync();
        if (brewers is null)
        {
            _logger.LogError($"No brewers added the database");
            return NotFound($"No brewers added");
        }

        var result = _mapper.Map<IEnumerable<BrewerDTO>>(brewers);
        return Ok(result);
    }

    [HttpGet("{brewerId}/beers/")]
    public async Task<ActionResult<IEnumerable<BeerDTO>>> GetBeersByBrewerId(Guid brewerId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var beers = await _brewerService.GetBeersByBrewerIdAsync(brewerId);
        if (beers is null)
        {
            _logger.LogError($"Brewer with id {brewerId} does not exist in the database");
            return NotFound($"No beers found for brewer {brewerId}");
        }

        var result = _mapper.Map<IEnumerable<BeerDTO>>(beers);
        return Ok(result);
    }

    [HttpPost("{brewerId}/beers/")]
    public async Task<ActionResult<BeerDTO>> AddBeer(Guid brewerId, AddBeerDTO beer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var brewer = await _brewerService.GetBrewerAsync(brewerId);
        if (brewer is null)
        {
            _logger.LogError($"Brewer with id {brewerId} does not exist in the database");
            return NotFound($"Brewer with ID {brewerId} not found");
        }

        var beerToAdd = _mapper.Map<Beer>(beer);
        beerToAdd.BrewerId = brewerId;

        var addedBeer = await _brewerService.AddBeerAsync(beerToAdd);
        if(addedBeer is null)
        {
            _logger.LogError($"Adding beer with id {beer.Name} to bewer {brewer.Name }returned null");
            return BadRequest("Something went wrong");
        }

        var result = _mapper.Map<BeerDTO>(beer);
        return Ok(result);
    }

    [HttpDelete("{brewerId}/beers/{id}")]
    public async Task<IActionResult> DeleteBeer(Guid brewerId, Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var brewer = await _brewerService.GetBrewerAsync(brewerId);
        if(brewer is null)
        {
            _logger.LogError($"Brewer with id {brewerId} does not exist in the database");
            return NotFound($"Brewer with id {brewerId} wnot found");
        }

        var beerToDelete = await _brewerService.GetBeerAsync(brewerId, id);
        if (beerToDelete is null)
        {
            _logger.LogError($"Beer with id {id} does not exist in the database");
            return NotFound($"Beer with ID {id} not found");
        }

        var deleted = await _brewerService.DeleteBeerAsync(beerToDelete);
        if (deleted)
        {
            return Ok($"Beer with ID {id} deleted");
        }
        else
        {
            _logger.LogError($"Deleting beer {id} from brewer {brewer.Name} returned false");
            return NotFound();
        }
    }

    [HttpPost("{brewerId}/sales/")]
    public async Task<ActionResult<ViewSaleDTO>> AddSaleToWholesaler(Guid brewerId, AddSaleDTO sale)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var brewer = await _brewerService.GetBrewerAsync(brewerId);
        if(brewer is null)
        {
            _logger.LogError($"Brewer with id {brewerId} does not exist in the database");
            return NotFound($"Brewer with id {brewerId} was not found");
        }

        var wholesaler = await _wholesalerService.GetByIdAsync(sale.WholesalerId);
        if (wholesaler is null)
        {
            _logger.LogError($"Wholesaler with id {sale.WholesalerId} does not exist in the database");
            return NotFound($"Wholesaler with ID {sale.WholesalerId} not found");
        }

        var beer = await _brewerService.GetBeerAsync(brewerId, sale.BeerId);
        if (beer is null)
        {
            _logger.LogError($"Beer with id {sale.BeerId} does not exist in the database");
            return NotFound($"Beer with ID {sale.BeerId} not found");
        }

        var saleToAdd = _mapper.Map<Sale>(sale);
        saleToAdd.Wholesaler = wholesaler;
        saleToAdd.Beer = beer;
        saleToAdd.BrewerId = brewerId;

        var addedSale = await _brewerService.AddSaleToWholesalerAsync(saleToAdd);
        if(addedSale is null)
        {
            _logger.LogError($"Adding the sale of Beer {sale.BeerId} to Wholesaler {sale.WholesalerId} returned null");
            return BadRequest($"Something went wrong when adding the sale of Beer {sale.BeerId} to Wholesaler {sale.WholesalerId}");
        }

        var wholesalerStock = await _wholesalerService.UpdateStockAsync(addedSale);
        if(wholesalerStock is null)
        {
            _logger.LogError($"UpdateStockAsync: the function returned a null object");
            return BadRequest($"Something went wrong when adding the sale of Beer {sale.BeerId} to Wholesaler {sale.WholesalerId}");
        }

        var result = _mapper.Map<ViewSaleDTO>(addedSale);
        return Ok(result);
    }
}

