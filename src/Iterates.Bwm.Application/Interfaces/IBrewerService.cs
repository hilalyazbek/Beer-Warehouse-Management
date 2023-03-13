using System;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Application.Interfaces;

public interface IBrewerService
{
    Task<IEnumerable<Beer>> GetBeersByBrewerIdAsync(Guid id);
    Task<Brewer> GetBrewerAsync(Guid id);
    Task<Beer> GetBeerAsync(Guid id);
    Task<Beer> AddBeerAsync(Beer beer);
    Task<bool> DeleteBeerAsync(Beer beer);
    Task<Sale> AddSaleToWholesalerAsync(Sale sale);
}

