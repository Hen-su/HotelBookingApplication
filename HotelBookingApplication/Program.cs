using HotelBookingApplication.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRoomRepository, MockRoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, MockRoomTypeRepository>();
builder.Services.AddScoped<IRoomStatusRepository, MockRoomStatusRepository>();
var app = builder.Build();

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
