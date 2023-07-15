using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace Downkyi.UI.Models;

public partial class ThirdParty : ObservableObject
{
    #region 属性申明

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _author = string.Empty;

    [ObservableProperty]
    private string _version = string.Empty;

    [ObservableProperty]
    private string _license = string.Empty;

    [ObservableProperty]
    private string _homepage = string.Empty;

    [ObservableProperty]
    private string _licenseUrl = string.Empty;

    #endregion

    #region 命令申明

    [RelayCommand]
    private void VisitHomepage() { Process.Start(new ProcessStartInfo(Homepage) { UseShellExecute = true }); }

    [RelayCommand]
    private void GetLicense() { Process.Start(new ProcessStartInfo(LicenseUrl) { UseShellExecute = true }); }

    #endregion
}