using System.Windows.Forms;

namespace DownKyi.Utils
{
    public static class DialogUtils
    {

        /// <summary>
        /// 弹出选择文件夹弹窗
        /// </summary>
        /// <returns></returns>
        public static string SetDownloadDirectory()
        {
            // 选择文件夹
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = DictionaryResource.GetString("SelectDirectory")
            };

            DialogResult showDialog = dialog.ShowDialog();
            return showDialog == DialogResult.OK || showDialog == DialogResult.Yes
                ? string.IsNullOrEmpty(dialog.SelectedPath) ? "" : dialog.SelectedPath : "";
        }

    }
}
