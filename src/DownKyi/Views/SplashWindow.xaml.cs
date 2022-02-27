using DownKyi.Models;
using System.Windows;

namespace DownKyi.Views
{
    /// <summary>
    /// SplashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SplashWindow : Window
    {
        public SplashWindow()
        {
            InitializeComponent();

            nameVersion.Text = new AppInfo().VersionName;
        }
    }
}
