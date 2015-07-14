namespace Tuite
{
    public delegate void NotifyObserverHandler(object source, ReadLineEventArgs e);

    public interface IConsole
    {
        event NotifyObserverHandler RaiseReadLine;
        void ReadLine();
        void WriteLine(string value);
    }
}