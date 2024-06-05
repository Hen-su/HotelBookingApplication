
using System.Diagnostics;

namespace HotelBookingApplication.Models
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        private IShoppingCart _shoppingCart;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationDetailsRepository _reservationDetailsRepository;
        IReservationStatusRepository _reservationStatusRepository;
        public ReservationRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext, IShoppingCart shoppingCart, 
            IRoomRepository roomRepository, IReservationDetailsRepository reservationDetailsRepository, IReservationStatusRepository reservationStatusRepository)
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
            _shoppingCart = shoppingCart;
            _roomRepository = roomRepository;
            _reservationDetailsRepository = reservationDetailsRepository;
            _reservationStatusRepository = reservationStatusRepository;
        }

        public IEnumerable<Reservation> AllReservations
        {
            get
            {
                return _hotelBookingApplicationDbContext.Reservations;
            }
        }

        public Reservation GetById(int resId)
        {
            throw new NotImplementedException();
        }

        public Reservation GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void MakeReservation(Reservation reservation)
        {
            reservation.CreationTime = DateTime.Now;
            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            reservation.ReservationDetails = new List<ReservationDetails>();
            reservation.ReservationStatus = _reservationStatusRepository.AllReservationStatus.FirstOrDefault(rs => rs.ReservationStatusId == 1);
            List<Room> roomList = AvailableRooms();
            foreach (var item in shoppingCartItems)
            {
                var detail = new ReservationDetails
                {
                    CheckInDate = item.CheckInDate,
                    CheckOutDate = item.CheckOutDate,
                    RoomId = roomList.FirstOrDefault(r => r.RoomType == item.RoomType).RoomId,
                    Price = roomList.FirstOrDefault(r => r.RoomType == item.RoomType).RoomType.RoomRate,
                };
                reservation.ReservationDetails.Add(detail);
            }
            _hotelBookingApplicationDbContext.Reservations.Add(reservation);
            _hotelBookingApplicationDbContext.SaveChanges();
        }

        private List<Room> AvailableRooms()
        {
            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            List<Room> allRooms = _roomRepository.AllRooms.ToList();
            List<ReservationDetails> reservationDetails = _reservationDetailsRepository.AllDetails.ToList();
            List<Room> removeRooms = new List<Room>();
            foreach (var item in shoppingCartItems)
            {
                foreach (var details in reservationDetails)
                {
                    if (item.CheckInDate >=  details.CheckInDate && item.CheckOutDate < details.CheckOutDate)
                    {
                        removeRooms.Add(allRooms.FirstOrDefault(r => r.RoomId == details.RoomId));
                    }
                }
            }
            foreach (var room in removeRooms)
            {
                allRooms.Remove(room);
            }
            return allRooms;
        }
    }
}
