using System;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Application.Interfaces;

public interface IWholesalerService
{
    Task<IEnumerable<Wholesaler?>> GetWholesalersAsync();
    Task<Wholesaler?> GetByIdAsync(Guid id);
    Task<IEnumerable<WholesalerStock?>> GetStockByWholesalerIdAsync(Guid wholesalerId);
    Task<WholesalerStock?> GetStockByBeerIdAsync(Guid wholesalerId, Guid id);
    Task<WholesalerStock?> UpdateStockAsync(Sale sale);
    Task<WholesalerStock?> UpdateStockAsync(Guid wholesalerId, Guid beerId, int quantity);
    Task<QuotationResponse?> GetQuoteResponseAsync(QuotationRequest quoteRequest);
}

