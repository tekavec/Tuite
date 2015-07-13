using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class ServiceShould
    {
        private Mock<IMessageRepository> _MessageRepository;
        private Mock<ISubscriptionRepository> _SubscriptionRepository;
        private Mock<IUserRepository> _UserRepository;
        private Mock<IMessagePrinter> _MessagePrinter;
        private IService _Service;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _MessageRepository = new Mock<IMessageRepository>();
            _SubscriptionRepository = new Mock<ISubscriptionRepository>();
            _UserRepository = new Mock<IUserRepository>();
            _MessagePrinter = new Mock<IMessagePrinter>();
            _Service = new Service(_MessagePrinter.Object, _MessageRepository.Object, _SubscriptionRepository.Object, _UserRepository.Object);
        }

        [Test]
        public void CreateAUserIfNecessaryAndPostAMessage()
        {
            _Service.CreateUserIfNecessaryAndPostMessage("Alice", "I love the weather today");

            _UserRepository.Verify(a => a.AddUser("Alice"));
            _MessageRepository.Verify(a => a.AddMessage(null, "I love the weather today"));
        }

        [Test]
        public void ShowTimelineOfAUser()
        {
            IList<Message> messages = new List<Message>();
            string user = "Alice";
            _MessageRepository.Setup(a => a.MessagesOfUser(user)).Returns(messages);
            _Service.ShowTimeline(user);

            _MessagePrinter.Verify(a => a.PrintTimeline(messages));
        }

        [Test]
        public void SubscribeFollowerToFollowee()
        {
            var charlie = new User { Name = "Charlie" };
            var alice = new User { Name = "Alice" };
            _UserRepository.Setup(a => a.GetUser("Charlie")).Returns(charlie);
            _UserRepository.Setup(a => a.GetUser("Alice")).Returns(alice);

            _Service.Subscribe("Charlie", "Alice");

            _SubscriptionRepository.Verify(a => a.AddSubscriptionIfNoneParticipantIsNull(charlie, alice));
        }

        [Test]
        public void ShowAggregatedListOfAllSubscriptions()
        {
            IList<Message> messages = new List<Message>();
            var user = new User { Name = "Charlie" };
            var subscriptions = new List<User> { user };
            _UserRepository.Setup(a => a.GetUser(user.Name)).Returns(user);
            _SubscriptionRepository.Setup(a => a.GetSubscribedUsersOfFollower(user.Name)).Returns(subscriptions);
            _MessageRepository.Setup(a => a.MessagesOfSubscribedUsers(subscriptions)).Returns(messages);

            _Service.ShowWall(user.Name);

            _MessagePrinter.Verify(a => a.PrintWall(messages));
        }

        [Test]
        public void NotShowAggregatedListOfAllSubscriptionsIfUserIsNull()
        {
            IList<Message> messages = new List<Message>();
            string iDontExist = "i_dont_exist";
            User user = null;
            var subscriptions = new List<User>();
            _UserRepository.Setup(a => a.GetUser(iDontExist)).Returns(user);
            _SubscriptionRepository.Setup(a => a.GetSubscribedUsersOfFollower(iDontExist)).Returns(subscriptions);
            _MessageRepository.Setup(a => a.MessagesOfSubscribedUsers(subscriptions)).Returns(messages);

            _Service.ShowWall(iDontExist);

            _MessagePrinter.Verify(a => a.PrintWall(messages), Times.Never);
        }

    }
}
