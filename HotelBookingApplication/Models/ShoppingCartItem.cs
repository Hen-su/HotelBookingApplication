namespace HotelBookingApplication.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public RoomType RoomType { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int StayDuration {  get; set; }
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; }
    }
}
