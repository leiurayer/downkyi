using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.DownloadManager;

public class DownloadFinishedViewModel : ViewModelBase
{
    public const string Key = "DownloadManager_DownloadFinished";

    public DownloadFinishedViewModel(BaseServices baseServices) : base(baseServices)
    {
    }
}