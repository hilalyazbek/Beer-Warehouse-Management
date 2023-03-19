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
public class WholesalerController : Controller
{
    private readonly IWholesalerService _wholesalerService;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public WholesalerController(IWholesalerService wholesalerService,
        IMapper mapper,
        ILoggerManager logger)
    {
        _wholesalerService = wholesalerService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPut("/{wholesalerId}/stock/{beerId}")]
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

    [HttpPost("/{wholesalerId}/quotes")]
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

