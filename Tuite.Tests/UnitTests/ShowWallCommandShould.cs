using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tuite.Commands;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    public class ShowWallCommandShould
    {
        private Mock<IMessagePrinter> _MessagePrinter;
        private ShowWallCommand _ShowWallCommand;
        private Mock<IUserRepository> _UserRepository;
        private Mock<ISubscriptionRepository> _SubscriptionRepository;
        private Mock<IMessageRepository> _MessageRepository;

        [SetUp]
        public void Initialize()
        {
            _UserRepository = new Mock<IUserRepository>();
            _SubscriptionRepository = new Mock<ISubscriptionRepository>();
            _MessageRepository = new Mock<IMessageRepository>();
            _MessagePrinter = new Mock<IMessagePrinter>();
            _ShowWallCommand = new ShowWallCommand
            {
                UserRepository = _UserRepository.Object,
                SubscriptionRepository = _SubscriptionRepository.Object,
                MessageRepository = _MessageRepository.Object,
                MessagePrinter = _MessagePrinter.Object
            };

        }

        [Test]
        public void CreateItself()
        {
            Assert.IsInstanceOf(typeof(ShowWallCommand),
                _ShowWallCommand.CreateCommand(new string[2], new UserRepository(), new MessageRepository(new Clock()),
                    new SubscriptionRepository(), new MessagePrinter(new Console(), new Clock())));
        }

        [Test]
        public void ShowMessagesOfSubscribedUsers()
        {
            IList<Message> messages = new List<Message>();
            var user = new User { Name = "Charlie" };
            var subscriptions = new List<User> { user };
            _UserRepository.Setup(a => a.GetUser(user.Name)).Returns(user);
            _SubscriptionRepository.Setup(a => a.GetSubscribedUsersOf(user)).Returns(subscriptions);
            _MessageRepository.Setup(a => a.MessagesOfSubscribedUsers(subscriptions)).Returns(messages);
            _ShowWallCommand.UserName = user.Name;

            _ShowWallCommand.Execute();

            _MessagePrinter.Verify(a => a.PrintWall(messages));

        }
    }
}
