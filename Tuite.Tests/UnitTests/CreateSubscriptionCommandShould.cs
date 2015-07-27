using Moq;
using NUnit.Framework;
using Tuite.Commands;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    public class CreateSubscriptionCommandShould
    {
        private Mock<IUserRepository> _UserRepository;
        private Mock<ISubscriptionRepository> _SubscriptionRepository;
        private CreateSubscriptionCommand _CreateSubscriptionCommand;

        [Test]
        public void CreateItself()
        {
            _CreateSubscriptionCommand = new CreateSubscriptionCommand();

            Assert.IsInstanceOf(typeof(CreateSubscriptionCommand),
                _CreateSubscriptionCommand.CreateCommand(new string[3], new UserRepository(), new MessageRepository(new Clock()),
                    new SubscriptionRepository(), new MessagePrinter(new Console(), new Clock())));
        }

        [Test]
        public void CreateSubscription()
        {
            _UserRepository = new Mock<IUserRepository>();
            _SubscriptionRepository = new Mock<ISubscriptionRepository>();
            var charlie = new User { Name = "Charlie" };
            var alice = new User { Name = "Alice" };

            _CreateSubscriptionCommand = new CreateSubscriptionCommand
            {
                UserRepository = _UserRepository.Object,
                SubscriptionRepository = _SubscriptionRepository.Object,
                FollowerName = charlie.Name,
                FolloweeName = alice.Name
            };

            _UserRepository.Setup(a => a.GetUser(charlie.Name)).Returns(charlie);
            _UserRepository.Setup(a => a.GetUser(alice.Name)).Returns(alice);
            _CreateSubscriptionCommand.Execute();

            _SubscriptionRepository.Verify(a => a.AddSubscriptionIfNoneParticipantIsNull(charlie, alice));
        }
    }
}
