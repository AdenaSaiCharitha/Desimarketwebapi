using DesiMarket.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesiMarket.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly OnlineGroceryDbContext _dbContext;
        public UsersRepository(OnlineGroceryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Users> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<Users> LoginAsync(string Email, string Password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task CreateUserAsync(Users user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Users user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Users user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}
