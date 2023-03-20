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
public class WholesalersController : Controller
{
    private readonly IWholesalerService _wholesalerService;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public WholesalersController(IWholesalerService wholesalerService,
        IMapper mapper,
        ILoggerManager logger)
    {
        _wholesalerService = wholesalerService;
        _mapper = mapper;
        _logger = logger;
    }

   /// <summary>
   /// It gets all the wholesalers from the database and returns them as a list of WholesalerDTO objects
   /// </summary>
   /// <returns>
   /// A list of wholesalers
   /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WholesalerDTO>>> GetWholesalers()
    {
        var wholesalers = await _wholesalerService.GetWholesalersAsync();
        if(wholesalers is null)
        {
            _logger.LogInfo($"GetWholesalers: returned null");
            return NotFound($"No wholesalers found");
        }

        var result = _mapper.Map<IEnumerable<WholesalerDTO>>(wholesalers);
        return Ok(result);
    }

    /// <summary>
    /// > GetStockByWholesalerId: returns a list of WholesalerStockDTO objects for a given wholesalerId
    /// </summary>
    /// <param name="Guid">a unique identifier</param>
    /// <returns>
    /// A list of WholesalerStockDTO objects
    /// </returns>
    [HttpGet("{wholesalerId}/stock")]
    public async Task<ActionResult<IEnumerable<WholesalerStockDTO>>> GetStockByWholesalerId(Guid wholesalerId)
    {
        var stock = await _wholesalerService.GetStockByWholesalerIdAsync(wholesalerId);
        if (stock is null)
        {
            _logger.LogInfo($"GetStockByWholesalerId: returned null for wholesaler {wholesalerId}");
            return NotFound($"No stock found for wholesaler {wholesalerId}");
        }

        var result = _mapper.Map<IEnumerable<WholesalerStockDTO>>(stock);
        return Ok(result);
    }

    /// <summary>
    /// It updates the stock of a beer in a wholesaler's stock
    /// </summary>
    /// <param name="Guid">wholesalerId</param>
    /// <param name="Guid">wholesalerId</param>
    /// <param name="stock">the new stock value</param>
    /// <returns>
    /// The updated stock for the beer in the wholesaler's stock.
    /// </returns>
    [HttpPut("{wholesalerId}/stock/{beerId}")]
    public async Task<ActionResult<WholesalerStockDTO>> UpdateBeerStock(Guid wholesalerId, Guid beerId, int stock)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var wholesaler = await _wholesalerService.GetByIdAsync(wholesalerId);
        if (wholesaler is null)
        {
            _logger.LogError($"GetByIdAsync: returned null, Wholesaler with id {wholesalerId}");
            return NotFound($"Wholesaler with id {wholesalerId} not found");
        }

        var wholesalerStock = await _wholesalerService.GetStockByBeerIdAsync(wholesalerId, beerId);
        if(wholesalerStock is null)
        {
            _logger.LogError($"GetStockByBeerIdAsync: returned null, beer {beerId} from wholesaler {wholesalerId}");
            return NotFound($"Beer {beerId} does not exist in wholesaler {wholesalerId} stock");
        }

        var updatedStock = await _wholesalerService.UpdateStockAsync(wholesalerStock.WholesalerId, wholesalerStock.BeerId, stock);
        if(updatedStock is null)
        {
            _logger.LogError($"UpdateStockAsync: returned null, Wholesaler {wholesalerId} and beer {beerId}");
            return BadRequest($"Could not update stock for wholesaler {wholesalerId} and beer {beerId}");
        }

        var result = _mapper.Map<WholesalerStockDTO>(updatedStock);

        return Ok(result);
    }

    /// <summary>
    /// It takes a wholesalerId and a QuotationRequestDTO object, validates the QuotationRequestDTO
    /// object, maps it to a QuotationRequest object, calls the GetQuoteResponseAsync function on the
    /// wholesalerService, maps the result to a QuotationResponseDTO object and returns it
    /// </summary>
    /// <param name="Guid">wholesalerId</param>
    /// <param name="QuotationRequestDTO"></param>
    /// <returns>
    /// A QuotationResponseDTO object
    /// </returns>
    [HttpPost("{wholesalerId}/quotes")]
    public async Task<ActionResult<QuotationResponseDTO>> GetQuote(Guid wholesalerId, QuotationRequestDTO quoteRequestDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var wholesaler = await _wholesalerService.GetByIdAsync(wholesalerId);
        if(wholesaler is null)
        {
            _logger.LogError($"GetByIdAsync: returned null, Wholesaler with id {wholesalerId}");
            return NotFound($"Wholesaler with id {wholesalerId} not found");
        }

        if(quoteRequestDTO.Items == null || quoteRequestDTO.Items.Count == 0)
        {
            _logger.LogError($"No items are added to the Items List");
            return BadRequest("Please add items to the quotation request");
        }

        var containsDuplicates = quoteRequestDTO.Items.GroupBy(itm => itm.BeerId).Any(g => g.Count() > 1);
        if (containsDuplicates)
        {
            _logger.LogError($"Request contains duplicate beers");
            return BadRequest("The quotation request contains duplicate beers");
        }

        var quoteRequest = _mapper.Map<QuotationRequest>(quoteRequestDTO);
        quoteRequest.Wholesaler = wholesaler;
        
        var quoteResponse = await _wholesalerService.GetQuoteResponseAsync(quoteRequest);
        if(quoteResponse is null)
        {
            _logger.LogError($"GetQuoteResponseAsync: returned null, quoteResponse");
            return BadRequest("Something went wrong");
        }

        var result = _mapper.Map<QuotationResponseDTO>(quoteResponse);

        return Ok(result);
    }

}

