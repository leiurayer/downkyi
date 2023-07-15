namespace Downkyi.UI.Services.Event;

public class BroadcastEvent : IBroadcastEvent
{
    private delegate void BroadcastedEventHandler(object obj, string key);
    private event BroadcastedEventHandler? Broadcasted;

    public void Receive(string key, Action<object> action)
    {
        Broadcasted += (obj, k) =>
        {
            if (key == k)
            {
                Task.Run(() => action?.Invoke(obj));
            }
        };
    }

    public void Send(string key, object obj)
    {
        Broadcasted?.Invoke(obj, key);
    }
}