using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.DependencyInjection;
using Downkyi.UI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Downkyi.Services;

public class StoragePicker : IStoragePicker
{
    public async Task<string> FolderPicker(string directory = "")
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.StorageProvider is not { } provider)
            throw new NullReferenceException("Missing StorageProvider instance.");

        var storageProvider = desktop.MainWindow!.StorageProvider;
        FolderPickerOpenOptions options = new()
        {
            AllowMultiple = false,
            Title = Ioc.Default.GetRequiredService<IDictionaryResource>().GetString("SelectDirectory"),
            SuggestedStartLocation = await storageProvider.TryGetFolderFromPathAsync(directory)
        };

        var folder = await storageProvider.OpenFolderPickerAsync(options);
        if (folder.Count > 0)
        {
            return folder[0].Path.AbsolutePath;
        }

        return string.Empty;
    }

    public async Task<string> SelectVideoFileAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                   desktop.MainWindow?.StorageProvider is not { } provider)
            throw new NullReferenceException("Missing StorageProvider instance.");

        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(desktop.MainWindow);

        // Start async operation to open the dialog.
        var file = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = Ioc.Default.GetRequiredService<IDictionaryResource>().GetString("SelectVideoFile"),
            AllowMultiple = false,
            FileTypeFilter = new List<FilePickerFileType>
            {
                new FilePickerFileType("mp4")
                {
                    Patterns = new List<string>() { "*.mp4" }
                }
            }
        });

        if (file.Count > 0)
        {
            return HttpUtility.UrlDecode(file[0].Path.AbsolutePath);
        }

        return string.Empty;
    }

    public async Task<List<string>> SelectMultiVideoFileAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
                    desktop.MainWindow?.StorageProvider is not { } provider)
            throw new NullReferenceException("Missing StorageProvider instance.");

        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(desktop.MainWindow);

        // Start async operation to open the dialog.
        var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = Ioc.Default.GetRequiredService<IDictionaryResource>().GetString("SelectVideoFile"),
            AllowMultiple = true,
            FileTypeFilter = new List<FilePickerFileType>
            {
                new FilePickerFileType("mp4")
                {
                    Patterns = new List<string>() { "*.mp4" }
                }
            }
        });

        var result = new List<string>();
        foreach (var file in files)
        {
            result.Add(HttpUtility.UrlDecode(file.Path.AbsolutePath));
        }

        return result;
    }

}