using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels.Toolbox;

namespace Downkyi.ViewModels.Toolbox;

public partial class ExtractMediaViewModelProxy : ExtractMediaViewModel
{
    public ExtractMediaViewModelProxy(BaseServices baseServices) : base(baseServices)
    {
    }

    #region 命令申明

    /// <summary>
    /// Status改变事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetStatus(object parameter)
    {
        if (parameter is not TextBox output) { return; }

        // TextBox滚动到底部
        output.SelectionStart = Status.Length;
        output.SelectionEnd = Status.Length;
    }

    #endregion
}