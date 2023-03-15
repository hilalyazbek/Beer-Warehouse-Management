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
public class WholesalerController : Controller
{
    private readonly IWholesalerService _wholesalerService;
    private readonly IMapper _mapper;

    public WholesalerController(IWholesalerService wholesalerService, IMapper mapper)
    {
        _mapper = mapper;
        _wholesalerService = wholesalerService;
    }

    [HttpPost("/quotes")]
    public async Task<ActionResult<QuotationResponseDTO>> GetQuote(QuotationRequestDTO quoteRequestDTO)
    {
        var wholesaler = await _wholesalerService.GetByIdAsync(quoteRequestDTO.WholesalerId);
        if(wholesaler is null)
        {
            return NotFound($"Wholesaler with id {quoteRequestDTO.WholesalerId} not found");
        }

        var containsDuplicates = quoteRequestDTO.Items.GroupBy(itm => itm.BeerId).Any(g => g.Count() > 1);
        if (containsDuplicates)
        {
            return BadRequest("The quotation request contains duplicate beers");
        }

        var quoteRequest = _mapper.Map<QuotationRequest>(quoteRequestDTO);
        quoteRequest.Wholesaler = wholesaler;

        var quoteResponse = await _wholesalerService.GetQuoteResponseAsync(quoteRequest);

        var result = _mapper.Map<QuotationResponseDTO>(quoteResponse);

        return Ok(result);
    }

}

