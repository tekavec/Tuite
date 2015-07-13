using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Tuite.Model.Message;

namespace Tuite
{
    public class MessagePrinter : IMessagePrinter
    {
        private readonly IConsole _Console;
        private readonly IClock _Clock;

        public MessagePrinter(IConsole console, IClock clock)
        {
            _Console = console;
            _Clock = clock;
        }

        public void PrintTimeline(IList<Message> messages)
        {
            DateTime now = _Clock.CurrentDateAndTime;
            foreach (var message in messages.OrderByDescending(a => a.Time))
            {
                string humanizedTimeSpan = GetHumanizedTimeSpan(now.Subtract(message.Time));
                var formattedMessage = string.Format("{0} ({1} ago)", message.Text, humanizedTimeSpan);
                _Console.WriteLine(formattedMessage);
            }
        }

        public void PrintWall(IList<Message> messages)
        {
            DateTime now = _Clock.CurrentDateAndTime;
            foreach (var message in messages.OrderByDescending(a => a.Time))
            {
                string humanizedTimeSpan = GetHumanizedTimeSpan(now.Subtract(message.Time));
                var formattedMessage = string.Format("{0} - {1} ({2} ago)", message.Author.Name, message.Text, humanizedTimeSpan);
                _Console.WriteLine(formattedMessage);
            }
        }

        private string GetHumanizedTimeSpan(TimeSpan relativeTime)
        {
            if (relativeTime.Days > 0)
            {
                return TimeSpan.FromDays(relativeTime.Days).Humanize();
            }
            if (relativeTime.Hours > 0)
            {
                return TimeSpan.FromHours(relativeTime.Hours).Humanize();
            }
            if (relativeTime.Minutes > 0)
            {
                return TimeSpan.FromMinutes(relativeTime.Minutes).Humanize();
            }
            return TimeSpan.FromSeconds(relativeTime.Seconds).Humanize();
        }
    }
}