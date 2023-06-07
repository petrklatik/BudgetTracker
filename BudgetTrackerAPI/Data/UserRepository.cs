using BudgetTrackerAPI.Models;

namespace BudgetTrackerAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly BudgetTrackerDbContext _userContext;

        public UserRepository(BudgetTrackerDbContext userContext)
        {
            _userContext = userContext;
        }

        public User Create(User user)
        {
            _userContext.User.Add(user);
            user.Id = _userContext.SaveChanges();
            return user;
        }

        public User? GetByUsername(string username)
        {
            return _userContext.User.FirstOrDefault(u => u.Username == username);
        }

        public User? GetById(int id)
        {
            return _userContext.User.FirstOrDefault(u => u.Id == id);
        }

        public User? Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _userContext.User.Remove(user);
                _userContext.SaveChanges();
            }
            return user;
        }
    }
}
