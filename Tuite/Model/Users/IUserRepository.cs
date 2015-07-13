using System.Collections.Generic;

namespace Tuite.Model.Users
{
    public interface IUserRepository
    {
        void AddUser(string name);
        User GetUser(string name);
        IList<User> AllUsers();
    }
}