using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite
{
    class Program
    {
        static void Main()
        {
            var clock = new Clock();
            var console = new Console();
            var messagePrinter = new MessagePrinter(console, clock);
            var messageRepository = new MessageRepository(clock);
            var subscriptionRepository = new SubscriptionRepository();
            var userRepository = new UserRepository();
            var service = new Service(messagePrinter, messageRepository, subscriptionRepository, userRepository);
            var controller = new ConsoleController(service, console);
            while (true)
            {
                console.ReadLine();
            }
        }
    }
}
