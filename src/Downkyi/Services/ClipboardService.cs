using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Downkyi.UI.Services;
using System;
using System.Threading.Tasks;

namespace Downkyi.Services;

public class ClipboardService : IClipboardService
{
    public async Task<string> GetTextAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.Clipboard is not { } provider)
            throw new NullReferenceException("Missing Clipboard instance.");

        return await provider.GetTextAsync() ?? string.Empty;
    }

    public async Task SetTextAsync(string text)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.Clipboard is not { } provider)
            throw new NullReferenceException("Missing Clipboard instance.");

        await provider.SetTextAsync(text);
    }

    public Task SetImageAsync(object obj)
    {
        throw new NotImplementedException();
    }

}