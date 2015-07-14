using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tuite.Model.Message;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class MessagePrinterShould
    {
        private readonly DateTime _Now = new DateTime(2015, 7, 1, 12, 0, 0);
        private Mock<IClock> _Clock;
        private Mock<IConsole> _Console;
        private IMessagePrinter _MessagePrinter;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _Clock = new Mock<IClock>();
            _Console = new Mock<IConsole>();
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now);
            _MessagePrinter = new MessagePrinter(_Console.Object, _Clock.Object);
        }

        [Test]
        public void PrintTimelineInReverseChronologicalOrder()
        {
            var setupMessages = CreateDummyMessages();

            _MessagePrinter.PrintTimeline(setupMessages);

            _Console.Verify(c => c.WriteLine("Damn! We lost! (2 minutes ago)"));
            _Console.Verify(c => c.WriteLine("Good game though. (1 minute ago)"));
        }

        [Test]
        public void PrintWallInReverseChronologicalOrder()
        {
            var setupMessages = CreateDummyMessages();

            _MessagePrinter.PrintWall(setupMessages);

            _Console.Verify(c => c.WriteLine("Bob - Damn! We lost! (2 minutes ago)"));
            _Console.Verify(c => c.WriteLine("Bob - Good game though. (1 minute ago)"));
        }

        private List<Message> CreateDummyMessages()
        {
            var bob = new User { Name = "Bob" };

            var messages = new List<Message>
            {
                new Message {Author = bob, Text = "Damn! We lost!", Time = _Now.AddMinutes(-2)},
                new Message {Author = bob, Text = "Good game though.", Time = _Now.AddMinutes(-1)}
            };
            return messages;
        }

    }
}