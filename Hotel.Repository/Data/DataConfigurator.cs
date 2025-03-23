using Hotels.Models.Entities;
using Microsoft.AspNetCore.Identity;
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
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Id)
                    .ValueGeneratedNever()
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
                    .HasForeignKey(b => b.GuestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Reservation)
                    .WithMany(r => r.Bookings)
                    .HasForeignKey(b => b.ReservationId)
                    .OnDelete(DeleteBehavior.Cascade);
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
                    .ValueGeneratedNever()
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


        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole<int> { Id = 2, Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole<int> { Id = 3, Name = "Admin", NormalizedName = "ADMIN" }
            );
        }


        //public static void SeedManagers(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Manager>().HasData(
        //        new Manager { Id = 1, Name = "John", Surname = "Doe", IdNumber = "A1234567", Email = "john.doe@hotel.com", PhoneNumber = "123-456-7890", HotelId = 1 },
        //        new Manager { Id = 2, Name = "Jane", Surname = "Smith", IdNumber = "B2345678", Email = "jane.smith@hotel.com", PhoneNumber = "234-567-8901", HotelId = 2 },
        //        new Manager { Id = 3, Name = "Tom", Surname = "Johnson", IdNumber = "C3456789", Email = "tom.johnson@hotel.com", PhoneNumber = "345-678-9012", HotelId = 3 },
        //        new Manager { Id = 4, Name = "Emily", Surname = "Brown", IdNumber = "D4567890", Email = "emily.brown@hotel.com", PhoneNumber = "456-789-0123", HotelId = 4 }
        //    );
        //}


        //public static void SeedHotels(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Hotel>().HasData(
        //        new Hotel { Id = 1, Title = "Seaside Resort", Rating = 4.5f, Country = "USA", City = "Miami", Address = "123 Beach Ave" },
        //        new Hotel { Id = 2, Title = "Mountain View Hotel", Rating = 4.0f, Country = "USA", City = "Denver", Address = "456 Mountain Rd" },
        //        new Hotel { Id = 3, Title = "City Center Inn", Rating = 3.8f, Country = "Canada", City = "Toronto", Address = "789 City St" },
        //        new Hotel { Id = 4, Title = "Lakeside Retreat", Rating = 4.7f, Country = "Canada", City = "Vancouver", Address = "101 Lakeshore Blvd" }
        //    );
        //}



        //public static void SeedRooms(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Room>().HasData(
        //        new Room { Id = 1, Title = "Ocean View Suite", IsFree = true, Price = 200, HotelId = 1 },
        //        new Room { Id = 2, Title = "Mountain View Suite", IsFree = false, Price = 250, HotelId = 2 },
        //        new Room { Id = 3, Title = "Standard Room", IsFree = true, Price = 150, HotelId = 3 },
        //        new Room { Id = 4, Title = "Lakefront Suite", IsFree = true, Price = 300, HotelId = 4 }
        //    );
        //}


        //public static void SeedGuests(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Guest>().HasData(
        //        new Guest { Id = 5, Name = "Alice", Surname = "Smith", IdNumber = "98765432101", PhoneNumber = "321-654-9870" },
        //        new Guest { Id = 6, Name = "Bob", Surname = "Jones", IdNumber = "12345678901", PhoneNumber = "432-765-0987" },
        //        new Guest { Id = 7, Name = "Charlie", Surname = "Davis", IdNumber = "23456789012", PhoneNumber = "543-876-1098" },
        //        new Guest { Id = 8, Name = "David", Surname = "Martinez", IdNumber = "34567890123", PhoneNumber = "654-987-2109" }
        //    );
        //}


        //public static void SeedReservations(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Reservation>().HasData(
        //        new Reservation { Id = 1, CheckIn = new DateTime(2025, 5, 1), CheckOut = new DateTime(2025, 5, 7), RoomId = 1 },
        //        new Reservation { Id = 2, CheckIn = new DateTime(2025, 6, 15), CheckOut = new DateTime(2025, 6, 20), RoomId = 2 },
        //        new Reservation { Id = 3, CheckIn = new DateTime(2025, 7, 10), CheckOut = new DateTime(2025, 7, 12), RoomId = 3 },
        //        new Reservation { Id = 4, CheckIn = new DateTime(2025, 8, 5), CheckOut = new DateTime(2025, 8, 8), RoomId = 4 }
        //    );
        //}



        //public static void SeedBookings(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Booking>().HasData(
        //        new Booking { Id = 1, GuestId = 5, ReservationId = 1 },
        //        new Booking { Id = 2, GuestId = 6, ReservationId = 2 },
        //        new Booking { Id = 3, GuestId = 7, ReservationId = 3 },
        //        new Booking { Id = 4, GuestId = 8, ReservationId = 4 }
        //    );
        //}






    }
}