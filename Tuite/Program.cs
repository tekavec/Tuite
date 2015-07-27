using System.Collections.Generic;
using Tuite.Commands;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite
{
    class Program
    {
        static void Main()
        {
            //NOTE: IoC (e.g. Windsor Container) could be introduced for easier dependency graph handling
            var clock = new Clock();
            var console = new Console();
            var messagePrinter = new MessagePrinter(console, clock);
            var messageRepository = new MessageRepository(clock);
            var subscriptionRepository = new SubscriptionRepository();
            var userRepository = new UserRepository();
            IEnumerable<ICommandFactory> allCommands = new ICommandFactory[]
            {
                new PostMessageCommand(), 
                new ShowTimelineCommand(), 
                new CreateSubscriptionCommand(), 
                new ShowWallCommand() 
            };
            var commandParser = new CommandParser(
                allCommands, 
                userRepository, 
                messageRepository,
                subscriptionRepository, 
                messagePrinter);
            while (true)
            {
                commandParser.Parse(console.ReadLine()).Execute();
            }
        }
    }
}