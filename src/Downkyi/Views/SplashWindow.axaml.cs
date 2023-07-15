using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Downkyi.Models;
using System;

namespace Downkyi.Views;

public partial class SplashWindow : Window
{
    private readonly Action? _mainAction;

    public SplashWindow()
    {
    }

    public SplashWindow(Action mainAction)
    {
        InitializeComponent();

        nameVersion.Text = new AppInfo().VersionName;
        _mainAction = mainAction;
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        DummyLoad();
    }

    private async void DummyLoad()
    {
        // Do some background stuff here.
        //Task.Delay(1000);

        await Dispatcher.UIThread.InvokeAsync(async () =>
        {
            _mainAction?.Invoke();
            Close();
        });
    }

}