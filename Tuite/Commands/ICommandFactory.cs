using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Commands
{
    public interface ICommandFactory
    {
        string Name { get; }
        ICommand CreateCommand(
            string[] inputParts, 
            IUserRepository userRepository, 
            IMessageRepository messageRepository,
            ISubscriptionRepository subscriptionRepository,
            IMessagePrinter messagePrinter);
    }
}