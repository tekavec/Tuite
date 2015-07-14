using System.Collections.Generic;
using System.Linq;

namespace Tuite.Model.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IList<User> _Users = new List<User>();

        public void AddUser(string name)
        {
            var user = GetUser(name);
            if (user == null)
            {
                user = new User {Name = name};
                _Users.Add(user);
            }
        }

        public User GetUser(string name)
        {
            return _Users.FirstOrDefault(a => a.Name == name);
        }

        public IList<User> AllUsers()
        {
            return _Users;
        }
    }
}