using System.Collections.Generic;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Commands
{
    public class ShowWallCommand : ICommand, ICommandFactory
    {

        public void Execute()
        {
            User user = UserRepository.GetUser(UserName);
            if (user != null)
            {
                IList<User> aggregatedSubscriptions = SubscriptionRepository.GetSubscribedUsersOf(user);
                aggregatedSubscriptions.Add(user);
                IList<Message> messages = MessageRepository.MessagesOfSubscribedUsers(aggregatedSubscriptions);
                MessagePrinter.PrintWall(messages);
            }
        }

        public string Name {
            get { return "wall"; }
        }

        public string UserName { get; set; }

        public IMessagePrinter MessagePrinter { get; set; }

        public ISubscriptionRepository SubscriptionRepository { get; set; }
        
        public IMessageRepository MessageRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public ICommand CreateCommand(string[] inputParts, IUserRepository userRepository, IMessageRepository messageRepository,
            ISubscriptionRepository subscriptionRepository, IMessagePrinter messagePrinter)
        {
            return new ShowWallCommand
            {
                MessagePrinter = messagePrinter,
                UserName = inputParts[0],
                MessageRepository = messageRepository,
                UserRepository = userRepository,
                SubscriptionRepository = subscriptionRepository
            };
        }

    }
}