using System;
using Moq;
using NUnit.Framework;
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
        private IMessageRepository _MessageRepository;
        private ISubscriptionRepository _SubscriptionRepository;
        private IUserRepository _UserRepository;
        private ConsoleController _ConsoleController;
        private IMessagePrinter _MessagePrinter;
        private IService _Service;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _Clock = new Mock<IClock>();
            _Console = new Mock<Console> ();
            _MessagePrinter = new MessagePrinter(_Console.Object, _Clock.Object);
            _MessageRepository = new MessageRepository(_Clock.Object);
            _SubscriptionRepository = new SubscriptionRepository();
            _UserRepository = new UserRepository();
            _Service = new Service(_MessagePrinter, _MessageRepository, _SubscriptionRepository, _UserRepository);
            _ConsoleController = new ConsoleController(_Service, _Console.Object);
        }

        [Test]
        public void PostAndReadMessages()
        {
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now.AddMinutes(-5));
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Alice -> I love the weather today" });
            _ConsoleController.PerformReadLine();
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now.AddMinutes(-2));
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Bob -> Damn! We lost!" });
            _ConsoleController.PerformReadLine();
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now.AddMinutes(-1));
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Bob -> Good game though." });
            _ConsoleController.PerformReadLine();
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Alice" });
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now);
            _ConsoleController.PerformReadLine();
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Bob" });
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now);
            _ConsoleController.PerformReadLine();
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Charlie -> I'm in New York today! Anyone want to have a coffee?" });
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now.AddSeconds(-15));
            _ConsoleController.PerformReadLine();
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Charlie follows Alice" });
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now.AddSeconds(-10));
            _ConsoleController.PerformReadLine();
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Charlie wall" });
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now);
            _ConsoleController.PerformReadLine();
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Charlie follows Bob" });
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now);
            _ConsoleController.PerformReadLine();
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Charlie wall" });
            _Clock.Setup(a => a.CurrentDateAndTime).Returns(_Now);
            _ConsoleController.PerformReadLine();

            //NOTE: shortcut to verifying WriteLine method, not quite happy with that
            _Console.CallBase = true; 

            //Alice
            _Console.Verify(c => c.WriteLine("I love the weather today (5 minutes ago)"));
            //Bob
            _Console.Verify(c => c.WriteLine("Damn! We lost! (2 minutes ago)"));
            _Console.Verify(c => c.WriteLine("Good game though. (1 minute ago)"));
            //Charlie follows Alice; Charlie wall
            _Console.Verify(c => c.WriteLine("Charlie - I'm in New York today! Anyone want to have a coffee? (15 seconds ago)"));
            _Console.Verify(c => c.WriteLine("Alice - I love the weather today (5 minutes ago)"));
            //Charlie follows Bob; Charlie wall
            _Console.Verify(c => c.WriteLine("Charlie - I'm in New York today! Anyone want to have a coffee? (15 seconds ago)"));
            _Console.Verify(c => c.WriteLine("Bob - Damn! We lost! (2 minutes ago)"));
            _Console.Verify(c => c.WriteLine("Bob - Good game though. (1 minute ago)"));
            _Console.Verify(c => c.WriteLine("Alice - I love the weather today (5 minutes ago)"));
        }

    }

}