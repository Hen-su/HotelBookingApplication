
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelBookingApplication.Models
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        private IShoppingCart _shoppingCart;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationDetailsRepository _reservationDetailsRepository;
        private readonly IGuestDetailsRepository _guestDetailsRepository;
        private readonly IReservationStatusRepository _reservationStatusRepository;
        public ReservationRepository(HotelBookingApplicationDbContext hotelBookingApplicationDbContext, IShoppingCart shoppingCart, 
            IRoomRepository roomRepository, IReservationDetailsRepository reservationDetailsRepository, IReservationStatusRepository reservationStatusRepository, IGuestDetailsRepository guestDetailsRepository)
        {
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
            _shoppingCart = shoppingCart;
            _roomRepository = roomRepository;
            _reservationDetailsRepository = reservationDetailsRepository;
            _reservationStatusRepository = reservationStatusRepository;
            _guestDetailsRepository = guestDetailsRepository;
        }
        
        public IEnumerable<Reservation> AllReservations
        {
            get
            {
                return _hotelBookingApplicationDbContext.Reservations.Include(r => r.RSidNavigation).Include(r => r.RDidNavigation).Include(r => r.GidNavigation);
            }
        }

        public Reservation GetById(int resId)
        {
            return _hotelBookingApplicationDbContext.Reservations.Include(r => r.RSidNavigation).Include(r => r.RDidNavigation).Include(r => r.GidNavigation).FirstOrDefault(r => r.ReservationId == resId);
        }

        public Reservation GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void MakeReservation(Reservation reservation)
        {
            reservation.CreationTime = DateTime.Now;
            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            reservation.ReservationStatusId = 1;
            List<Room> roomList = AvailableRooms();
            _hotelBookingApplicationDbContext.Reservations.Add(reservation);
            _hotelBookingApplicationDbContext.SaveChanges();
            foreach (var item in shoppingCartItems)
            {
                var detail = new ReservationDetails
                {
                    ReservationId = reservation.ReservationId,
                    CheckInDate = item.CheckInDate,
                    CheckOutDate = item.CheckOutDate,
                    RoomId = roomList.FirstOrDefault(r => r.RoomType == item.RoomType).RoomId,
                    Price = roomList.FirstOrDefault(r => r.RoomType == item.RoomType).RoomType.RoomRate,
                };
                _reservationDetailsRepository.Add(detail);
            }
        }

        private List<Room> AvailableRooms()
        {
            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            List<Room> allRooms = _roomRepository.AllRooms.ToList();
            List<ReservationDetails> reservationDetails = _reservationDetailsRepository.AllDetails.ToList();
            List<Room> removeRooms = new List<Room>();
            foreach (var item in shoppingCartItems)
            {
                foreach (var details in reservationDetails)
                {
                    if (item.CheckInDate >=  details.CheckInDate && item.CheckInDate < details.CheckOutDate)
                    {
                        if (item.CheckOutDate > details.CheckInDate && item.CheckOutDate <= details.CheckOutDate)
                        {
                            removeRooms.Add(allRooms.FirstOrDefault(r => r.RoomId == details.RoomId));
                        }
                    }
                }
            }
            foreach (var room in removeRooms)
            {
                allRooms.Remove(room);
            }
            return allRooms;
        }

        public void UpdateReservation(Reservation reservation)
        {
            _hotelBookingApplicationDbContext.Reservations.Update(reservation);
            _hotelBookingApplicationDbContext.SaveChanges();
        }

        public void DeleteReservation(Reservation reservation)
        {
            _hotelBookingApplicationDbContext.Reservations.Remove(reservation);
            _hotelBookingApplicationDbContext.SaveChanges();
        }

        public IEnumerable<Reservation> SearchReservations(string searchQuery)
        {
            return _hotelBookingApplicationDbContext.Reservations.Where(r => r.Email.Contains(searchQuery));
        }
    }
}
