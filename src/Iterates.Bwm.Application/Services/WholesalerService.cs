using System;
using Iterates.Bwm.Application.Interfaces;
using Iterates.Bwm.Domain.Entities;
using Iterates.Bwm.Domain.Interfaces;

namespace Iterates.Bwm.Application.Services;

public class WholesalerService : IWholesalerService
{
    private readonly IGenericRepository<Wholesaler> _wholesalerRepository;

    public WholesalerService(
        IGenericRepository<Wholesaler> wholesalerRepository
        )
    {
        _wholesalerRepository = wholesalerRepository;
    }

    public async Task<Wholesaler> GetByIdAsync(Guid id)
    {
        var wholesaler = await _wholesalerRepository.GetByIdAsync(id);

        return wholesaler;
    }

}
