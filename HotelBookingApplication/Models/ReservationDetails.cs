using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApplication.Models
{
    public class ReservationDetails
    {
        public int ReservationDetailsId { get; set; }
        
        public int RoomId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal Price { get; set; }
        public virtual Room RMidNavigation { get; set; }
        public int ReservationId { get; set; }
        public virtual Reservation RidNavigation { get; set; }
    }
}
