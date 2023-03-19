using System;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces.Repositories;

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

    public async Task<WholesalerStock?> GetStockByBeerIdAsync(Guid wholesalerId, Guid id)
    {
        var beerStock = await _wholesalerStockRepository.FindAsync(
            itm => itm.WholesalerId == wholesalerId && itm.BeerId == id);

        return beerStock.FirstOrDefault();
    }

    public async Task<WholesalerStock?> UpdateStockAsync(Sale sale)
    {
        var beerId = sale.BeerId;
        var stock = sale.Stock;
        var price = sale.Price;
        var wholesalerId = sale.WholesalerId;

        var existingStock = await GetStockByBeerIdAsync(wholesalerId, beerId);

        if (existingStock is not null)
        {
            existingStock.Stock += stock;
            existingStock.Price = price;
            await _wholesalerStockRepository.UpdateAsync(existingStock);
        }
        else
        {
            var stockToAdd = new WholesalerStock()
            {
                BeerId = beerId,
                Stock = stock,
                Price = price
            };
            return await _wholesalerStockRepository.AddAsync(stockToAdd);
        }

        return null;
    }

    public async Task<WholesalerStock?> UpdateStockAsync(Guid wholesalerId, Guid beerId, int quantity)
    {
        var existingStock = await GetStockByBeerIdAsync(wholesalerId, beerId);
        existingStock.Stock += quantity;

        var updatedStock = await _wholesalerStockRepository.UpdateAsync(existingStock);

        return updatedStock;
    }

    public async Task<QuotationResponse?> GetQuoteResponseAsync(QuotationRequest quoteRequest)
    {
        var result = new QuotationResponse
        {
            Wholesaler = quoteRequest.Wholesaler,
            Items = await GetQuotation(quoteRequest.WholesalerId, quoteRequest.Items)
        };

        if(result.Items is null)
        {
            return null;
        }

        result.Description = $"Quotation generated for {result.Items.Count} items";

        return result;
    }

    private async Task<List<ItemResponse>?> GetQuotation(Guid wholesalerId, List<ItemRequest> items)
    {
        var result = new List<ItemResponse>();

        foreach (var item in items)
        {
            var query = await _wholesalerStockRepository.FindAsync(
                itm => itm.BeerId == item.BeerId
                && itm.WholesalerId == wholesalerId
                && item.Quantity <= itm.Stock);

            if (query is null)
            {
                return null;
            }

            var currentStock = query.FirstOrDefault();

            if (currentStock is null)
            {
                result.Add(new ItemResponse()
                {
                    BeerId = item.BeerId,
                    Description = "Beer not available in stock",
                    Discount = "NA",
                    Price = 0.0m,
                    PriceAfterDiscount = 0.0m,
                    PriceBeforeDiscount = 0.0m,
                    Quantity = 0
                });
                continue;
            }

            var discount = CheckDiscount(item.Quantity);
            var priceAfterDiscount = 0.0m;
            if (discount != 0)
            {
                var originalPrice = item.Quantity * currentStock.Price;
                priceAfterDiscount = originalPrice - (originalPrice * (discount / 100));
            }

            result.Add(new ItemResponse()
            {
                BeerId = currentStock.BeerId,
                Quantity = item.Quantity,
                Description = "Beer is Available",
                Discount = $"{discount} % applied",
                PriceBeforeDiscount = item.Quantity * currentStock.Price,
                PriceAfterDiscount = priceAfterDiscount
            });
        }

        return result;
    }

    private static decimal CheckDiscount(int quantity)
    {
        if (quantity > 20)
        {
            return 20;
        }
        else if (quantity > 10)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
}
