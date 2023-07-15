namespace Downkyi.UI.Services.Event;

public interface INotificationEvent
{
    void Subscribe(Action<string> action);

    void Publish(string msg);
}