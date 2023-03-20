# Iterates
Dotnet api built using dotnet core and entity framework.

### Solution Architecture and Design Pattern
This solution uses a variation of a Domain Driven Design and Repository / Service pattern

- Iterates.Bwm
    - src
        - Iterates.Bwm.Api
            (contains controllers, DTOs, Mappings, etc.)
        - Iterates.Bwm.Application
            (contains service classes)
        - Iterates.Bwm.Domain
            (contains Entities, Logging interfaces )
        - Iterates.Bwm.Infrastructure
            (contains Repository, DBContext, Logging Implementations, Migrations)
    - tests

### Repository / Service pattern
Similar to the Repository and Unit of Work pattern, I like to use a service class instead of the Unit of work class.
The service class handles the business logic and communicates with the Repository

```
public interface IBrewerService
{
    Task<IEnumerable<Brewer?>> GetAllBrewersAsync();
    Task<Brewer?> GetBrewerAsync(Guid id);
    Task<Beer?> GetBeerAsync(Guid beerId);
    Task<Beer?> GetBeerAsync(Guid bewerId, Guid beerId);
    Task<IEnumerable<Beer?>> GetBeersByBrewerIdAsync(Guid id);
    Task<Beer?> AddBeerAsync(Beer beer);
    Task<bool> DeleteBeerAsync(Beer beer);
    Task<Sale?> AddSaleToWholesalerAsync(Sale sale);
}
```

https://iterates.azurewebsites.net/brewers