using System;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Application.Interfaces;

public interface IWholesalerService
{
    Task<Wholesaler> GetByIdAsync(Guid id);
    Task<WholesalerStock> GetStocksByBeerIdAsync(Guid id);
    Task<WholesalerStock> UpdateStockAsync(Wholesaler wholesaler, Guid beerId, int stock);
}

