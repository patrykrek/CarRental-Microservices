using app.Services.CarAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Services.CarAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        
        public DbSet<Car> Cars { get; set; }
        
    }
}
