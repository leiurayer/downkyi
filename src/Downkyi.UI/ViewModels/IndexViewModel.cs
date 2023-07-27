using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Bili;
using Downkyi.Core.Log;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Models;
using Downkyi.Core.Storage;
using Downkyi.UI.Mvvm;
using Downkyi.UI.Services;
using Downkyi.UI.ViewModels.DownloadManager;
using Downkyi.UI.ViewModels.Login;
using Downkyi.UI.ViewModels.Settings;
using Downkyi.UI.ViewModels.Toolbox;
using Downkyi.UI.ViewModels.User;
using System.Text.RegularExpressions;

namespace Downkyi.UI.ViewModels;

public partial class IndexViewModel : ViewModelBase
{
    public const string Key = "Index";

    private const string defaultHeader = "avares://Downkyi/Assets/bili/default_header.jpg";

    #region 页面属性申明

    [ObservableProperty]
    private bool _loginPanelVisibility = false;

    [ObservableProperty]
    private string _header = defaultHeader;

    [ObservableProperty]
    private string? _userName = null;

    [ObservableProperty]
    private string _inputText = string.Empty;

    #endregion

    public IndexViewModel(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化
        #endregion
    }

    /// <summary>
    /// 导航到页面时执行
    /// </summary>
    /// <param name="parameter"></param>
    public override async void OnNavigatedTo(Dictionary<string, object>? parameter)
    {
        base.OnNavigatedTo(parameter);

        await UpdateUserInfo();
    }

    #region 命令申明

    /// <summary>
    /// 进入登录页面
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task LoginAsync()
    {
        if (UserName == null || UserName == string.Empty)
        {
            await NavigationService.ForwardAsync(LoginViewModel.Key);
        }
        else
        {
            // 进入用户空间
            var userInfo = SettingsManager.GetInstance().GetUserInfo();
            if (userInfo != null && userInfo.Mid != -1)
            {
                Dictionary<string, object> parameter = new()
                {
                    { "key", Key },
                    { "mid", userInfo.Mid },
                };
                await NavigationService.ForwardAsync(MySpaceViewModel.Key, parameter);
            }
        }
    }

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private Task InputAsync()
    {
        EnterBili();

        return Task.CompletedTask;
    }

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task SettingsAsync()
    {
        await NavigationService.ForwardAsync(SettingsViewModel.Key);
    }

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task DownloadManagerAsync()
    {
        await NavigationService.ForwardAsync(DownloadManagerViewModel.Key);
    }

    /// <summary>
    /// 进入工具箱页面
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task ToolboxAsync()
    {
        await NavigationService.ForwardAsync(ToolboxViewModel.Key);
    }

    #endregion

    #region 业务逻辑

    /// <summary>
    /// 进入B站链接的处理逻辑，
    /// 只负责处理输入，并跳转到视频详情页。<para/>
    /// 不是支持的格式，则进入搜索页面。
    /// </summary>
    private void EnterBili()
    {
        if (InputText == string.Empty)
        {
            return;
        }
        Log.Logger.Debug(InputText);

        InputText = Regex.Replace(InputText, @"[【]*[^【]*[^】]*[】 ]", "");

        bool isSupport = MainSearchService.BiliInput(InputText);
        if (!isSupport)
        {
            // 关键词搜索
            MainSearchService.SearchKey(InputText);
        }

        InputText = string.Empty;
    }

    private async Task UpdateUserInfo()
    {
        LoginPanelVisibility = false;

        // 检查本地是否存在login文件，没有则说明未登录
        if (!File.Exists(StorageManager.GetLogin()))
        {
            LoginPanelVisibility = true;
            Header = defaultHeader;
            UserName = null;
            return;
        }

        await Task.Run(() =>
        {
            // 获取用户信息
            var userInfo = BiliLocator.Login.GetNavigationInfo();
            if (userInfo != null)
            {
                SettingsManager.GetInstance().SetUserInfo(new UserInfoSettings
                {
                    Mid = userInfo.Mid,
                    Name = userInfo.Name,
                    IsLogin = userInfo.IsLogin,
                    IsVip = userInfo.VipStatus == 1
                });
            }
            else
            {
                SettingsManager.GetInstance().SetUserInfo(new UserInfoSettings
                {
                    Mid = -1,
                    Name = "",
                    IsLogin = false,
                    IsVip = false
                });
            }

            //
            LoginPanelVisibility = true;

            // 设置头像和用户名
            if (userInfo != null)
            {
                if (userInfo.Header != null)
                {
                    Header = userInfo.Header;
                }
                else
                {
                    Header = defaultHeader;
                }
                UserName = userInfo.Name;
            }
            else
            {
                Header = defaultHeader;
                UserName = null;
            }
        });

    }

    #endregion

}