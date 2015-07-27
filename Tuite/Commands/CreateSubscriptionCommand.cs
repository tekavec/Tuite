using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Commands
{
    public class CreateSubscriptionCommand : ICommand, ICommandFactory
    {

        public void Execute()
        {
            User follower = UserRepository.GetUser(FollowerName);
            User followee = UserRepository.GetUser(FolloweeName);
            SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(follower, followee);
        }

        public string Name
        {
            get { return "follows"; }
        }

        public string FolloweeName { get; set; }

        public string FollowerName { get; set; }
        
        public ISubscriptionRepository SubscriptionRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public ICommand CreateCommand(string[] inputParts, IUserRepository userRepository, IMessageRepository messageRepository,
            ISubscriptionRepository subscriptionRepository, IMessagePrinter messagePrinter)
        {
            return new CreateSubscriptionCommand
            {
                FollowerName = inputParts[0],
                FolloweeName = inputParts[2],
                SubscriptionRepository = subscriptionRepository,
                UserRepository = userRepository
            };
        }

    }
}