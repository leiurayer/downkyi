namespace Downkyi.UI.Services.Event;

public class NotificationEvent : INotificationEvent
{
    private Action<string>? _action;
    private string _message = string.Empty;

    public void Publish(string msg)
    {
        _message = msg;
        Task.Run(() => _action?.Invoke(_message));
    }

    public void Subscribe(Action<string> action)
    {
        _action = action;
    }
}