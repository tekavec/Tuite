using System.Collections.Generic;
using Tuite.Model.Message;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite
{
    public class Service : IService
    {
        private readonly IMessagePrinter _MessagePrinter;
        private readonly IMessageRepository _MessageRepository;
        private readonly ISubscriptionRepository _SubscriptionRepository;
        private readonly IUserRepository _UserRepository;

        public Service(
            IMessagePrinter messagePrinter, 
            IMessageRepository messageRepository, 
            ISubscriptionRepository subscriptionRepository, 
            IUserRepository userRepository)
        {
            _MessagePrinter = messagePrinter;
            _MessageRepository = messageRepository;
            _SubscriptionRepository = subscriptionRepository;
            _UserRepository = userRepository;
        }

        public void CreateUserIfNecessaryAndPostMessage(string name, string message)
        {
            _UserRepository.AddUser(name);
            User user = _UserRepository.GetUser(name);
            _MessageRepository.AddMessage(user, message);
        }

        public void ShowTimeline(string name)
        {
            IList<Message> messages = _MessageRepository.MessagesOfUser(name);
            _MessagePrinter.PrintTimeline(messages);
        }

        public void Subscribe(string followerName, string followeeName)
        {
            User follower = _UserRepository.GetUser(followerName);
            User followee = _UserRepository.GetUser(followeeName);
            _SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(follower, followee);
        }

        public void ShowWall(string name)
        {
            User user = _UserRepository.GetUser(name);
            if (user != null)
            {
                IList<User> aggregatedSubscriptions = _SubscriptionRepository.GetSubscribedUsersOf(user);
                aggregatedSubscriptions.Add(user);
                IList<Message> messages = _MessageRepository.MessagesOfSubscribedUsers(aggregatedSubscriptions);
                _MessagePrinter.PrintWall(messages);
            }
        }
    }
}