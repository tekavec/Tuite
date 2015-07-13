namespace Tuite
{
    public interface IService
    {
        void CreateUserIfNecessaryAndPostMessage(string name, string message);
        void ShowTimeline(string name);
        void Subscribe(string followerName, string followeeName);
        void ShowWall(string name);
    }
}
