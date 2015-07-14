using NUnit.Framework;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class ConsoleShould
    {
        private IConsole _Console;
        private bool _EventRaised;
        private object _EventSource;

        [SetUp]
        public void Setup()
        {
            _Console = new Console();
            _Console.RaiseReadLine += _Console_RaiseReadLine;
            _Console.ReadLine();
        }

        [Test]
        public void RaiseTheReadLineEvent()
        {
            Assert.IsTrue(_EventRaised);
        }

        [Test]
        public void BeTheSourceOfTheReadLineEvent()
        {
            Assert.AreSame(_EventSource, _Console);
        }

        private void _Console_RaiseReadLine(object source, ReadLineEventArgs e)
        {
            _EventRaised = true;
            _EventSource = source;
        }

    }
}
