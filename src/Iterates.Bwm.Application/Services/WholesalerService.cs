using System;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces;

namespace Iterates.Bwm.Application.Services;

public class WholesalerService : IWholesalerService
{
    private readonly IGenericRepository<Wholesaler> _wholesalerRepository;
    private readonly IGenericRepository<WholesalerStock> _wholesalerStockRepository;

    public WholesalerService(IGenericRepository<Wholesaler> wholesalerRepository,
        IGenericRepository<WholesalerStock> wholesalerStockRepository
        )
    {
        _wholesalerRepository = wholesalerRepository;
        _wholesalerStockRepository = wholesalerStockRepository;
    }

    public async Task<Wholesaler> GetByIdAsync(Guid id)
    {
        var wholesaler = await _wholesalerRepository.GetByIdAsync(id);

        return wholesaler;
    }

    public async Task<WholesalerStock> GetStocksByBeerIdAsync(Guid id)
    {
        var beerStock = await _wholesalerStockRepository.FindAsync(itm => itm.BeerId == id);

        return beerStock.FirstOrDefault();
    }

    public async Task<WholesalerStock> UpdateStockAsync(Wholesaler wholesaler, Guid beerId, int stock)
    {
        
        var existingStock = await GetStocksByBeerIdAsync(beerId);

        if (existingStock is not null)
        {
            existingStock.Stock += stock;
            await _wholesalerStockRepository.UpdateAsync(existingStock);
        }
        else
        {
            var stockToAdd = new WholesalerStock()
            {
                BeerId = beerId,
                Stock = stock
            };
            return await _wholesalerStockRepository.AddAsync(stockToAdd);
        }

        return null;
    }
}
