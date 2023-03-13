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
    private readonly IGenericRepository<Sale> _saleRepository;

    public BrewerService(IGenericRepository<Brewer> brewerRepository,
                          IGenericRepository<Beer> beerRepository,
                          IGenericRepository<Wholesaler> wholesalerRepository,
                          IGenericRepository<WholesalerStock> wholesalerStockRepository,
                          IGenericRepository<Sale> saleRepository)
    {
        _brewerRepository = brewerRepository;
        _beerRepository = beerRepository;
        _wholesalerRepository = wholesalerRepository;
        _wholesalerStockRepository = wholesalerStockRepository;
        _saleRepository = saleRepository;
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

    public async Task<Sale> AddSaleToWholesalerAsync(Sale sale)
    {
        var addedSale = await _saleRepository.AddAsync(sale);

        return addedSale;
    }
}