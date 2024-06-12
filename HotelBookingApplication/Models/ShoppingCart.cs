
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApplication.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly HotelBookingApplicationDbContext _hotelBookingApplicationDbContext;
        public ShoppingCart(HotelBookingApplicationDbContext hotelBookingApplicationDbContext) 
        { 
            _hotelBookingApplicationDbContext = hotelBookingApplicationDbContext;
        }

        public string? ShoppingCartId { get; set; } = default!;
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            HotelBookingApplicationDbContext context = services.GetService<HotelBookingApplicationDbContext>() ?? throw new Exception("Error initialiseing"); ;
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId};
        }

        public void AddToCart(RoomType roomType, DateOnly checkInDate, DateOnly checkOutDate)
        {
            var shoppingCartItem = _hotelBookingApplicationDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.RoomType.RoomTypeId == roomType.RoomTypeId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    RoomType = roomType,
                    Amount = 1,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    StayDuration = checkOutDate.DayNumber - checkInDate.DayNumber
                };
                _hotelBookingApplicationDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _hotelBookingApplicationDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _hotelBookingApplicationDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _hotelBookingApplicationDbContext.ShoppingCartItems.RemoveRange(cartItems);
            _hotelBookingApplicationDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _hotelBookingApplicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.RoomType.RoomRate * c.StayDuration * c.Amount).Sum();
            return total;
        }

        public int RemoveFromCart(ShoppingCartItem item)
        {
            var shoppingCartItem = _hotelBookingApplicationDbContext.ShoppingCartItems.SingleOrDefault(
                s => s.ShoppingCartItemId == item.ShoppingCartItemId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _hotelBookingApplicationDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _hotelBookingApplicationDbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??= _hotelBookingApplicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.RoomType)
                .ToList();
        }
    }
}
