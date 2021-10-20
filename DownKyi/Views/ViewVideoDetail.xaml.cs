using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace DownKyi.Views
{
    /// <summary>
    /// Interaction logic for ViewVideoDetail
    /// </summary>
    public partial class ViewVideoDetail : UserControl
    {
        public ViewVideoDetail()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 由于预览事件tunneling这将阻止rightmousebuttondown在listviewitems上发生，从而阻止它们被选中，
        /// 但不会阻止rightmousebuttondown在listview上出现，因此仍然允许contextmenu打开。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewItemPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
#if DEBUG
            Trace.WriteLine("Preview MouseRightButtonDown");
#endif
            e.Handled = true;
        }

    }
}
