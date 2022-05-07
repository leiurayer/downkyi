using DownKyi.Images;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace DownKyi.ViewModels.Dialogs
{
    public class ViewAlertDialogViewModel : BaseDialogViewModel
    {
        public const string Tag = "DialogAlert";

        #region 页面属性申明

        private VectorImage image;
        public VectorImage Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        #endregion

        public ViewAlertDialogViewModel()
        {

        }

        #region 命令申明

        // 确认事件
        private DelegateCommand allowCommand;
        public DelegateCommand AllowCommand => allowCommand ?? (allowCommand = new DelegateCommand(ExecuteAllowCommand));

        /// <summary>
        /// 确认事件
        /// </summary>
        private void ExecuteAllowCommand()
        {
            ButtonResult result = ButtonResult.OK;
            RaiseRequestClose(new DialogResult(result));
        }

        #endregion

        #region 接口实现

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);

            Image = parameters.GetValue<VectorImage>("image");
            Title = parameters.GetValue<string>("title");
            Message = parameters.GetValue<string>("message");
        }

        #endregion

    }
}
