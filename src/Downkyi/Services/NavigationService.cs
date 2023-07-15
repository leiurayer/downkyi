using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using Downkyi.UI.Services;
using Downkyi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Downkyi.Services;

public class NavigationService : INavigationService
{
    public NavigationService()
    {
    }

    public async Task ForwardAsync(string viewKey)
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (App.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow!.DataContext is MainWindowViewModel viewModel)
                {
                    viewModel.Forward(viewKey);
                }
            }
        });
    }

    public async Task ForwardAsync(string viewKey, Dictionary<string, object> parameter)
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (App.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow!.DataContext is MainWindowViewModel viewModel)
                {
                    viewModel.Forward(viewKey, parameter);
                }
            }
        });
    }

    public async Task BackwardAsync()
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (App.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow!.DataContext is MainWindowViewModel viewModel)
                {
                    viewModel.Backward();
                }
            }
        });
    }

    public async Task BackwardAsync(Dictionary<string, object> parameter)
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (App.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow!.DataContext is MainWindowViewModel viewModel)
                {
                    viewModel.Backward(parameter);
                }
            }
        });
    }
}