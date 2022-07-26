using Microsoft.AspNetCore.Authorization;

namespace BikeService.Database.Entites
{
    
    public class Bike
    {
        public int BikeId { get; set; }
        public string BikeName { get; set; }
        public string BikeType { get; set; }
        public string BikeBrand { get; set; }

    }
}
