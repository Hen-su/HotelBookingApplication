using HotelBookingApplication.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IRoomStatusRepository, RoomStatusRepository>();
builder.Services.AddDbContext<HotelBookingApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:HotelBookingApplicationDbContextConnection"]);
});
var app = builder.Build();

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.MapDefaultControllerRoute();
DbInitializer.Seed(app);
app.Run();
