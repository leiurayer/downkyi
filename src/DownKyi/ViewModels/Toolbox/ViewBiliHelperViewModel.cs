using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.Logging;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace DownKyi.ViewModels.Toolbox
{
    public class ViewBiliHelperViewModel : BaseViewModel
    {
        public const string Tag = "PageToolboxBiliHelper";

        #region 页面属性申明

        private string avid;
        public string Avid
        {
            get { return avid; }
            set { SetProperty(ref avid, value); }
        }

        private string bvid;
        public string Bvid
        {
            get { return bvid; }
            set { SetProperty(ref bvid, value); }
        }

        private string danmakuUserID;
        public string DanmakuUserID
        {
            get { return danmakuUserID; }
            set { SetProperty(ref danmakuUserID, value); }
        }

        private string userMid;
        public string UserMid
        {
            get { return userMid; }
            set { SetProperty(ref userMid, value); }
        }

        #endregion

        public ViewBiliHelperViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化
            #endregion
        }

        #region 命令申明

        // 输入avid事件
        private DelegateCommand<string> avidCommand;
        public DelegateCommand<string> AvidCommand => avidCommand ?? (avidCommand = new DelegateCommand<string>(ExecuteAvidCommand));

        /// <summary>
        /// 输入avid事件
        /// </summary>
        private async void ExecuteAvidCommand(string parameter)
        {
            if (parameter == null) { return; }
            if (!ParseEntrance.IsAvId(parameter)) { return; }

            long avid = ParseEntrance.GetAvId(parameter);
            if (avid == -1) { return; }

            await Task.Run(() =>
            {
                Bvid = BvId.Av2Bv((ulong)avid);
            });
        }

        // 输入bvid事件
        private DelegateCommand<string> bvidCommand;
        public DelegateCommand<string> BvidCommand => bvidCommand ?? (bvidCommand = new DelegateCommand<string>(ExecuteBvidCommand));

        /// <summary>
        /// 输入bvid事件
        /// </summary>
        /// <param name="parameter"></param>
        private async void ExecuteBvidCommand(string parameter)
        {
            if (parameter == null) { return; }
            if (!ParseEntrance.IsBvId(parameter)) { return; }

            await Task.Run(() =>
            {
                ulong avid = BvId.Bv2Av(parameter);
                Avid = $"av{avid}";
            });
        }

        // 访问网页事件
        private DelegateCommand gotoWebCommand;
        public DelegateCommand GotoWebCommand => gotoWebCommand ?? (gotoWebCommand = new DelegateCommand(ExecuteGotoWebCommand));

        /// <summary>
        /// 访问网页事件
        /// </summary>
        private void ExecuteGotoWebCommand()
        {
            string baseUrl = "https://www.bilibili.com/video/";
            System.Diagnostics.Process.Start(baseUrl + Bvid);
        }

        // 查询弹幕发送者事件
        private DelegateCommand findDanmakuSenderCommand;
        public DelegateCommand FindDanmakuSenderCommand => findDanmakuSenderCommand ?? (findDanmakuSenderCommand = new DelegateCommand(ExecuteFindDanmakuSenderCommand));

        /// <summary>
        /// 查询弹幕发送者事件
        /// </summary>
        private async void ExecuteFindDanmakuSenderCommand()
        {
            await Task.Run(() =>
            {
                try
                {
                    UserMid = DanmakuSender.FindDanmakuSender(DanmakuUserID);
                }
                catch (Exception e)
                {
                    UserMid = null;

                    Core.Utils.Debugging.Console.PrintLine("FindDanmakuSenderCommand()发生异常: {0}", e);
                    LogManager.Error(Tag, e);
                }
            });
        }

        // 访问用户空间事件
        private DelegateCommand visitUserSpaceCommand;
        public DelegateCommand VisitUserSpaceCommand => visitUserSpaceCommand ?? (visitUserSpaceCommand = new DelegateCommand(ExecuteVisitUserSpaceCommand));

        /// <summary>
        /// 访问用户空间事件
        /// </summary>
        private void ExecuteVisitUserSpaceCommand()
        {
            if (UserMid == null) { return; }

            string baseUrl = "https://space.bilibili.com/";
            System.Diagnostics.Process.Start(baseUrl + UserMid);
        }

        #endregion

    }
}
