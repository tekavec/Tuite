using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Commands
{
    public class ShowTimelineCommand : ICommand, ICommandFactory
    {

        public void Execute()
        {
            MessagePrinter.PrintTimeline(MessageRepository.MessagesOfUser(UserName));
        }

        public string Name {
            get { return "timeline"; }
        }

        public string UserName { get; set; }

        public IMessagePrinter MessagePrinter { get; set; }

        public IMessageRepository MessageRepository { get; set; }

        public ICommand CreateCommand(string[] inputParts, IUserRepository userRepository, IMessageRepository messageRepository,
            ISubscriptionRepository subscriptionRepository, IMessagePrinter messagePrinter)
        {
            return new ShowTimelineCommand { MessagePrinter = messagePrinter, UserName = inputParts[0], MessageRepository = messageRepository };
        }

    }
}