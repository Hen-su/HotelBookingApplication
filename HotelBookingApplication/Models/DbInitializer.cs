﻿using HotelBookingApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HotelBookingApplication.Models
{
    public class DbInitializer
    {
        private static Dictionary<string, RoomType>? roomTypes;
        
        public static Dictionary<string, RoomType> RoomTypes
        {
            get
            {
                if (roomTypes == null)
                {
                    var typeList = new RoomType[]
                    {
                            new RoomType { RoomTypeName = "Queen Room", RoomTypeDescription = "Step into luxury and comfort in our Queen Room, designed for couples or solo travelers seeking tranquility. Unwind on a plush queen-sized bed adorned with premium linens, and let the soft lighting create an inviting ambiance. With modern amenities at your fingertips and a sleek ensuite bathroom, relaxation is effortless. Whether you're here for business or pleasure, this room offers a serene retreat after a day of exploration.", Occupancy = 2, RoomRate = 165.00m, ImageThumbnail = "/images/Queen_Thumb.jpg" },
                            new RoomType { RoomTypeName = "King Room", RoomTypeDescription = "Indulge in opulence and space in our King Room, perfect for those craving a regal experience. Sink into the expansive king-sized bed, outfitted with sumptuous bedding for a restful night's sleep. Take advantage of the ample seating area for unwinding or catching up on work, and revel in the luxurious marble bathroom complete with a rejuvenating rain shower. With panoramic views of the city skyline or tranquil surroundings, every moment in this room is designed for unparalleled comfort and sophistication.", Occupancy = 3, RoomRate = 175.00m, ImageThumbnail = "/images/King_Thumb.jpg" },
                            new RoomType { RoomTypeName = "Double Room", RoomTypeDescription = "Ideal for friends or colleagues traveling together, our Twin Room offers comfort and convenience in a stylish setting. Each guest will enjoy their own cozy twin bed, complete with premium linens for a restful night's sleep. The contemporary design and thoughtful amenities ensure a pleasant stay, while the spacious work desk provides a dedicated space for productivity. Whether you're exploring the city or attending meetings, return to this inviting room for relaxation and rejuvenation.", Occupancy = 5, RoomRate = 175.00m, ImageThumbnail = "/images/Double_Thumb.jpg" }
                    };

                    roomTypes = new Dictionary<string, RoomType>();

                    foreach (RoomType type in typeList)
                    {
                        roomTypes.Add(type.RoomTypeName, type);
                    }
                }
                return roomTypes;
            }
        }
        

        private static Dictionary<string, ReservationStatus>? resStatuses;
        public static Dictionary<string, ReservationStatus> ResStatuses
        {
            get
            {
                if (resStatuses == null)
                {
                    var statusList = new ReservationStatus[]
                    {
                                        new ReservationStatus { ReservationStatusName = "Active" },
                                        new ReservationStatus { ReservationStatusName = "Checked In" },
                                        new ReservationStatus { ReservationStatusName = "Checked Out" },
                                        new ReservationStatus { ReservationStatusName = "Closed" },
                                        new ReservationStatus { ReservationStatusName = "Cancelled" }
                    };

                    resStatuses = new Dictionary<string, ReservationStatus>();

                    foreach (ReservationStatus status in statusList)
                    {
                        resStatuses.Add(status.ReservationStatusName, status);
                    }
                }
                return resStatuses;
            }
        }

        public async static void Seed(IApplicationBuilder applicationBuilder)
        {
            HotelBookingApplicationDbContext context =
                applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetService<HotelBookingApplicationDbContext>();

            context.Database.Migrate();

            if (!context.ReservationStatuses.Any())
            {
                foreach (ReservationStatus status in ResStatuses.Values)
                {
                    context.Add(status);
                }
                context.SaveChanges();
            }
            
            if (!context.RoomsTypes.Any())
            {
                foreach (RoomType roomtype in RoomTypes.Values)
                {
                    context.Add(roomtype);
                }
                context.SaveChanges();
            }
            
            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange
                (
                    new Room { RoomNumber = 101, RoomType = RoomTypes["Queen Room"] },
                    new Room { RoomNumber = 102, RoomType = RoomTypes["Queen Room"] },
                    new Room { RoomNumber = 103, RoomType = RoomTypes["King Room"] },
                    new Room { RoomNumber = 104, RoomType = RoomTypes["King Room"] },
                    new Room { RoomNumber = 105, RoomType = RoomTypes["Double Room"] },
                    new Room { RoomNumber = 201, RoomType = RoomTypes["Queen Room"] },
                    new Room { RoomNumber = 202, RoomType = RoomTypes["Queen Room"] },
                    new Room { RoomNumber = 203, RoomType = RoomTypes["King Room"] },
                    new Room { RoomNumber = 204, RoomType = RoomTypes["King Room"] },
                    new Room { RoomNumber = 205, RoomType = RoomTypes["Double Room"] }
                );
                context.SaveChanges();
            }
            
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Member" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                string email = "test@admin.com";
                string password = "Password123?";
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser(){ UserName = email, Email = email};
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}  