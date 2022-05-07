using DownKyi.Images;
using DownKyi.Utils;
using DownKyi.ViewModels.Dialogs;
using Prism.Services.Dialogs;

namespace DownKyi.Services
{
    public class AlertService
    {
        private readonly IDialogService dialogService;

        public AlertService(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        /// <summary>
        /// 显示一个信息弹窗
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ButtonResult ShowInfo(string message)
        {
            VectorImage image = SystemIcon.Instance().Info;
            string title = DictionaryResource.GetString("Info");
            return ShowMessage(image, title, message);
        }

        /// <summary>
        /// 显示一个警告弹窗
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ButtonResult ShowWarning(string message)
        {
            VectorImage image = SystemIcon.Instance().Warning;
            string title = DictionaryResource.GetString("Warning");
            return ShowMessage(image, title, message);
        }

        /// <summary>
        /// 显示一个错误弹窗
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ButtonResult ShowError(string message)
        {
            VectorImage image = SystemIcon.Instance().Error;
            string title = DictionaryResource.GetString("Error");
            return ShowMessage(image, title, message);
        }

        private ButtonResult ShowMessage(VectorImage image, string type, string message)
        {
            ButtonResult result = ButtonResult.None;
            if (dialogService == null)
            {
                return result;
            }

            DialogParameters param = new DialogParameters
            {
                { "image", image },
                { "title", type },
                { "message", message }
            };
            dialogService.ShowDialog(ViewAlertDialogViewModel.Tag, param, buttonResult =>
            {
                result = buttonResult.Result;
            });
            return result;
        }

    }
}
