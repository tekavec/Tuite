namespace Tuite.Utils
{
    public class CommandNameResolver
    {
        private const string TimelineCommandString = "timeline";
        private readonly string[] _InputParts;

        public CommandNameResolver(string[] inputParts)
        {
            _InputParts = inputParts;
        }

        public string GetCommandName()
        {
            var length = _InputParts.Length;
            string commandName = string.Empty;
            if (length == 1)
            {
                commandName = TimelineCommandString;
            }
            else if (length > 1)
            {
                commandName = _InputParts[1];
            }
            return commandName;
        }
    }
}