using Microsoft.AspNetCore.Mvc;
using GBook.Models;

namespace GBook.Repository
{
    public interface IRepository
    { 
        Task<List<Messages>> GetMessageList();
        Task Create(Messages mes);
        Task Save();
        Task<Users?> GetUserByLoginAsync(string login);
        Task<bool> UserExistsAsync(string login);
        Task CreateUserAsync(Users user);
    }
}
