using Microsoft.EntityFrameworkCore;
using BikeService.Database.Entites;
using Microsoft.AspNetCore.Authorization;

namespace BikeService.Database
{
    
    public class DatabaseContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilderbike)
        {
            optionsBuilderbike.UseSqlServer("data source=192.168.0.72,1433;Database=BikeMicroService;User Id=sa;Password=Jko3va(D9821j#$vGD;MultipleActiveResultSets=True;"); 
        }
    }
}
