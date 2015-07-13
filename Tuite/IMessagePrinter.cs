using System.Collections.Generic;
using Tuite.Model.Message;

namespace Tuite
{
    public interface IMessagePrinter
    {
        void PrintTimeline(IList<Message> messages);
        void PrintWall(IList<Message> messages);
    }
}
