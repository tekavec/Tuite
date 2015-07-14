using System.Collections.Generic;
using Tuite.Model.Users;

namespace Tuite.Model.Message
{
    public interface IMessageRepository
    {
        void AddMessage(User author, string text);
        IList<Message> MessagesOfUser(string name);
        IList<Message> MessagesOfSubscribedUsers(IList<User> aggregatedSubscriptionUserNames);
        IList<Message> AllMessages();
    }
}