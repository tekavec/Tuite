namespace Tuite
{
    public class Console : IConsole
    {
        public virtual event NotifyObserverHandler RaiseReadLine;
        const string CReadPrompt = "> ";

        public virtual void ReadLine()
        {
            System.Console.Write(CReadPrompt); //not sure if needed but this way it works as in requrements
            string input = System.Console.ReadLine();
            if (RaiseReadLine != null)
            {
                RaiseReadLine(this, new ReadLineEventArgs{Input = input});
            }
        }

        public virtual void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}
