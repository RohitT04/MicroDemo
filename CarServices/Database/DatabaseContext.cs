using CarServices.Database.Entites;
using Microsoft.EntityFrameworkCore;

namespace CarServices.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Car> cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuildercar)
        {
            optionsBuildercar.UseSqlServer(@"Data Source=DESKTOP-24V1S4R\MSSQLSERVER01;Database=CarMicroService;Integrated Security=True;");
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-24V1S4R\\MSSQLSERVER01;Initial Catalog=CarMicroService;Integrated Security=True");
            // optionsBuilder.UseSqlServer("data source=192.168.0.72,1433;Database=CarMicroService;User Id=sa;Password=Jko3va(D9821j#$vGD;MultipleActiveResultSets=True;");

        }
    }
}
