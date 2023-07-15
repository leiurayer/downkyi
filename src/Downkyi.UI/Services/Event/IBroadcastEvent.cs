namespace Downkyi.UI.Services.Event;

public interface IBroadcastEvent
{
    void Receive(string key, Action<object> action);

    void Send(string key, object obj);
}