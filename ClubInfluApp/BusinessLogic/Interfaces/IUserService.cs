using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByEmailAsync(string email);
        public Task<bool> CreateUserAsync(string email, string name, string role);
    }
}
