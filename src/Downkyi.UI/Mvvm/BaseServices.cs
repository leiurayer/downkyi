using Downkyi.UI.Services;
using Downkyi.UI.Services.Event;

namespace Downkyi.UI.Mvvm;

public class BaseServices
{
    public IBroadcastEvent BroadcastEvent { get; }

    public INotificationEvent NotificationEvent { get; }

    public IDictionaryResource DictionaryResource { get; }

    public INavigationService NavigationService { get; }

    public IStoragePicker StoragePicker { get; }

    public IStorageService StorageService { get; }

    public BaseServices(IBroadcastEvent broadcastEvent,
        INotificationEvent notificationEvent,
        IDictionaryResource dictionaryResource,
        INavigationService navigationService,
        IStoragePicker storagePicker,
        IStorageService storageService)
    {
        BroadcastEvent = broadcastEvent;
        NotificationEvent = notificationEvent;
        DictionaryResource = dictionaryResource;
        NavigationService = navigationService;
        StoragePicker = storagePicker;
        StorageService = storageService;
    }
}