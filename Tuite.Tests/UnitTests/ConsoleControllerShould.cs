using Moq;
using NUnit.Framework;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class ConsoleControllerShould
    {
        private Mock<IConsole> _Console;
        private ConsoleController _ConsoleController;
        private Mock<IService> _Service;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _Console = new Mock<IConsole>();
            _Service = new Mock<IService>();
            _ConsoleController = new ConsoleController(_Service.Object, _Console.Object);
        }

        [Test]
        public void ProcessConsoleInputAndExecutePostMessageCommand()
        {
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Alice -> I love the weather today" });

            _ConsoleController.PerformReadLine();

            _Service.Verify(a => a.CreateUserIfNecessaryAndPostMessage("Alice", "I love the weather today"));
        }

        [Test]
        public void ProcessConsoleInputAndExecuteShowTimelineCommand()
        {
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Alice" });

            _ConsoleController.PerformReadLine();

            _Service.Verify(a => a.ShowTimeline("Alice"));
        }

        [Test]
        public void ProcessConsoleInputAndExecuteFollowsCommand()
        {
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Charlie follows Alice" });

            _ConsoleController.PerformReadLine();

            _Service.Verify(a => a.Subscribe("Charlie", "Alice"));
        }

        [Test]
        public void ProcessConsoleInputAndExecuteShowWallCommand()
        {
            _Console.Setup(a => a.ReadLine()).Raises(a => a.RaiseReadLine += null, new ReadLineEventArgs { Input = "Charlie wall" });

            _ConsoleController.PerformReadLine();

            _Service.Verify(a => a.ShowWall("Charlie"));
        }
    }
}
