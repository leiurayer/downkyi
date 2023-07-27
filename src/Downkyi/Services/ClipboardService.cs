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
}