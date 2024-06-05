using Microsoft.EntityFrameworkCore;

namespace HotelBookingApplication.Models
{
    public class HotelBookingApplicationDbContext : DbContext
    {
        public HotelBookingApplicationDbContext(DbContextOptions<HotelBookingApplicationDbContext> options) : base(options) { }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomsTypes { get; set;}
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<GuestDetails> GuestDetails { get; set; } 
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ReservationDetails> ReservationsDetails { get; set; }
    }
}
