using Microsoft.EntityFrameworkCore;
using GBook.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace GBook.Repository
{
    public class GBookRepository : IRepository
    {
        private readonly UserContext _context;
        public GBookRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<List<Messages>> GetMessageList()
        {
            return await _context.Messages.Include(p => p.User).ToListAsync();
        }

        public async Task Create(Messages mes)
        {
            await _context.Messages.AddAsync(mes);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Users?> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<bool> UserExistsAsync(string login)
        {
            return await _context.Users.AnyAsync(u => u.Login == login);
        }

        public async Task CreateUserAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
