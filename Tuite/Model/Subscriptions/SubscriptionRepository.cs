using System.Collections.Generic;
using System.Linq;
using Tuite.Model.Users;

namespace Tuite.Model.Subscriptions
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IList<Subscription> _Subscriptions = new List<Subscription>();

        public void AddSubscriptionIfNoneParticipantIsNull(User follower, User followee)
        {
            if (follower != null && followee != null)
            {
                _Subscriptions.Add(new Subscription{Followee = followee, Follower = follower});
            }
            //NOTE: checking for follower duplicates is not a request therefore is not implemented.
        }

        public IList<User> GetSubscribedUsersOf(User follower)
        {
            var followees = _Subscriptions.Where(a => a.Follower.Equals(follower)).Select(a => a.Followee).ToList();
            return followees;
        }

        public IList<Subscription> AllSubscription()
        {
            return _Subscriptions;
        }
    }
}