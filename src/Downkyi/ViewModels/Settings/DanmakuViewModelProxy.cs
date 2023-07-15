using Avalonia.Media;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels.Settings;

namespace Downkyi.ViewModels.Settings;

public class DanmakuViewModelProxy : DanmakuViewModel
{
    public DanmakuViewModelProxy(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化

        var fonts = FontManager.Current.SystemFonts;
        foreach (var font in fonts)
        {
            Fonts.Add(font.Name);
        }

        #endregion
    }
}