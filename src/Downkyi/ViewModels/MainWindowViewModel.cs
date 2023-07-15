using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Downkyi.Core.Utils;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels;
using Downkyi.UI.ViewModels.DownloadManager;
using Downkyi.UI.ViewModels.Login;
using Downkyi.UI.ViewModels.Settings;
using Downkyi.UI.ViewModels.Toolbox;
using Downkyi.UI.ViewModels.User;
using System.Collections.Generic;
using System.Threading;

namespace Downkyi.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public const string Key = "Main";

    private readonly LinkStack<string> _pages = new();

    #region 页面属性申明

    [ObservableProperty]
    private ViewModelBase? _content = null;

    [ObservableProperty]
    private bool _messageVisibility = false;

    [ObservableProperty]
    private string _message = string.Empty;

    #endregion

    public MainWindowViewModel(BaseServices baseServices) : base(baseServices)
    {
        Dictionary<string, object> parameter = new()
        {
            { "key", Key },
            { "result", "start" },
        };
        Forward(IndexViewModel.Key, parameter);

        // 订阅消息发送事件
        string oldMessage;
        NotificationEvent.Subscribe((message) =>
        {
            oldMessage = Message;
            Message = message;
            int sleep = 2000;
            if (oldMessage == Message) { sleep = 1500; }

            MessageVisibility = true;
            Thread.Sleep(sleep);
            MessageVisibility = false;
        });

    }

    public void Forward(string viewKey, Dictionary<string, object>? parameter = null)
    {
        var viewModel = SetContent(viewKey);
        if (viewModel == null) { return; }

        viewModel.OnNavigatedTo(parameter);
        Content = viewModel;

        _pages.Push(viewKey);
    }

    public void Backward(Dictionary<string, object>? parameter = null)
    {
        _pages.Pop();
        var viewModel = SetContent(_pages.Peek());
        if (viewModel == null) { return; }

        viewModel.OnNavigatedTo(parameter);
        Content = viewModel;
    }

    private static ViewModelBase? SetContent(string? viewKey)
    {
        if (viewKey == null) { return null; }

        ViewModelBase? viewModel = null;
        switch (viewKey)
        {
            case IndexViewModel.Key:
                viewModel = Ioc.Default.GetRequiredService<IndexViewModel>();
                break;
            case LoginViewModel.Key:
                viewModel = Ioc.Default.GetRequiredService<LoginViewModel>();
                break;
            case MySpaceViewModel.Key:
                viewModel = Ioc.Default.GetRequiredService<MySpaceViewModel>();
                break;
            case UserSpaceViewModel.Key:
                viewModel = Ioc.Default.GetRequiredService<UserSpaceViewModel>();
                break;
            case SettingsViewModel.Key:
                viewModel = Ioc.Default.GetRequiredService<SettingsViewModel>();
                break;
            case DownloadManagerViewModel.Key:
                viewModel = Ioc.Default.GetRequiredService<DownloadManagerViewModel>();
                break;
            case ToolboxViewModel.Key:
                viewModel = Ioc.Default.GetRequiredService<ToolboxViewModel>();
                break;
            default:
                break;
        }
        return viewModel;
    }

}