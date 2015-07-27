using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Tuite.Commands;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Tests.Features
{
    [TestFixture]
    public class TuiteFeature
    {
        private readonly DateTime _Now = new DateTime(2015, 7, 1, 12, 0, 0);

        private Mock<IClock> _Clock;
        private Mock<Console> _Console;
        private IUserRepository _UserRepository;
        private IMessageRepository _MessageRepository;
        private ISubscriptionRepository _SubscriptionRepository;
        private CommandParser _CommandParser;
        private IMessagePrinter _MessagePrinter;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _Clock = new Mock<IClock>();
            _Console = new Mock<Console> ();
            _MessagePrinter = new MessagePrinter(_Console.Object, _Clock.Object);
            _UserRepository = new UserRepository();
            _MessageRepository = new MessageRepository(_Clock.Object);
            _SubscriptionRepository = new SubscriptionRepository();
            _CommandParser = new CommandParser(GetAllCommands(), _UserRepository, _MessageRepository, _SubscriptionRepository, _MessagePrinter);
        }

        private IEnumerable<ICommandFactory> GetAllCommands()
        {
            var commands = new ICommandFactory[] 
            { 
                new PostMessageCommand(), 
                new ShowTimelineCommand(), 
                new CreateSubscriptionCommand(), 
                new ShowWallCommand()
            };
            return commands;
        }


        [Test]
        public void ShowTimelineWithoutSubscriptions()
        {
            ExecuteCommandParsedFromInput(-300, "Alice -> I love the weather today");
            ExecuteCommandParsedFromInput(-120, "Bob -> Damn! We lost!");
            ExecuteCommandParsedFromInput(-60, "Bob -> Good game though.");
            ExecuteCommandParsedFromInput(0, "Alice");
            ExecuteCommandParsedFromInput(0, "Bob");

            //NOTE: shortcut to verifying WriteLine method, not quite happy with that
            _Console.CallBase = true;

            //Bob
            _Console.Verify(c => c.WriteLine("Good game though. (1 minute ago)"));
            _Console.Verify(c => c.WriteLine("Damn! We lost! (2 minutes ago)"));
            //Alice
            _Console.Verify(c => c.WriteLine("I love the weather today (5 minutes ago)"));
        }

        [Test]
        public void ShowTimelineWithSubscriptions()
        {
            ExecuteCommandParsedFromInput(-300, "Alice -> I love the weather today");
            ExecuteCommandParsedFromInput(-120, "Bob -> Damn! We lost!");
            ExecuteCommandParsedFromInput(-60, "Bob -> Good game though.");
            ExecuteCommandParsedFromInput(-15, "Charlie -> I'm in New York today! Anyone want to have a coffee?");
            ExecuteCommandParsedFromInput(-10, "Charlie follows Alice");
            ExecuteCommandParsedFromInput(0, "Charlie wall");
            ExecuteCommandParsedFromInput(0, "Charlie follows Bob");
            ExecuteCommandParsedFromInput(0, "Charlie wall");

            //NOTE: shortcut to verifying WriteLine method, not quite happy with that
            _Console.CallBase = true;

            //Charlie follows Alice; Charlie wall
            _Console.Verify(c => c.WriteLine("Charlie - I'm in New York today! Anyone want to have a coffee? (15 seconds ago)"));
            _Console.Verify(c => c.WriteLine("Alice - I love the weather today (5 minutes ago)"));
            //Charlie follows Bob; Charlie wall
            _Console.Verify(c => c.WriteLine("Charlie - I'm in New York today! Anyone want to have a coffee? (15 seconds ago)"));
            _Console.Verify(c => c.WriteLine("Bob - Good game though. (1 minute ago)"));
            _Console.Verify(c => c.WriteLine("Bob - Damn! We lost! (2 minutes ago)"));
            _Console.Verify(c => c.WriteLine("Alice - I love the weather today (5 minutes ago)"));
        }

        private void ExecuteCommandParsedFromInput(int relativeTimeInSeconds, string input)
        {
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now.AddSeconds(relativeTimeInSeconds));
            _Console.Setup(a => a.ReadLine()).Returns(input);
            var command = _CommandParser.Parse(_Console.Object.ReadLine());
            command.Execute();
        }
    }

}