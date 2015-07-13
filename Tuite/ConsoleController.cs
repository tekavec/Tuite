namespace Tuite
{
    public class ConsoleController
    {
        private readonly IConsole _Console;
        private readonly IService _Service;
        private readonly char[] _Separators = { ' ' };
        private const string CWallCommandString = "wall";
        private const string CPostCommandString = "->";
        private const string CFollowsCommandString = "follows";


        public ConsoleController(IService service, IConsole console)
        {
            _Console = console;
            _Service = service;
            _Console.RaiseReadLine += ProcessConsoleInput;
        }

        private void ProcessConsoleInput(object source, ReadLineEventArgs e)
        {
            //could use command pattern (if new commands request arise) 
            string[] parts = e.Input.Split(_Separators, 3);
            var length = parts.Length;
            switch (length)
            {
                case 1:
                    _Service.ShowTimeline(parts[0]);
                    break;
                case 2:
                    switch (parts[1])
                    {
                        case CWallCommandString:
                        {
                            _Service.ShowWall(parts[0]);
                            break;
                        }
                    }
                    break;
                case 3:
                {
                    switch (parts[1])
                    {
                        case CPostCommandString:
                            {
                                _Service.CreateUserIfNecessaryAndPostMessage(parts[0], parts[2]);
                                break;
                            }
                        case CFollowsCommandString:
                            {
                                _Service.Subscribe(parts[0], parts[2]);
                                break;
                            }
                    }
                    break;
                }
            }
        }

        public void PerformReadLine()
        {
            _Console.ReadLine();
        }
    }
}
