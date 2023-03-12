using Iterates.Bwm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Infrastructure.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Brewer> Brewers { get; set; }
    public DbSet<Beer> Beers { get; set; }
    public DbSet<Wholesaler> Wholesalers { get; set; }
    public DbSet<BeerStock> BeerStocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=sql-qa-stg-001.database.windows.net;Database=BWM;User Id=sqladmin;Password=P@ssw0rd;Trust Server Certificate=true ");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beer>()
            .HasOne(b => b.Brewer)
            .WithMany(bw => bw.Beers)
            .IsRequired();

        modelBuilder.Entity<BeerStock>()
            .HasKey(bs => new { bs.WholesalerId, bs.BeerId });

        modelBuilder.Entity<BeerStock>()
            .HasOne(bs => bs.Wholesaler)
            .WithMany(w => w.BeerStocks)
            .HasForeignKey(bs => bs.WholesalerId);

        ApplicationDbContextInitializer.SeedData(modelBuilder);
    }
}
