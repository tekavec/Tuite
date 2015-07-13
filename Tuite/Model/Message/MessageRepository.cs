using System.Collections.Generic;
using System.Linq;
using Tuite.Model.Users;

namespace Tuite.Model.Message
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IClock _Clock;
        private readonly IList<Message> _Messages = new List<Message>();

        public MessageRepository(IClock clock)
        {
            _Clock = clock;
        }

        public void AddMessage(User author, string text)
        {
            _Messages.Add(new Message { Author = author, Text = text, Time = _Clock.CurrentDateAndTime });
        }

        public IList<Message> MessagesOfUser(string name)
        {
            return _Messages.Where(a => a.Author.Name.Equals(name)).ToList();
        }

        public IList<Message> MessagesOfSubscribedUsers(IList<User> aggregatedSubscriptionUserNames)
        {
            return _Messages.Where(a => aggregatedSubscriptionUserNames.Any(b => b.Equals(a.Author))).ToList();
        }

        public IList<Message> AllMessages()
        {
            return _Messages;
        }
    }
}
