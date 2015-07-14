using System;
using Tuite.Model.Users;

namespace Tuite.Model.Message
{
    public class Message
    {
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        protected bool Equals(Message other)
        {
            return Equals(Author, other.Author) && Time.Equals(other.Time) && string.Equals(Text, other.Text);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Message) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Time.GetHashCode();
                hashCode = (hashCode*397) ^ (Text != null ? Text.GetHashCode() : 0);
                return hashCode;
            }
        }
        
    }
}