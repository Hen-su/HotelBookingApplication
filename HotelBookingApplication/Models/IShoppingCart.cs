namespace HotelBookingApplication.Models
{
    public interface IShoppingCart
    {
        void AddToCart(RoomType roomType, DateOnly checkInDate, DateOnly checkOutDate);
        int RemoveFromCart(RoomType roomType);
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> GetShoppingCartItems();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }  
    }
}
