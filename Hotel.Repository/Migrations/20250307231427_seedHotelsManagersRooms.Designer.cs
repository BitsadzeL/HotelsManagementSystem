﻿// <auto-generated />
using System;
using Hotels.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hotels.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250307231427_seedHotelsManagersRooms")]
    partial class seedHotelsManagersRooms
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hotels.Models.Entities.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Hotels.Models.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Plaza Ave",
                            City = "Tbilisi",
                            Country = "Georgia",
                            Rating = 4.5f,
                            Title = "Grand Plaza"
                        },
                        new
                        {
                            Id = 2,
                            Address = "456 Ocean St",
                            City = "Berlin",
                            Country = "Germany",
                            Rating = 4.1f,
                            Title = "Luxury Inn"
                        },
                        new
                        {
                            Id = 3,
                            Address = "789 Mountain Rd",
                            City = "Barcelona",
                            Country = "Spain",
                            Rating = 5f,
                            Title = "Barcelona Plaza"
                        });
                });

            modelBuilder.Entity("Hotels.Models.Entities.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasColumnType("char(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("HotelId")
                        .IsUnique();

                    b.HasIndex("IdNumber")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Managers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john.doe@example.com",
                            HotelId = 1,
                            IdNumber = "12345678901",
                            Name = "John",
                            PhoneNumber = "+995123456789",
                            Surname = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane.smith@example.com",
                            HotelId = 2,
                            IdNumber = "98765432101",
                            Name = "Jane",
                            PhoneNumber = "+995987654321",
                            Surname = "Smith"
                        },
                        new
                        {
                            Id = 3,
                            Email = "michael.jordan@example.com",
                            HotelId = 3,
                            IdNumber = "19283746501",
                            Name = "Michael",
                            PhoneNumber = "+995112233445",
                            Surname = "Jordan"
                        });
                });

            modelBuilder.Entity("Hotels.Models.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Hotels.Models.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFree")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HotelId = 1,
                            IsFree = false,
                            Price = 150f,
                            Title = "Deluxe Room"
                        },
                        new
                        {
                            Id = 2,
                            HotelId = 2,
                            IsFree = false,
                            Price = 100f,
                            Title = "Standard Room"
                        },
                        new
                        {
                            Id = 3,
                            HotelId = 3,
                            IsFree = false,
                            Price = 250f,
                            Title = "Suite"
                        },
                        new
                        {
                            Id = 4,
                            HotelId = 3,
                            IsFree = false,
                            Price = 500f,
                            Title = "Royal"
                        });
                });

            modelBuilder.Entity("Hotels.Models.Entities.Manager", b =>
                {
                    b.HasOne("Hotels.Models.Entities.Hotel", "Hotel")
                        .WithOne("Manager")
                        .HasForeignKey("Hotels.Models.Entities.Manager", "HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotels.Models.Entities.Room", b =>
                {
                    b.HasOne("Hotels.Models.Entities.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotels.Models.Entities.Hotel", b =>
                {
                    b.Navigation("Manager")
                        .IsRequired();

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
