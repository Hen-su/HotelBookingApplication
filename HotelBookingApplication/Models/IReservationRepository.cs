namespace HotelBookingApplication.Models
{
    public interface IReservationRepository 
    {
        IEnumerable<Reservation> AllReservations { get; }
        void MakeReservation(Reservation reservation);
        Reservation GetById(int resId);
        Reservation GetByName(string name);
    }
}
