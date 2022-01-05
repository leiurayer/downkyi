using DownKyi.Core.FileName;
using Prism.Mvvm;

namespace DownKyi.ViewModels.Settings
{
    public class DisplayFileNamePart : BindableBase
    {
        public FileNamePart Id { get; set; }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}
