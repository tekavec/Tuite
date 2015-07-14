using System;

namespace Tuite.Model.Message
{
    public class Clock : IClock
    {

        DateTime IClock.CurrentDateAndTime
        {
            get { return DateTime.UtcNow; }
        }
    }
}