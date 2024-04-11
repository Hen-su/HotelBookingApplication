using Microsoft.EntityFrameworkCore;

namespace HotelBookingApplication.Models
{
    public class HotelBookingApplicationDbContext : DbContext
    {
        public HotelBookingApplicationDbContext(DbContextOptions<HotelBookingApplicationDbContext> options) : base(options) { }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomsTypes { get; set;}
        public DbSet<RoomStatus> RoomStatuses { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
