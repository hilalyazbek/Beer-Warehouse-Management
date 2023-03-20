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

### Api Calls
- Get All Brewers: https://iterates.azurewebsites.net/brewers

- Get All Beers by Brewer Id: https://iterates.azurewebsites.net/brewers/[BrewerId]/beers

- Add a Beer to a Brewer: https://iterates.azurewebsites.net/brewers/[BrewerId]/beers
    ```
    {
    "name": "Beer Name",
    "alcoholContent": "7,7%",
    "batchNumber": "BRU#982A"
    }
    ```

- Delete a Beer from a Brewer: https://iterates.azurewebsites.net/brewers/[BrewerId]/beers/[BeerId]

- Add a Sale of a Beer to a Wholesaler: 'https://iterates.azurewebsites.net/Brewers/[BrewerId]/sales
    ```
    {
    "wholesalerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "beerId": "[BeerId]",
    "orderNumber": "ON2341",
    "stock": 500,
    "price": 11.1,
    "delivery": true
    }
    ```

- Update Beer Stock by Wholesaler: https://iterates.azurewebsites.net/[WholesalerId]/stock/[BeerId]?stock=32

- Request Quote from Wholesaler: https://iterates.azurewebsites.net/[WholesalerId]/quote
    ```
    {
    "items": [
        {
        "beerId": "3fa85f64-5717-4562-BBBB-2c963f66afa6",
        "quantity": 10
        },
        {
        "beerId": "3fa85f64-5717-4562-AAAA-2c963f66afa6",
        "quantity": 400
        },
    ]
    }
    ```