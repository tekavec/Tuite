using System.Collections.Generic;
using NUnit.Framework;
using Tuite.Model.Subscriptions;
using Tuite.Model.Users;

namespace Tuite.Tests.UnitTests
{
    [TestFixture]
    public class SubscriptionRepositoryShould
    {
        private ISubscriptionRepository _SubscriptionRepository;

        [TestFixtureSetUp]
        public void Initialize()
        {
        }

        [Test]
        public void CreateAndStoreASubscription()
        {
            _SubscriptionRepository = new SubscriptionRepository();
            var follower = new User { Name = "Charlie" };
            var followee = new User { Name = "Alice" };

            _SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(follower, followee);

            var allSubscription = _SubscriptionRepository.AllSubscription();
            Assert.AreEqual(allSubscription.Count, 1);
            Assert.AreEqual(allSubscription[0], new Subscription { Followee = followee, Follower = follower });
        }

        [Test]
        public void NotCreateAndStoreASubscriptionIfAnyParticipantIsNull()
        {
            _SubscriptionRepository = new SubscriptionRepository();
            var user = new User { Name = "Alice" };

            _SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(user, null);
            _SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(null, user);

            var allSubscription = _SubscriptionRepository.AllSubscription();
            Assert.AreEqual(allSubscription.Count, 0);
        }

        [Test]
        public void RetrieveSubscribedUsers()
        {
            _SubscriptionRepository = new SubscriptionRepository();
            var charlie = new User { Name = "Charlie" };
            var alice = new User { Name = "Alice" };
            var bob = new User { Name = "Bob" };

            _SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(charlie, alice);
            _SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(charlie, bob);
            _SubscriptionRepository.AddSubscriptionIfNoneParticipantIsNull(bob, charlie);

            IList<User> followeesOfCharlie = _SubscriptionRepository.GetSubscribedUsersOfFollower(charlie.Name);
            Assert.AreEqual(followeesOfCharlie.Count, 2);
            IList<User> followeesOfBob = _SubscriptionRepository.GetSubscribedUsersOfFollower(bob.Name);
            Assert.AreEqual(followeesOfBob.Count, 1);

        }

    }
}
