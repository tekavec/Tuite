using Moq;
using NUnit.Framework;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class ConsoleShould
    {
        private Mock<IConsole> _Console;


        [SetUp]
        public void Setup()
        {
            _Console = new Mock<IConsole>();
        }

        [Test]
        public void ReadALine()
        {
            _Console.Setup(a => a.ReadLine()).Returns("some text");
            var input = _Console.Object.ReadLine();

            Assert.AreEqual("some text", input);
        }


        [Test]
        public void WriteAString()
        {
            _Console.Object.WriteLine("some text");

            _Console.Verify(a => a.WriteLine("some text"));
        }

    }
}
