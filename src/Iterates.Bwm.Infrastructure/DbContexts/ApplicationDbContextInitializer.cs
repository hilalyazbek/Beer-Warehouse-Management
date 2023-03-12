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

        modelBuilder.Entity<Wholesaler>().HasData(
            new Wholesaler
            {
            });
    }
}