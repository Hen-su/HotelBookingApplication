﻿using HotelBookingApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository; 
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IReservationStatusRepository _roomStatusRepository;

        public RoomController(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository, IReservationStatusRepository roomStatusRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _roomStatusRepository = roomStatusRepository;
        }
        public ViewResult Index()
        {
            return View(_roomRepository.AllRooms);
        }


    }
}
