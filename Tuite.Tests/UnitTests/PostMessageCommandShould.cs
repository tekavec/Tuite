using Moq;
using NUnit.Framework;
using Tuite.Commands;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class PostMessageCommandShould
    {
        private PostMessageCommand _PostMessageCommand;
        private Mock<IUserRepository> _UserRepository;
        private Mock<IMessageRepository> _MessageRepository;

        [SetUp]
        public void Initialize()
        {
            _UserRepository = new Mock<IUserRepository>();
            _MessageRepository= new Mock<IMessageRepository>();
        }

        [Test]
        public void CreateItself()
        {
            _PostMessageCommand = new PostMessageCommand();

            Assert.IsInstanceOf(typeof(PostMessageCommand),
                _PostMessageCommand.CreateCommand(new string[3], new UserRepository(), new MessageRepository(new Clock()),
                    new SubscriptionRepository(), new MessagePrinter(new Console(), new Clock())));
        }

        [Test]
        public void CreateAUserIfNecessaryAndPostAMessage()
        {
            _PostMessageCommand = new PostMessageCommand
            {
                UserRepository = _UserRepository.Object,
                MessageRepository = _MessageRepository.Object,
                UserName = "Alice",
                MessageText = "I love the weather today"
            };

            _PostMessageCommand.Execute();

            _UserRepository.Verify(a => a.AddUser("Alice"));
            _MessageRepository.Verify(a => a.AddMessage(null, "I love the weather today"));
        }
    }
}
