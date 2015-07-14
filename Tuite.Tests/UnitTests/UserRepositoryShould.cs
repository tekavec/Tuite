using NUnit.Framework;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class UserRepositoryShould
    {
        private IUserRepository _UserRepository;

        [Test]
        public void CreateAndStoreAUser()
        {
            _UserRepository = new UserRepository();

            _UserRepository.AddUser("Alice");

            var allUsers = _UserRepository.AllUsers();
            Assert.AreEqual(allUsers.Count, 1);
            Assert.AreEqual(
                allUsers[0],
                new User { Name = "Alice" });
        }

        [Test]
        public void NotCreateTwoUsersWithTheSameName()
        {
            _UserRepository = new UserRepository();

            _UserRepository.AddUser("Alice");
            _UserRepository.AddUser("Alice");

            Assert.AreEqual(_UserRepository.AllUsers().Count, 1);
        }

        [Test]
        public void RetrieveAUserByName()
        {
            _UserRepository = new UserRepository();

            _UserRepository.AddUser("Alice");
            _UserRepository.AddUser("Charlie");

            Assert.AreEqual(_UserRepository.GetUser("Alice"), new User {Name = "Alice"});
        }

    }
}