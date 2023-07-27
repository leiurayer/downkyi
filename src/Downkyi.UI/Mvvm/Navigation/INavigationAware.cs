namespace Downkyi.UI.Mvvm.Navigation;

public interface INavigationAware
{
    void OnNavigatedTo(Dictionary<string, object>? parameter);

    void OnNavigatedFrom(Dictionary<string, object>? parameter);
}