namespace Tuite
{
    public class ConsoleController
    {
        private readonly IConsole _Console;
        private readonly IService _Service;
        private readonly char[] _Separators = { ' ' };
        private const string WallCommandString = "wall";
        private const string PostCommandString = "->";
        private const string FollowsCommandString = "follows";


        public ConsoleController(IService service, IConsole console)
        {
            _Console = console;
            _Service = service;
            _Console.RaiseReadLine += ProcessConsoleInput;
        }

        private void ProcessConsoleInput(object source, ReadLineEventArgs e)
        {
            //NOTE: refactor to interpreter if command grammar become too complex
            string[] parts = e.Input.Split(_Separators, 3);
            var length = parts.Length;
            if (length == 0)
            {
                return;
            }
            var userName = parts[0];
            if (length == 1)
            {
                _Service.ShowTimeline(userName);
            }
            else 
            {
                var commandString = parts[1];
                if (commandString == WallCommandString)
                {
                    _Service.ShowWall(userName);
                }
                else if (commandString == PostCommandString)
                {
                    _Service.CreateUserIfNecessaryAndPostMessage(userName, parts[2]);
                }
                else if (commandString == FollowsCommandString)
                {
                    _Service.Subscribe(userName, parts[2]);
                }
            }
        }

        public void PerformReadLine()
        {
            _Console.ReadLine();
        }
    }
}