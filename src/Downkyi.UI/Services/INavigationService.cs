namespace Downkyi.UI.Services;

public interface INavigationService
{
    Task ForwardAsync(string viewKey);

    Task ForwardAsync(string viewKey, Dictionary<string, object> parameter);

    Task BackwardAsync();

    Task BackwardAsync(Dictionary<string, object> parameter);
}