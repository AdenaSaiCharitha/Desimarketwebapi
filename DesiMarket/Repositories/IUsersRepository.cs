using DesiMarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesiMarket.Repositories
{
    public interface IUsersRepository
    {
        Task<Users> GetUserByIdAsync(int id);
        Task<Users> LoginAsync(string Email,string Password);
        Task<IEnumerable<Users>> GetUsersAsync();
        Task CreateUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task DeleteUserAsync(Users user);
    }
}
