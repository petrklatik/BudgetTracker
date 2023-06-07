using BudgetTrackerAPI.Models;

namespace BudgetTrackerAPI.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User? GetByUsername(string username);
        User? GetById(int id);
        User? Delete(int id);
    }
}
