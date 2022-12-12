﻿// <auto-generated />
using System;
using CaveManager.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CaveManager.Migrations
{
    [DbContext(typeof(CaveManagerContext))]
    [Migration("20221211205225_AdressChanged")]
    partial class AdressChanged
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CaveManager.Entities.Cave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Cave");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "BatCave",
                            OwnerId = 2
                        },
                        new
                        {
                            Id = 2,
                            Name = "ThomCave",
                            OwnerId = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cavaleo",
                            OwnerId = 1
                        });
                });

            modelBuilder.Entity("CaveManager.Entities.Drawer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CaveId")
                        .HasColumnType("int");

                    b.Property<int>("MaxPlace")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlaceUsed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CaveId");

                    b.ToTable("Drawer");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CaveId = 1,
                            MaxPlace = 10,
                            Name = "Pomme",
                            PlaceUsed = 2
                        },
                        new
                        {
                            Id = 2,
                            CaveId = 2,
                            MaxPlace = 10,
                            Name = "Poire",
                            PlaceUsed = 1
                        },
                        new
                        {
                            Id = 3,
                            CaveId = 1,
                            MaxPlace = 10,
                            Name = "Banana",
                            PlaceUsed = 0
                        });
                });

            modelBuilder.Entity("CaveManager.Entities.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCGUAccepted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber3")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Owner");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "wil@gmail.com",
                            FirstName = "Wil",
                            IsCGUAccepted = true,
                            LastName = "TF",
                            Password = "MelmanoucheA9"
                        },
                        new
                        {
                            Id = 2,
                            Email = "leo@gmail.com",
                            FirstName = "Leo",
                            IsCGUAccepted = true,
                            LastName = "SMaster",
                            Password = "1v9A"
                        },
                        new
                        {
                            Id = 3,
                            Email = "thom@gmail.com",
                            FirstName = "Thom",
                            IsCGUAccepted = false,
                            LastName = "PokFan",
                            Password = "DAzE2"
                        });
                });

            modelBuilder.Entity("CaveManager.Entities.Wine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Bottling")
                        .HasColumnType("int");

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DrawerId")
                        .HasColumnType("int");

                    b.Property<int?>("MaxVintageRecommended")
                        .HasColumnType("int");

                    b.Property<int?>("MinVintageRecommended")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DrawerId");

                    b.ToTable("Wine");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bottling = 2019,
                            DrawerId = 1,
                            MaxVintageRecommended = 8,
                            MinVintageRecommended = 2,
                            Name = "Vin de fou",
                            Type = "Red Wine"
                        },
                        new
                        {
                            Id = 2,
                            Bottling = 2011,
                            DrawerId = 1,
                            MaxVintageRecommended = 8,
                            MinVintageRecommended = 4,
                            Name = "Vin pas fou",
                            Type = "Rosé Wine"
                        },
                        new
                        {
                            Id = 3,
                            Bottling = 2012,
                            DrawerId = 2,
                            MaxVintageRecommended = 12,
                            MinVintageRecommended = 10,
                            Name = "Vin de fou pas fou",
                            Type = "White Wine"
                        });
                });

            modelBuilder.Entity("CaveManager.Entities.Cave", b =>
                {
                    b.HasOne("CaveManager.Entities.Owner", "Owner")
                        .WithMany("Caves")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CaveManager.Entities.Drawer", b =>
                {
                    b.HasOne("CaveManager.Entities.Cave", "Cave")
                        .WithMany("Drawer")
                        .HasForeignKey("CaveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cave");
                });

            modelBuilder.Entity("CaveManager.Entities.Wine", b =>
                {
                    b.HasOne("CaveManager.Entities.Drawer", "Drawer")
                        .WithMany("Wines")
                        .HasForeignKey("DrawerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drawer");
                });

            modelBuilder.Entity("CaveManager.Entities.Cave", b =>
                {
                    b.Navigation("Drawer");
                });

            modelBuilder.Entity("CaveManager.Entities.Drawer", b =>
                {
                    b.Navigation("Wines");
                });

            modelBuilder.Entity("CaveManager.Entities.Owner", b =>
                {
                    b.Navigation("Caves");
                });
#pragma warning restore 612, 618
        }
    }
}