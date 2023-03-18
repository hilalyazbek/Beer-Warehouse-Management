using System;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Application.Interfaces;

public interface IBrewerService
{
    Task<IEnumerable<Brewer?>> GetAllBrewersAsync();
    Task<Brewer?> GetBrewerAsync(Guid id);
    Task<Beer?> GetBeerAsync(Guid beerId);
    Task<Beer?> GetBeerAsync(Guid bewerId, Guid beerId);
    Task<IEnumerable<Beer?>> GetBeersByBrewerIdAsync(Guid id);
    Task<Beer?> AddBeerAsync(Beer beer);
    Task<bool> DeleteBeerAsync(Beer beer);
    Task<Sale?> AddSaleToWholesalerAsync(Sale sale);
}

