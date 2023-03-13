using System;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces;

namespace Iterates.Bwm.Application.Services;

public class BrewerService : IBrewerService
{
    private readonly IGenericRepository<Brewer> _brewerRepository;
    private readonly IGenericRepository<Beer> _beerRepository;
    private readonly IGenericRepository<Wholesaler> _wholesalerRepository;
    private readonly IGenericRepository<WholesalerStock> _wholesalerStockRepository;

    public BrewerService(IGenericRepository<Brewer> brewerRepository,
                          IGenericRepository<Beer> beerRepository,
                          IGenericRepository<Wholesaler> wholesalerRepository,
                          IGenericRepository<WholesalerStock> wholesalerStockRepository)
    {
        _brewerRepository = brewerRepository;
        _beerRepository = beerRepository;
        _wholesalerRepository = wholesalerRepository;
        _wholesalerStockRepository = wholesalerStockRepository;
    }

    public async Task<Beer> AddBeerAsync(Beer beer)
    {
        var addedBeer = await _beerRepository.AddAsync(beer);
        return addedBeer;
    }

    public async Task<Brewer> GetBrewerAsync(Guid id)
    {
        var brewer = await _brewerRepository.GetByIdAsync(id);

        return brewer;
    }

    public async Task<Beer> GetBeerAsync(Guid id)
    {
        var beer = await _beerRepository.GetByIdAsync(id);

        return beer;
    }

    public async Task<bool> DeleteBeerAsync(Beer beer)
    {
        await _beerRepository.RemoveAsync(beer);
        return true;
    }

    public async Task<IEnumerable<Beer>> GetBeersByBrewerIdAsync(Guid id)
    {
        var beers = await _beerRepository.FindAsync(itm => itm.BrewerId == id);

        return beers;
    }

    public async Task<WholesalerStock> AddSaleToWholesalerAsync(WholesalerStock wholesalerStock)
    {
        var wholesaler = await _wholesalerRepository.GetByIdAsync(wholesalerStock.WholesalerId);
        if (wholesaler == null)
        {
            throw new ArgumentException($"Wholesaler with ID {wholesalerStock.WholesalerId} not found");
        }

        var beer = await _beerRepository.GetByIdAsync(wholesalerStock.BeerId);
        if (beer == null)
        {
            throw new ArgumentException($"Beer with ID {wholesalerStock.BeerId} not found");
        }

        if (wholesaler.WholesalerStocks.Any(s => s.BeerId == wholesalerStock.BeerId))
        {
            var existingStock = wholesaler.WholesalerStocks.First(s => s.BeerId == wholesalerStock.BeerId);
            existingStock.Stock += wholesalerStock.Stock;
            await _wholesalerStockRepository.UpdateAsync(existingStock);
            return existingStock;
        }
        else
        {
            wholesaler.WholesalerStocks.Add(wholesalerStock);
            var addedSale = await _wholesalerStockRepository.AddAsync(wholesalerStock);
            return addedSale;
        }
    }


}
