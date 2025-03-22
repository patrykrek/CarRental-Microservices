using app.Services.AuthAPI.Data;
using app.Services.AuthAPI.Models;
using app.Services.AuthAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace app.Services.AuthAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task DeleteUser(ApplicationUser user)
        {
            _context.ApplicationUsers.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> FindUser(string userId)
        {
            return await _context.ApplicationUsers.FindAsync(userId);
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _context.ApplicationUsers.ToListAsync(); ;
        }
    }
}
