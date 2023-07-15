using Downkyi.Models;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels.Settings;
using System.Threading.Tasks;

namespace Downkyi.ViewModels.Settings;

public class AboutViewModelProxy : AboutViewModel
{
    public AboutViewModelProxy(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化

        AppInfo app = new();
        AppName = app.Name;
        AppVersion = app.VersionName;

        AddThirdParty();

        #endregion
    }

    private async void AddThirdParty()
    {
        await Task.Run(() =>
        {
            ThirdParties.Add(new()
            {
                Name = "Aria2cNet",
                Author = "Leiurayer",
                Version = "1.0.1",
                License = "MIT",
                Homepage = "https://github.com/leiurayer/Aria2cNet",
                LicenseUrl = "https://licenses.nuget.org/MIT"
            });
            ThirdParties.Add(new()
            {
                Name = "AsyncImageLoader.Avalonia",
                Author = "SKProCH",
                Version = "3.0.0-avalonia11-preview4",
                License = "LICENSE",
                Homepage = "https://github.com/AvaloniaUtils/AsyncImageLoader.Avalonia",
                LicenseUrl = "https://github.com/AvaloniaUtils/AsyncImageLoader.Avalonia/blob/master/LICENSE"
            });
            ThirdParties.Add(new()
            {
                Name = "Avalonia",
                Author = "Avalonia Team",
                Version = "11.0.0",
                License = "MIT",
                Homepage = "https://avaloniaui.net/",
                LicenseUrl = "https://licenses.nuget.org/MIT"
            });
            ThirdParties.Add(new()
            {
                Name = "CommunityToolkit.Mvvm",
                Author = "Microsoft",
                Version = "8.2.1",
                License = "MIT",
                Homepage = "https://github.com/CommunityToolkit/dotnet",
                LicenseUrl = "https://licenses.nuget.org/MIT"
            });
            ThirdParties.Add(new()
            {
                Name = "MessageBox.Avalonia",
                Author = "Lary",
                Version = "3.0.0",
                License = "MIT",
                Homepage = "https://github.com/CreateLab/MessageBox.Avalonia",
                LicenseUrl = "https://licenses.nuget.org/MIT"
            });
            ThirdParties.Add(new()
            {
                Name = "Newtonsoft.Json",
                Author = "James Newton-King",
                Version = "13.0.3",
                License = "MIT",
                Homepage = "https://www.newtonsoft.com/json",
                LicenseUrl = "https://licenses.nuget.org/MIT"
            });
            ThirdParties.Add(new()
            {
                Name = "NLog",
                Author = "Jarek Kowalski et al.",
                Version = "5.2.2",
                License = "BSD-3-Clause",
                Homepage = "https://nlog-project.org/",
                LicenseUrl = "https://licenses.nuget.org/BSD-3-Clause"
            });
            ThirdParties.Add(new()
            {
                Name = "QRCoder",
                Author = "Raffael Herrmann",
                Version = "1.4.3",
                License = "MIT",
                Homepage = "https://github.com/codebude/QRCoder/",
                LicenseUrl = "https://licenses.nuget.org/MIT"
            });

            ThirdParties.Add(new()
            {
                Name = "sqlite-net-pcl",
                Author = "SQLite-net",
                Version = "1.8.116",
                License = "LICENSE",
                Homepage = "https://github.com/praeclarum/sqlite-net",
                LicenseUrl = "https://www.nuget.org/packages/sqlite-net-pcl/1.8.116/license"
            });
        });
    }

}