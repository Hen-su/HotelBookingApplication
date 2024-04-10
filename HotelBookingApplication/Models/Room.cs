﻿using System.ComponentModel;

namespace HotelBookingApplication.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public int RoomStatusId { get; set; }
        public RoomStatus RoomStatus { get; set; } = default;
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; } = default;
    }
}
