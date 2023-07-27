using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Enum;
using Downkyi.Core.Utils;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels;
using Downkyi.UI.ViewModels.DownloadManager;
using Downkyi.UI.ViewModels.Login;
using Downkyi.UI.ViewModels.Settings;
using Downkyi.UI.ViewModels.Toolbox;
using Downkyi.UI.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Downkyi.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public const string Key = "Main";

    private readonly LinkStack<string> _pages = new();

    private readonly CancellationTokenSource? tokenSource;

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

        // 监听剪贴板线程
        string oldClip = string.Empty;
        Task.Run(async () =>
        {
            CancellationToken cancellationToken = tokenSource!.Token;

            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.Clipboard is not { } provider)
                throw new NullReferenceException("Missing Clipboard instance.");
            await provider.SetTextAsync(oldClip);

            while (true)
            {
                AllowStatus isListenClipboard = SettingsManager.GetInstance().IsListenClipboard();
                if (isListenClipboard != AllowStatus.YES)
                {
                    continue;
                }

                // 判断是否该结束线程，若为true，跳出while循环
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                else
                {
                    string clip = await ClipboardService.GetTextAsync();
                    if (clip.Equals(oldClip))
                    {
                        await Task.Delay(100);
                        continue;
                    }

                    oldClip = clip;
                    MainSearchService.BiliInput(clip);
                }
            }
        }, (tokenSource = new CancellationTokenSource()).Token);

    }

    #region 命令申明

    /// <summary>
    /// 退出窗口时执行
    /// </summary>
    [RelayCommand]
    private void OnClosing()
    {
        // 取消任务
        tokenSource?.Cancel();
    }

    #endregion

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