namespace HotelBookingApplication.Models
{
    public interface IReservationRepository 
    {
        IEnumerable<Reservation> AllReservations { get; }
        void MakeReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
        Reservation GetById(int resId);
        Reservation GetByName(string name);
        IEnumerable<Reservation> SearchReservations(string searchQuery);
    }
}
