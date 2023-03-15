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

    public async Task<WholesalerStock> UpdateStockAsync(Sale sale)
    {
        var beerId = sale.BeerId;
        var stock = sale.Stock;
        var price = sale.Price;

        var existingStock = await GetStocksByBeerIdAsync(beerId);

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

    public async Task<QuotationResponse?> GetQuoteResponseAsync(QuotationRequest quoteRequest)
    {
        if (quoteRequest.Items is null || quoteRequest.Items.Count() == 0)
        {
            return null;
        }

        var result = new QuotationResponse()
        {
            Wholesaler = quoteRequest.Wholesaler
        };
        
        result.Items = GetQuotation(quoteRequest.WholesalerId, quoteRequest.Items);

        result.Description = $"Quotation generated for {result.Items.Count} items";

        return result;
    }

    private List<ItemResponse> GetQuotation(Guid wholesalerId, List<ItemRequest> items)
    {
        var result = new List<ItemResponse>();

        foreach (var item in items)
        {
            var currentStock = _wholesalerStockRepository.FindAsync(
                itm => itm.BeerId == item.BeerId 
                && item.Quantity <= itm.Stock
                && itm.WholesalerId == wholesalerId).Result.FirstOrDefault();

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
                priceAfterDiscount = originalPrice - ((originalPrice / discount) / 100);
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

    private int CheckDiscount(int quantity)
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
