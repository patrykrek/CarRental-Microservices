using app.Services.ReservationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Services.ReservationAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }
        
            
        public DbSet<Reservation> Reservations { get; set; }

       
    }
}
