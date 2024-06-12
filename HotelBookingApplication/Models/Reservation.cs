using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApplication.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Email { get; set; }
        public int NumberOfGuests { get; set; }
        public int GuestDetailsId { get; set; }
        public bool IsPaid { get; set; }
        public int ReservationStatusId { get; set; }
        public virtual ICollection<ReservationDetails>? RDidNavigation { get; set; } = new List<ReservationDetails>();
        public virtual ReservationStatus? RSidNavigation { get; set; } = null!;

        public virtual GuestDetails? GidNavigation { get; set; } = null!;
    }
}
