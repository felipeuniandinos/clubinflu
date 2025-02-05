using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByEmailAsync(string email);

        public Task<int> CreateUserAsync(User user);
    }
}
