namespace HotelBookingApplication.Models
{
    public interface IShoppingCart
    {
        void AddToCart(RoomType roomType, DateOnly checkInDate, DateOnly checkOutDate);
        int RemoveFromCart(ShoppingCartItem item);
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> GetShoppingCartItems();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }  
    }
}
