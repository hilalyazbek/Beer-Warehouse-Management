using System;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Application.Interfaces;

public interface IBrewerService
{
    Task<Brewer> GetBrewerAsync(Guid id);
    Task<Beer> AddBeerAsync(Beer beer);
    Task<bool> DeleteBeerAsync(Beer beer);
    Task<WholesalerStock> AddSaleToWholesalerAsync(WholesalerStock wholesalerStock);
}

