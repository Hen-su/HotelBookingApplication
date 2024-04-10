
namespace HotelBookingApplication.Models
{
    public class MockRoomTypeRepository : IRoomTypeRepository
    {
        public IEnumerable<RoomType> AllRoomTypes =>
            new List<RoomType>()
            {
                new RoomType{RoomTypeId = 1, RoomTypeName = "Queen Room", RoomTypeDescription = "Step into luxury and comfort in our Queen Room, designed for couples or solo travelers seeking tranquility. Unwind on a plush queen-sized bed adorned with premium linens, and let the soft lighting create an inviting ambiance. With modern amenities at your fingertips and a sleek ensuite bathroom, relaxation is effortless. Whether you're here for business or pleasure, this room offers a serene retreat after a day of exploration.", Occupancy = 2, RoomRate = 165.00m},
                new RoomType{RoomTypeId = 2, RoomTypeName = "King Room", RoomTypeDescription = "Indulge in opulence and space in our King Room, perfect for those craving a regal experience. Sink into the expansive king-sized bed, outfitted with sumptuous bedding for a restful night's sleep. Take advantage of the ample seating area for unwinding or catching up on work, and revel in the luxurious marble bathroom complete with a rejuvenating rain shower. With panoramic views of the city skyline or tranquil surroundings, every moment in this room is designed for unparalleled comfort and sophistication.", Occupancy = 2, RoomRate = 175.00m},
                new RoomType{RoomTypeId = 3, RoomTypeName = "Double Room", RoomTypeDescription = "Ideal for friends or colleagues traveling together, our Twin Room offers comfort and convenience in a stylish setting. Each guest will enjoy their own cozy twin bed, complete with premium linens for a restful night's sleep. The contemporary design and thoughtful amenities ensure a pleasant stay, while the spacious work desk provides a dedicated space for productivity. Whether you're exploring the city or attending meetings, return to this inviting room for relaxation and rejuvenation.", Occupancy = 2, RoomRate = 175.00m }
            };

        public RoomType GetRoomTypeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
