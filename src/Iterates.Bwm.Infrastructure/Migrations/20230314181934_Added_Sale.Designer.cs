﻿// <auto-generated />
using System;
using Iterates.Bwm.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Iterates.Bwm.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230314181934_Added_Sale")]
    partial class Added_Sale
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Iterates.Bwm.Domain.Entities.Beer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AlcoholContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BatchNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BrewerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Beers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                            AlcoholContent = "6,6%",
                            BatchNumber = "Batch #231",
                            BrewerId = new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1800),
                            Name = " Leffe Blonde",
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1800)
                        });
                });

            modelBuilder.Entity("Iterates.Bwm.Domain.Entities.Brewer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Brewers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1630),
                            Name = "Abbaye de Leffe",
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1660)
                        },
                        new
                        {
                            Id = new Guid("8c641b60-6d70-4fb3-94f0-e8f6c23e8535"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1700),
                            Name = "Brasserie de la Senne",
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1700)
                        });
                });

            modelBuilder.Entity("Iterates.Bwm.Domain.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BrewerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Delivery")
                        .HasColumnType("bit");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WholesalerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Sales");

                    b.HasData(
                        new
                        {
                            Id = new Guid("499e866c-d0fd-4c5e-9856-6720c350c714"),
                            BeerId = new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                            BrewerId = new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1840),
                            Delivery = true,
                            OrderNumber = "#BRU241",
                            Price = 2.20m,
                            Stock = 1000,
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1840),
                            WholesalerId = new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67")
                        });
                });

            modelBuilder.Entity("Iterates.Bwm.Domain.Entities.Wholesaler", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1880),
                            Name = "GeneDrinks",
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1880)
                        },
                        new
                        {
                            Id = new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1890),
                            Name = "OneShot",
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1890)
                        });
                });

            modelBuilder.Entity("Iterates.Bwm.Domain.Entities.WholesalerStock", b =>
                {
                    b.Property<Guid>("WholesalerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("WholesalerId", "BeerId");

                    b.HasIndex("BeerId");

                    b.ToTable("WholesalerStocks");

                    b.HasData(
                        new
                        {
                            WholesalerId = new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
                            BeerId = new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1920),
                            Id = new Guid("78c128c3-0ebd-464d-8b2b-7bbac3b17604"),
                            Price = 2.2m,
                            Stock = 100,
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1920)
                        },
                        new
                        {
                            WholesalerId = new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"),
                            BeerId = new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                            Created = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1940),
                            Id = new Guid("21e93c96-e2b0-4f02-af10-fbd31555b983"),
                            Price = 2.3m,
                            Stock = 500,
                            Updated = new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1940)
                        });
                });

            modelBuilder.Entity("Iterates.Bwm.Domain.Entities.Sale", b =>
                {
                    b.HasOne("Iterates.Bwm.Domain.Entities.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Iterates.Bwm.Domain.Entities.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("Iterates.Bwm.Domain.Entities.WholesalerStock", b =>
                {
                    b.HasOne("Iterates.Bwm.Domain.Entities.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Iterates.Bwm.Domain.Entities.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });
#pragma warning restore 612, 618
        }
    }
}