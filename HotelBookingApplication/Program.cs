using HotelBookingApplication.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IReservationStatusRepository, ReservationStatusRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationDetailsRepository, ReservationDetailsRepository>();
builder.Services.AddScoped<IGuestDetailsRepository, GuestDetailsRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddDbContext<HotelBookingApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:HotelBookingApplicationDbContextConnection"]);
});
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id:int?}");
app.UseSession();
DbInitializer.Seed(app);
app.Run();
