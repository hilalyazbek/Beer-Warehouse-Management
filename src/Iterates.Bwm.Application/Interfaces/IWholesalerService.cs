using System;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Application.Interfaces;

public interface IWholesalerService
{
    Task<Wholesaler> GetByIdAsync(Guid id);
    Task<WholesalerStock?> GetStockByBeerIdAsync(Guid wholesalerId, Guid id);
    Task<WholesalerStock?> UpdateStockAsync(Sale sale);
    Task<WholesalerStock?> UpdateStockAsync(Guid wholesalerId, Guid beerId, int quantity);
    Task<QuotationResponse?> GetQuoteResponseAsync(QuotationRequest quoteRequest);
}

