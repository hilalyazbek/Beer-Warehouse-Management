using Iterates.Bwm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterates.Bwm.Infrastructure.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Brewer> Brewers { get; set; }
    public DbSet<Beer> Beers { get; set; }
    public DbSet<Wholesaler> Wholesalers { get; set; }
    public DbSet<WholesalerStock> WholesalerStocks { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public ApplicationDbContext() : base()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=tcp:sql-qa-stg-001.database.windows.net,1433;Initial Catalog=BWM;Persist Security Info=False;User ID=sqladmin;Password=P@ssw0rd;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WholesalerStock>()
            .HasKey(wb => new { wb.WholesalerId, wb.BeerId });

        ApplicationDbContextInitializer.SeedData(modelBuilder);
    }
}
