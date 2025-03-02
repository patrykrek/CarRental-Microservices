using app.Services.ReservationAPI.Data;
using app.Services.ReservationAPI.Models;
using app.Services.ReservationAPI.Models.DTO;
using app.Services.ReservationAPI.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace app.Services.ReservationAPI.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;
       
        public ReservationRepository(DataContext context)
        {
            _context = context;
           
        }       

        public async Task<List<Reservation>> GetAllReservations()
        {
            return await _context.Reservations.ToListAsync();
        }
       

        public async Task<List<Reservation>> GetUserReservations(string userId)
        {
            return  _context.Reservations.Where(r => r.UserId == userId).ToList();
        }

        public async Task AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            
            await _context.SaveChangesAsync();  
        }

    }
}
