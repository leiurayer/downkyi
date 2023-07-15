using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Toolbox;

public partial class ToolboxViewModel : ViewModelBase
{
    public const string Key = "Toolbox";

    #region 页面属性申明

    [ObservableProperty]
    private int _selectedTabId = -1;

    [ObservableProperty]
    private List<TabHeader> _tabHeaders = new();

    #endregion

    public ToolboxViewModel(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化

        TabHeaders.Add(new TabHeader { Id = 0, Title = DictionaryResource.GetString("BiliHelper") });
        TabHeaders.Add(new TabHeader { Id = 1, Title = DictionaryResource.GetString("Delogo") });
        TabHeaders.Add(new TabHeader { Id = 2, Title = DictionaryResource.GetString("ExtractMedia") });

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