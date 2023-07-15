using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Bili.Utils;
using Downkyi.Core.Log;
using Downkyi.UI.Mvvm;
using System.Diagnostics;

namespace Downkyi.UI.ViewModels.Toolbox;

public partial class BiliHelperViewModel : ViewModelBase
{
    public const string Key = "Toolbox_BiliHelper";

    #region 页面属性申明

    [ObservableProperty]
    private string _avid = string.Empty;

    [ObservableProperty]
    private string _bvid = string.Empty;

    [ObservableProperty]
    private string _danmakuUserID = string.Empty;

    [ObservableProperty]
    private string _userMid = string.Empty;

    #endregion

    public BiliHelperViewModel(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化
        #endregion
    }

    #region 命令申明

    /// <summary>
    /// 输入avid事件
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task SetAvid()
    {
        if (!ParseEntrance.IsAvId(Avid)) { return; }

        long avid = ParseEntrance.GetAvId(Avid);
        if (avid == -1) { return; }

        await Task.Run(() =>
        {
            Bvid = BvId.Av2Bv((ulong)avid);
        });
    }

    /// <summary>
    /// 输入bvid事件
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task SetBvid()
    {
        if (!ParseEntrance.IsBvId(Bvid)) { return; }

        await Task.Run(() =>
        {
            ulong avid = BvId.Bv2Av(Bvid);
            Avid = $"av{avid}";
        });
    }

    /// <summary>
    /// 访问视频网页事件
    /// </summary>
    [RelayCommand]
    private void GotoWeb()
    {
        string baseUrl = "https://www.bilibili.com/video/";
        Process.Start(new ProcessStartInfo(baseUrl + Bvid) { UseShellExecute = true });
    }

    /// <summary>
    /// 查询弹幕发送者事件
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task FindDanmakuSender()
    {
        await Task.Run(() =>
        {
            try
            {
                UserMid = DanmakuSender.FindDanmakuSender(DanmakuUserID);
            }
            catch (Exception e)
            {
                UserMid = string.Empty;

                Log.Logger.Error(e);
            }
        });
    }

    /// <summary>
    /// 访问用户空间事件
    /// </summary>
    [RelayCommand]
    private void VisitUserSpace()
    {
        if (UserMid == string.Empty) { return; }

        string baseUrl = "https://space.bilibili.com/";
        Process.Start(new ProcessStartInfo(baseUrl + UserMid) { UseShellExecute = true });
    }

    #endregion

}