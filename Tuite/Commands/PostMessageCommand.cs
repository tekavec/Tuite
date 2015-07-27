using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Commands
{
    public class PostMessageCommand : ICommand, ICommandFactory
    {

        public void Execute()
        {
            UserRepository.AddUser(UserName);
            User user = UserRepository.GetUser(UserName);
            MessageRepository.AddMessage(user, MessageText);

        }

        public string MessageText { get; set; }
        
        public string UserName { get; set; }
        
        public string Name {
            get { return "->"; }
        }

        public IUserRepository UserRepository { get; set; }
        
        public IMessageRepository MessageRepository { get; set; }

        public ICommand CreateCommand(
            string[] inputParts, 
            IUserRepository userRepository, 
            IMessageRepository messageRepository,
            ISubscriptionRepository subscriptionRepository, 
            IMessagePrinter messagePrinter)
        {
            return new PostMessageCommand
            {
                UserName = inputParts[0],
                MessageText = inputParts[2],
                UserRepository = userRepository,
                MessageRepository = messageRepository
            };
        }

    }
}