using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.DownloadManager;

public partial class DownloadManagerViewModel : ViewModelBase
{
    public const string Key = "DownloadManager";

    #region 页面属性申明

    [ObservableProperty]
    private int _selectedTabId = -1;

    [ObservableProperty]
    private List<TabHeader> _tabHeaders = new();

    #endregion

    public DownloadManagerViewModel(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化

        TabHeaders.Add(new TabHeader { Id = 0, Title = DictionaryResource.GetString("Downloading") });
        TabHeaders.Add(new TabHeader { Id = 1, Title = DictionaryResource.GetString("DownloadFinished") });

        #endregion
    }

    #region 命令申明

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task BackwardAsync()
    {
        Dictionary<string, object> parameter = new()
        {
            { "key", Key },
        };

        await NavigationService.BackwardAsync(parameter);
    }

    #endregion

}