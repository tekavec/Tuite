using Tuite.Commands;

namespace Tuite
{
    public interface ICommandParser
    {
        ICommand Parse(string input);
    }
}