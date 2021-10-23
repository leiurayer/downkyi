using DownKyi.Utils;
using DownKyi.ViewModels;
using DownKyi.ViewModels.Dialogs;
using DownKyi.ViewModels.DownloadManager;
using DownKyi.ViewModels.Settings;
using DownKyi.ViewModels.Toolbox;
using DownKyi.Views;
using DownKyi.Views.Dialogs;
using DownKyi.Views.DownloadManager;
using DownKyi.Views.Settings;
using DownKyi.Views.Toolbox;
using Prism.Ioc;
using System;
using System.Windows;

namespace DownKyi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            // 设置主题
            DictionaryResource.LoadTheme("ThemeDefault");
            //DictionaryResource.LoadTheme("ThemeDiy");

            // 切换语言
            DictionaryResource.LoadLanguage("Default");
            //DictionaryResource.LoadLanguage("en_US");

            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // pages
            containerRegistry.RegisterForNavigation<ViewIndex>(ViewIndexViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewLogin>(ViewLoginViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewVideoDetail>(ViewVideoDetailViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewSettings>(ViewSettingsViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDownloadManager>(ViewDownloadManagerViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewToolbox>(ViewToolboxViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewUserSpace>(ViewUserSpaceViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewMySpace>(ViewMySpaceViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewPublicFavorites>(ViewPublicFavoritesViewModel.Tag);

            // downloadManager pages
            containerRegistry.RegisterForNavigation<ViewDownloading>(ViewDownloadingViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDownloadFinished>(ViewDownloadFinishedViewModel.Tag);

            // settings pages
            containerRegistry.RegisterForNavigation<ViewBasic>(ViewBasicViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewNetwork>(ViewNetworkViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewVideo>(ViewVideoViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDanmaku>(ViewDanmakuViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewAbout>(ViewAboutViewModel.Tag);

            // tools pages
            containerRegistry.RegisterForNavigation<ViewBiliHelper>(ViewBiliHelperViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDelogo>(ViewDelogoViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewExtractMedia>(ViewExtractMediaViewModel.Tag);

            // dialogs
            containerRegistry.RegisterDialog<ViewDownloadSetter>(ViewDownloadSetterViewModel.Tag);
            containerRegistry.RegisterDialog<ViewParsingSelector>(ViewParsingSelectorViewModel.Tag);

        }

        /// <summary>
        /// 异步修改绑定到UI的属性
        /// </summary>
        /// <param name="callback"></param>
        public static void PropertyChangeAsync(Action callback)
        {
            Current.Dispatcher.Invoke(callback);
        }

    }
}
