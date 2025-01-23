using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
    }
}
