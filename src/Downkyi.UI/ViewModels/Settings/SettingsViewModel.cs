using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Settings;

public partial class SettingsViewModel : ViewModelBase
{
    public const string Key = "Settings";

    #region 页面属性申明

    [ObservableProperty]
    private int _selectedTabId = -1;

    [ObservableProperty]
    private List<TabHeader> _tabHeaders = new();

    #endregion

    public SettingsViewModel(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化

        TabHeaders.Add(new TabHeader { Id = 0, Title = DictionaryResource.GetString("Basic") });
        TabHeaders.Add(new TabHeader { Id = 1, Title = DictionaryResource.GetString("Network") });
        TabHeaders.Add(new TabHeader { Id = 2, Title = DictionaryResource.GetString("Video") });
        TabHeaders.Add(new TabHeader { Id = 3, Title = DictionaryResource.GetString("SettingDanmaku") });
        TabHeaders.Add(new TabHeader { Id = 4, Title = DictionaryResource.GetString("About") });

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