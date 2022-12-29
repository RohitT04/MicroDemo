using BikeService.Database.Entites;
using Microsoft.EntityFrameworkCore;

namespace BikeService.Database
{

    public class DatabaseContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilderbike)
        {
            //optionsBuilderbike.UseSqlServer("data source=192.168.0.72,1433;Database=BikeMicroService;User Id=sa;Password=Jko3va(D9821j#$vGD;MultipleActiveResultSets=True;");
            optionsBuilderbike.UseSqlServer(@"Data Source=DESKTOP-24V1S4R\MSSQLSERVER01;Database=BikeMicroService;Integrated Security=True;");
        }
    }
}
