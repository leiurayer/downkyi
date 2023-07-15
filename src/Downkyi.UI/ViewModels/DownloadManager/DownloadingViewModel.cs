using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.DownloadManager;

public class DownloadingViewModel : ViewModelBase
{
    public const string Key = "DownloadManager_Downloading";

    public DownloadingViewModel(BaseServices baseServices) : base(baseServices)
    {
    }
}