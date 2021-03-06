﻿using System.Collections.Generic;
using Tuite.Model.Users;

namespace Tuite.Model.Subscriptions
{
    public interface ISubscriptionRepository
    {
        void AddSubscriptionIfNoneParticipantIsNull(User follower, User followee);
        IList<User> GetSubscribedUsersOf(User follower);
        IList<Subscription> AllSubscription();
    }
}