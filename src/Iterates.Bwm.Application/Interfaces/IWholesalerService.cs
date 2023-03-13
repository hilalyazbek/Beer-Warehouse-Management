using System;
using Iterates.Bwm.Domain.Entities;

namespace Iterates.Bwm.Application.Interfaces;

public interface IWholesalerService
{
    Task<Wholesaler> GetByIdAsync(Guid id);
}

