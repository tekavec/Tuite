using Tuite.Model.Users;

namespace Tuite.Model.Subscriptions
{
    public class Subscription
    {
        public User Follower { get; set; }
        public User Followee { get; set; }

        protected bool Equals(Subscription other)
        {
            return Equals(Follower, other.Follower) && Equals(Followee, other.Followee);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Subscription) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Follower != null ? Follower.GetHashCode() : 0)*397) ^ (Followee != null ? Followee.GetHashCode() : 0);
            }
        }
    }
}