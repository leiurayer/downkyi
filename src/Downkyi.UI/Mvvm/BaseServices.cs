using Downkyi.UI.Services;
using Downkyi.UI.Services.Event;

namespace Downkyi.UI.Mvvm;

public class BaseServices
{
    public IBroadcastEvent BroadcastEvent { get; }

    public INotificationEvent NotificationEvent { get; }

    public IClipboardService ClipboardService { get; }

    public IDictionaryResource DictionaryResource { get; }

    public IMainSearchService MainSearchService { get; }

    public INavigationService NavigationService { get; }

    public IStoragePicker StoragePicker { get; }

    public IStorageService StorageService { get; }

    public BaseServices(IBroadcastEvent broadcastEvent,
        INotificationEvent notificationEvent,
        IClipboardService clipboardService,
        IDictionaryResource dictionaryResource,
        IMainSearchService mainSearchService,
        INavigationService navigationService,
        IStoragePicker storagePicker,
        IStorageService storageService)
    {
        BroadcastEvent = broadcastEvent;
        NotificationEvent = notificationEvent;
        ClipboardService = clipboardService;
        DictionaryResource = dictionaryResource;
        MainSearchService = mainSearchService;
        NavigationService = navigationService;
        StoragePicker = storagePicker;
        StorageService = storageService;
    }
}