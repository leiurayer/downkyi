using Downkyi.Core.Log;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Settings;

public class BaseSettingsViewModel : ViewModelBase
{
    protected bool IsOnNavigatedTo;

    protected string TipSettingUpdated = string.Empty;

    protected string TipSettingFailed = string.Empty;

    public BaseSettingsViewModel(BaseServices baseServices) : base(baseServices)
    {
    }

    #region UI逻辑

    /// <summary>
    /// 发送需要显示的tip
    /// </summary>
    /// <param name="isSucceed"></param>
    protected void PublishTip(string key, bool isSucceed)
    {
        if (IsOnNavigatedTo) { return; }

        // 发送通知
        if (isSucceed)
        {
            //NotificationEvent.Publish(TipSettingUpdated);
            Log.Logger.Info($"{key}: {TipSettingUpdated}");
        }
        else
        {
            //NotificationEvent.Publish(TipSettingFailed);
            Log.Logger.Info($"{key}: {TipSettingFailed}");
        }
    }

    #endregion

}