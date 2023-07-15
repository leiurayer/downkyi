using CommunityToolkit.Mvvm.ComponentModel;
using Downkyi.UI.Mvvm.Navigation;
using Downkyi.UI.Services;
using Downkyi.UI.Services.Event;

namespace Downkyi.UI.Mvvm;

public abstract class ViewModelBase : ObservableObject, INavigationAware
{
    private readonly BaseServices _baseServices;

    protected IBroadcastEvent BroadcastEvent => _baseServices.BroadcastEvent;
    protected INotificationEvent NotificationEvent => _baseServices.NotificationEvent;
    protected IDictionaryResource DictionaryResource => _baseServices.DictionaryResource;
    protected INavigationService NavigationService => _baseServices.NavigationService;
    protected IStoragePicker StoragePicker => _baseServices.StoragePicker;
    protected IStorageService StorageService => _baseServices.StorageService;

    public ViewModelBase(BaseServices baseServices)
    {
        _baseServices = baseServices;
    }

    public virtual void OnNavigatedTo(Dictionary<string, object>? parameter)
    {
    }

    public virtual void OnNavigatedFrom(Dictionary<string, object>? parameter)
    {
    }
}