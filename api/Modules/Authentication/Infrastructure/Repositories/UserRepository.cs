using Api.Modules.Authentication.Domain;

namespace Api.Modules.Authentication.Infrastructure.Repositories
{
    public class UserRepository(List<User> users)
    {
        public void Create(User user)
        {
            users.Add(user);
        }
        public List<User> GetAll()
        {
            return users;
        }
        public User? GetOne(Guid id)
        {
            return users.FirstOrDefault(user => user.Id == id);
        }
    }
}
