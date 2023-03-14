using Iterates.Bwm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Iterates.Bwm.Infrastructure.DbContexts;

internal class ApplicationDbContextInitializer
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
        {
            return;
        }

        modelBuilder.Entity<Brewer>().HasData(
            new Brewer {
                Id = Guid.Parse("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                Name = "Abbaye de Leffe"
            },
            new Brewer { Id = Guid.Parse("8c641b60-6d70-4fb3-94f0-e8f6c23e8535"),
                Name = "Brasserie de la Senne"
            }
        );

        modelBuilder.Entity<Beer>().HasData(
            new Beer {
                Id = Guid.Parse("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                BrewerId = Guid.Parse("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                Name = " Leffe Blonde",
                AlcoholContent = "6,6%",
                BatchNumber = "Batch #231"                
            }
        );

        modelBuilder.Entity<Sale>().HasData(
           new Sale
           {
               BrewerId = Guid.Parse("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
               BeerId = Guid.Parse("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
               WholesalerId = Guid.Parse("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
               OrderNumber = "#BRU241",
               Delivery = true,
               Price = 2.20m,
               Stock = 1000
           }
       );

        modelBuilder.Entity<Wholesaler>().HasData(
            new Wholesaler {
                Id = Guid.Parse("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
                Name = "GeneDrinks"
            },
            new Wholesaler {
                Id = Guid.Parse("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"),
                Name = "OneShot"
            }
        );

        modelBuilder.Entity<WholesalerStock>().HasData(
            new WholesalerStock {
                WholesalerId = Guid.Parse("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
                BeerId = Guid.Parse("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                Stock = 100
            },
            new WholesalerStock {
                WholesalerId = Guid.Parse("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"),
                BeerId = Guid.Parse("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                Stock = 500
            }
        );
    }
}