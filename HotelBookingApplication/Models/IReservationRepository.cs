namespace HotelBookingApplication.Models
{
    public interface IReservationRepository 
    {
        IEnumerable<Reservation> AllReservations();
        Reservation GetById(int resId);
        Reservation GetByName(string name);
    }
}
