using System;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces.Repositories;

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

    /// <summary>
    /// It returns a list of all brewers from the database
    /// </summary>
    /// <returns>
    /// A list of Brewers
    /// </returns>
    public async Task<IEnumerable<Brewer?>> GetAllBrewersAsync()
    {
        return await _brewerRepository.GetAllAsync();
    }

    /// <summary>
    /// > This function returns a brewer from the database
    /// </summary>
    /// <param name="Guid">The id of the brewer</param>
    /// <returns>
    /// A Brewer object.
    /// </returns>
    public async Task<Brewer?> GetBrewerAsync(Guid id)
    {
        var brewer = await _brewerRepository.GetByIdAsync(id);

        return brewer;
    }

    /// <summary>
    /// > This function adds a beer to the database
    /// </summary>
    /// <param name="Beer">The beer object that we want to add to the database.</param>
    /// <returns>
    /// The added beer.
    /// </returns>
    public async Task<Beer?> AddBeerAsync(Beer beer)
    {
        var addedBeer = await _beerRepository.AddAsync(beer);

        return addedBeer;
    }

    /// <summary>
    /// > Get a beer by its ID
    /// </summary>
    /// <param name="Guid">beerId</param>
    /// <returns>
    /// A beer object
    /// </returns>
    public async Task<Beer?> GetBeerAsync(Guid beerId)
    {
        var beer = await _beerRepository.GetByIdAsync(beerId);

        return beer;
    }

    /// <summary>
    /// > Get a beer from the database
    /// </summary>
    /// <param name="Guid">bewerId - The id of the brewer</param>
    /// <param name="Guid">bewerId</param>
    /// <returns>
    /// A beer object
    /// </returns>
    public async Task<Beer?> GetBeerAsync(Guid bewerId, Guid beerId)
    {
        var beer = await _beerRepository.FindAsync(itm => itm.BrewerId == bewerId && itm.Id == beerId);

        if(beer is null)
        {
            return null;
        }

        return beer.FirstOrDefault();
    }

    /// <summary>
    /// This function deletes a beer from the database.
    /// </summary>
    /// <param name="Beer">The beer object that is being passed in.</param>
    /// <returns>
    /// A boolean value.
    /// </returns>
    public async Task<bool> DeleteBeerAsync(Beer beer)
    {
        await _beerRepository.RemoveAsync(beer);

        return true;
    }

    /// <summary>
    /// > This function returns a list of beers that are associated with a brewer
    /// </summary>
    /// <param name="Guid">The id of the brewer</param>
    /// <returns>
    /// A list of beers that are associated with the brewer id.
    /// </returns>
    public async Task<IEnumerable<Beer?>> GetBeersByBrewerIdAsync(Guid id)
    {
        var beers = await _beerRepository.FindAsync(itm => itm.BrewerId == id);

        return beers;
    }

    /// <summary>
    /// It adds a sale to the wholesaler's database
    /// </summary>
    /// <param name="Sale">This is the sale object that is being added to the database.</param>
    /// <returns>
    /// The addedSale is being returned.
    /// </returns>
    public async Task<Sale?> AddSaleToWholesalerAsync(Sale sale)
    {
        var addedSale = await _saleRepository.AddAsync(sale);

        return addedSale;
    }
}