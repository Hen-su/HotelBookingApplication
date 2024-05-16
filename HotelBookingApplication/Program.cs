using HotelBookingApplication.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IRoomStatusRepository, RoomStatusRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
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
