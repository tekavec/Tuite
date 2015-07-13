using System;

namespace Tuite.Model.Message
{
    public interface IClock
    {
        DateTime CurrentDateAndTime { get; }
    }
}