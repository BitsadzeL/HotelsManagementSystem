using Hotels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using University.Repository.Data;

namespace Hotels.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {          
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ConfigureHotels();
            modelBuilder.ConfigureRooms();
            modelBuilder.ConfigureManagers();
            modelBuilder.ConfigureBookings();
            modelBuilder.ConfigureReservations();
            modelBuilder.ConfigureGuests();

            //modelBuilder.SeedHotels();
            //modelBuilder.SeedRooms();
            //modelBuilder.SeedManagers();


        }


    }
}
