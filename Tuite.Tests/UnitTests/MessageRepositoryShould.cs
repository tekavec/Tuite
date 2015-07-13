using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tuite.Model.Message;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class MessageRepositoryShould
    {
        private readonly DateTime _CNow = new DateTime(2015, 7, 1, 12, 0, 0);
        private Mock<IClock> _Clock;
        private IMessageRepository _MessageRepository;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _Clock = new Mock<IClock>();
        }

        [Test]
        public void CreateAndStoreAMessage()
        {
            _MessageRepository = new MessageRepository(_Clock.Object);
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_CNow);
            var alice = new User {Name = "Alice"};
            _MessageRepository.AddMessage(alice, "I love the weather today");

            var messagesOfUser = _MessageRepository.AllMessages();
            Assert.AreEqual(messagesOfUser.Count, 1);
            Assert.AreEqual(
                messagesOfUser[0],
                new Message { Author = alice, Text = "I love the weather today", Time = _CNow });
        }

        [Test]
        public void GetMessagesOnlyForSubscriptions()
        {
            _MessageRepository = new MessageRepository(_Clock.Object);
            var alice = new User { Name = "Alice" };
            var bob = new User { Name = "Bob" };
            var charlie = new User { Name = "Charlie" };
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_CNow);
            _MessageRepository.AddMessage(alice, "I love the weather today");
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_CNow);
            _MessageRepository.AddMessage(bob, "Damn! We lost!");
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_CNow);
            _MessageRepository.AddMessage(charlie, "I’m in New York today! Anyone want to have a coffee?");

            var wall = _MessageRepository.MessagesOfSubscribedUsers(new List<User> { charlie, alice });

            Assert.AreEqual(wall.Count, 2);
            Assert.AreEqual(
                wall[0],
                new Message { Author = alice, Text = "I love the weather today", Time = _CNow });
            Assert.AreEqual(
                wall[1],
                new Message { Author = charlie, Text = "I’m in New York today! Anyone want to have a coffee?", Time = _CNow });
        }

    }
}
