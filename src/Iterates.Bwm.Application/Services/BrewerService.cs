using System;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces;

namespace Iterates.Bwm.Application.Services;

public class BrewerService : IBrewerService
{
    private readonly IGenericRepository<Brewer> _brewerRepository;
    private readonly IGenericRepository<Beer> _beerRepository;
    private readonly IGenericRepository<Sale> _saleRepository;

    public BrewerService(IGenericRepository<Brewer> brewerRepository,
                          IGenericRepository<Beer> beerRepository,
                          IGenericRepository<Sale> saleRepository)
    {
        _brewerRepository = brewerRepository;
        _beerRepository = beerRepository;
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<Brewer>> GetAllBrewersAsync()
    {
        return await _brewerRepository.GetAllAsync();
    }

    public async Task<Brewer> GetBrewerAsync(Guid id)
    {
        var brewer = await _brewerRepository.GetByIdAsync(id);

        return brewer;
    }

    public async Task<Beer> AddBeerAsync(Beer beer)
    {
        var addedBeer = await _beerRepository.AddAsync(beer);
        return addedBeer;
    }

    public async Task<Beer?> GetBeerAsync(Guid bewerId, Guid beerId)
    {
        var beer = await _beerRepository.FindAsync(itm => itm.BrewerId == bewerId && itm.Id == beerId);

        if(beer is null)
        {
            return null;
        }

        return beer.FirstOrDefault();
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

    public async Task<Sale> AddSaleToWholesalerAsync(Sale sale)
    {
        var addedSale = await _saleRepository.AddAsync(sale);

        return addedSale;
    }


}