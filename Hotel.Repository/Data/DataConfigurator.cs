using Hotels.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace University.Repository.Data
{
    public static class DataConfigurator
    {
        public static void ConfigureHotels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(h => h.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(h => h.Rating)
                    .IsRequired();

                entity.Property(h=>h.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(h => h.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(h => h.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity
                    .HasOne(h => h.Manager)
                    .WithOne(m => m.Hotel)
                    .HasForeignKey<Manager>(m=>m.HotelId);


                entity
                    .HasMany(h => h.Rooms)      
                    .WithOne(r=>r.Hotel)
                    .HasForeignKey(r=>r.HotelId);

            });



        }

        public static void ConfigureRooms(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>(entity =>
                {
                    entity.HasKey(r => r.Id);
                    entity.Property(h => h.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();

                    entity.Property(r => r.Title)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(r => r.IsFree)
                        .HasDefaultValue(true);

                    entity.Property(r=>r.Price)
                        .IsRequired();

                    entity
                        .HasOne(r=>r.Hotel)
                        .WithMany(h=>h.Rooms)
                        .HasForeignKey(r=>r.HotelId);


                }
                );
        }

        public static void ConfigureManagers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(50);


                entity.Property(m => m.Surname)
                    .IsRequired()
                    .HasMaxLength(50);


                entity.Property(m => m.IdNumber)
                    .IsRequired()
                    .HasColumnType("char(11)");

                entity.Property(m => m.Email)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(m => m.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(30);


                entity.Property(m => m.HotelId)
                    .HasDefaultValue(null);


                entity.HasIndex(m => m.IdNumber).IsUnique();
                entity.HasIndex(m => m.Email).IsUnique();
                entity.HasIndex(m => m.PhoneNumber).IsUnique();

                entity
                    .HasOne(m => m.Hotel)
                    .WithOne(h => h.Manager)
                    .HasForeignKey<Manager>(m => m.HotelId);





            }
                );
        }

        public static void ConfigureBookings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.HasOne(b => b.Guest)
                    .WithMany(g => g.Bookings)
                    .HasForeignKey(b => b.GuestId);

                entity.HasOne(b => b.Reservation)
                    .WithMany(r => r.Bookings)
                    .HasForeignKey(b => b.ReservationId);
            });
        }

        public static void ConfigureReservations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasOne(r => r.Room)
                    .WithMany(r => r.Reservations)
                    .HasForeignKey(r => r.RoomId);

                entity.Property(r=>r.CheckIn)
                    .IsRequired();

                entity.Property(r => r.CheckOut)
                    .IsRequired();

                entity.Property(r => r.RoomId)
                    .IsRequired();
            });
        }

        public static void ConfigureGuests(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(g => g.Surname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(g => g.IdNumber)
                    .IsRequired()
                    .HasColumnType("char(11)");

                entity.Property(g => g.PhoneNumber)
                    .IsRequired();


                entity.HasIndex(g => g.IdNumber).IsUnique();
                entity.HasIndex(g => g.PhoneNumber).IsUnique();


            });

        }
        public static void SeedHotels( this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Title = "Grand Plaza",
                    Rating = 4.5f,
                    Country = "Georgia",
                    City = "Tbilisi",
                    Address = "123 Plaza Ave"
                },
                new Hotel
                {
                    Id = 2,
                    Title = "Luxury Inn",
                    Rating = 4.1f,
                    Country = "Germany",
                    City = "Berlin",
                    Address = "456 Ocean St"
                },
                new Hotel
                {
                    Id = 3,
                    Title = "Barcelona Plaza",
                    Rating = 5.0f,
                    Country = "Spain",
                    City = "Barcelona",
                    Address = "789 Mountain Rd"
                }
            );
        }

        public static void SeedRooms( this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
               new Room
               {
                   Id = 1,
                   Title = "Deluxe Room",
                   Price = 150.00f,
                   HotelId = 1
               },
               new Room
               {
                   Id = 2,
                   Title = "Standard Room",
                   Price = 100.00f,
                   HotelId = 2
               },
               new Room
               {
                   Id = 3,
                   Title = "Suite",
                   Price = 250.00f,
                   HotelId = 3
               },

               new Room
               {
                   Id = 4,
                   Title = "Royal",
                   Price = 500.00f,
                   HotelId = 3
               }
           );
        }

        public static void SeedManagers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    Id = 1,
                    Name = "John",
                    Surname = "Doe",
                    IdNumber = "12345678901",
                    Email = "john.doe@example.com",
                    PhoneNumber = "+995123456789",
                    HotelId = 1
                },
                new Manager
                {
                    Id = 2,
                    Name = "Jane",
                    Surname = "Smith",
                    IdNumber = "98765432101",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "+995987654321",
                    HotelId = 2
                },
                new Manager
                {
                    Id = 3,
                    Name = "Michael",
                    Surname = "Jordan",
                    IdNumber = "19283746501",
                    Email = "michael.jordan@example.com",
                    PhoneNumber = "+995112233445",
                    HotelId = 3
                }
            );
        }

    }
}