namespace HotelBookingApplication.Models
{
    public interface IReservationStatusRepository
    {
        IEnumerable<ReservationStatus> AllReservationStatus {  get; }
    }
}
