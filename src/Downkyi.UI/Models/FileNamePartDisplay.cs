using CommunityToolkit.Mvvm.ComponentModel;
using Downkyi.Core.FileName;

namespace Downkyi.UI.Models;

public partial class FileNamePartDisplay : ObservableObject
{
    [ObservableProperty]
    public int _index;

    [ObservableProperty]
    public FileNamePart _id;

    [ObservableProperty]
    public string _title = string.Empty;
}