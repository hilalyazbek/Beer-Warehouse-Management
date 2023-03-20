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

    public async Task<IEnumerable<Wholesaler?>> GetWholesalersAsync()
    {
        return await _wholesalerRepository.GetAllAsync();
    }

    /// <summary>
    /// It gets a wholesaler by id
    /// </summary>
    /// <param name="Guid">The unique identifier for the wholesaler</param>
    /// <returns>
    /// A wholesaler object.
    /// </returns>
    public async Task<Wholesaler?> GetByIdAsync(Guid id)
    {
        var wholesaler = await _wholesalerRepository.GetByIdAsync(id);
        if(wholesaler is null)
        {
            return null;
        }

        return wholesaler;
    }

    public async Task<IEnumerable<WholesalerStock?>> GetStockByWholesalerIdAsync(Guid wholesalerId)
    {
        var result = await _wholesalerStockRepository.FindAsync(itm => itm.WholesalerId == wholesalerId);
        if(result is null)
        {
            return null;
        }

        return result;
    }

    /// <summary>
    /// Get the first item from the list of wholesaler stock items that match the wholesaler id and beer
    /// id.
    /// </summary>
    /// <param name="Guid"></param>
    /// <param name="Guid"></param>
    /// <returns>
    /// A list of WholesalerStock objects.
    /// </returns>
    public async Task<WholesalerStock?> GetStockByBeerIdAsync(Guid wholesalerId, Guid id)
    {
        var beerStock = await _wholesalerStockRepository.FindAsync(
            itm => itm.WholesalerId == wholesalerId && itm.BeerId == id);

        return beerStock.FirstOrDefault();
    }

    /// <summary>
    /// If the stock exists, update it, otherwise add it
    /// </summary>
    /// <param name="Sale"></param>
    /// <returns>
    /// The method returns a Task<WholesalerStock?>
    /// </returns>
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

    /// <summary>
    /// > Update the stock of a beer for a wholesaler
    /// </summary>
    /// <param name="Guid">The unique identifier for the wholesaler</param>
    /// <param name="Guid">The unique identifier for the wholesaler</param>
    /// <param name="quantity">The amount of stock to add to the existing stock</param>
    /// <returns>
    /// The updated stock
    /// </returns>
    public async Task<WholesalerStock?> UpdateStockAsync(Guid wholesalerId, Guid beerId, int quantity)
    {
        var existingStock = await GetStockByBeerIdAsync(wholesalerId, beerId);
        if(existingStock is null)
        {
            return null;
        }

        existingStock.Stock += quantity;

        var updatedStock = await _wholesalerStockRepository.UpdateAsync(existingStock);

        return updatedStock;
    }

    /// <summary>
    /// It takes a quotation request, gets the quotation from the wholesaler, and returns a quotation
    /// response
    /// </summary>
    /// <param name="QuotationRequest"></param>
    /// <returns>
    /// A QuotationResponse object
    /// </returns>
    public async Task<QuotationResponse?> GetQuoteResponseAsync(QuotationRequest quoteRequest)
    {
        var result = new QuotationResponse
        {
            Wholesaler = quoteRequest.Wholesaler,
            Items = await GetQuotation(quoteRequest.Wholesaler.Id, quoteRequest.Items)
        };

        if(result.Items is null)
        {
            return null;
        }

        result.Description = $"Quotation generated for {result.Items.Count} items";

        return result;
    }

    /// <summary>
    /// It takes a list of items and a wholesaler id, and returns a list of items with the price and
    /// discount applied
    /// </summary>
    /// <param name="Guid">wholesalerId</param>
    /// <param name="items">List of items that the customer wants to buy</param>
    /// <returns>
    /// A list of ItemResponse objects.
    /// </returns>
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
                    PricePerItem = 0.0m,
                    TotalAfterDiscount = 0.0m,
                    TotalBeforeDiscount = 0.0m,
                    Quantity = 0
                });
                continue;
            }

            var discount = CheckDiscount(item.Quantity);
            var originalPrice = item.Quantity * currentStock.Price;
            var priceAfterDiscount = originalPrice;
            
            if (discount != 0)
            {
                priceAfterDiscount = originalPrice - (originalPrice * (discount / 100));
            }
            
            result.Add(new ItemResponse()
            {
                BeerId = currentStock.BeerId,
                Quantity = item.Quantity,
                Description = $"Beer is Available",
                PricePerItem = currentStock.Price,
                Discount = $"{discount} % applied",
                TotalBeforeDiscount = item.Quantity * currentStock.Price,
                TotalAfterDiscount = priceAfterDiscount
            });
        }

        return result;
    }

    /// <summary>
    /// If the quantity is greater than 20, return 20. If the quantity is greater than 10, return 10.
    /// Otherwise, return 0
    /// </summary>
    /// <param name="quantity">The number of items purchased.</param>
    /// <returns>
    /// The discount percentage.
    /// </returns>
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
