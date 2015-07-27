namespace Tuite
{
    public class Console : IConsole
    {
        const string ReadPrompt = "> ";

        public virtual string ReadLine()
        {
            System.Console.Write(ReadPrompt); //not sure if needed but this way it works as in requrements
            return System.Console.ReadLine();
        }

        public virtual void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}