global using Microsoft.EntityFrameworkCore;

namespace RawDataFilter.Data
{

    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer(
                "Server=192.168.10.123, 1433;Database=db_bachelorarbeit;User ID=user1;Password=123456789");

        public DbSet<Entities.RawDataFilter> RawDataFilters { get; set; }
    }
}