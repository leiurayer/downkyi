using DownKyi.Core.Settings;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace DownKyi.ViewModels.Dialogs
{
    public class ViewParsingSelectorViewModel : BaseDialogViewModel
    {
        public const string Tag = "DialogParsingSelector";

        #region 页面属性申明

        private bool isParseDefault;
        public bool IsParseDefault
        {
            get { return isParseDefault; }
            set { SetProperty(ref isParseDefault, value); }
        }

        #endregion

        public ViewParsingSelectorViewModel()
        {
            #region 属性初始化

            Title = DictionaryResource.GetString("ParsingSelector");

            // 解析范围
            ParseScope parseScope = SettingsManager.GetInstance().GetParseScope();
            IsParseDefault = parseScope != ParseScope.NONE;

            #endregion

        }

        #region 命令申明

        // 解析当前项事件
        private DelegateCommand parseSelectedItemCommand;
        public DelegateCommand ParseSelectedItemCommand => parseSelectedItemCommand ?? (parseSelectedItemCommand = new DelegateCommand(ExecuteParseSelectedItemCommand));

        /// <summary>
        /// 解析当前项事件
        /// </summary>
        private void ExecuteParseSelectedItemCommand()
        {
            SetParseScopeSetting(ParseScope.SELECTED_ITEM);

            ButtonResult result = ButtonResult.OK;
            IDialogParameters parameters = new DialogParameters
            {
                { "parseScope", ParseScope.SELECTED_ITEM }
            };

            RaiseRequestClose(new DialogResult(result, parameters));
        }

        // 解析当前页视频事件
        private DelegateCommand parseCurrentSectionCommand;
        public DelegateCommand ParseCurrentSectionCommand => parseCurrentSectionCommand ?? (parseCurrentSectionCommand = new DelegateCommand(ExecuteParseCurrentSectionCommand));

        /// <summary>
        /// 解析当前页视频事件
        /// </summary>
        private void ExecuteParseCurrentSectionCommand()
        {
            SetParseScopeSetting(ParseScope.CURRENT_SECTION);

            ButtonResult result = ButtonResult.OK;
            IDialogParameters parameters = new DialogParameters
            {
                { "parseScope", ParseScope.CURRENT_SECTION }
            };

            RaiseRequestClose(new DialogResult(result, parameters));
        }

        // 解析所有视频事件
        private DelegateCommand parseAllCommand;
        public DelegateCommand ParseAllCommand => parseAllCommand ?? (parseAllCommand = new DelegateCommand(ExecuteParseAllCommand));

        /// <summary>
        /// 解析所有视频事件
        /// </summary>
        private void ExecuteParseAllCommand()
        {
            SetParseScopeSetting(ParseScope.ALL);

            ButtonResult result = ButtonResult.OK;
            IDialogParameters parameters = new DialogParameters
            {
                { "parseScope", ParseScope.ALL }
            };

            RaiseRequestClose(new DialogResult(result, parameters));
        }

        #endregion

        /// <summary>
        /// 写入设置
        /// </summary>
        /// <param name="parseScope"></param>
        private void SetParseScopeSetting(ParseScope parseScope)
        {
            if (IsParseDefault)
            {
                SettingsManager.GetInstance().SetParseScope(parseScope);
            }
            else
            {
                SettingsManager.GetInstance().SetParseScope(ParseScope.NONE);
            }
        }

    }
}
