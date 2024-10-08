using HotelBookingApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HotelBookingApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'HotelBookingApplicationDbContextConnection' not found.");
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
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

builder.Services.AddDefaultIdentity<IdentityUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<HotelBookingApplicationDbContext>();
        
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
DbInitializer.Seed(app);
app.Run();
