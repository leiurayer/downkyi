using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DatabaseManager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;


        private string _title = "DatabaseManager";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }



        private DelegateCommand coverCommand;
        public DelegateCommand CoverCommand => coverCommand ?? (coverCommand = new DelegateCommand(ExecuteCoverCommand));

        private void ExecuteCoverCommand()
        {
            regionManager.RequestNavigate("ContentRegion", "Cover");
        }

        private DelegateCommand headerCommand;
        public DelegateCommand HeaderCommand => headerCommand ?? (headerCommand = new DelegateCommand(ExecuteHeaderCommand));

        private void ExecuteHeaderCommand()
        {
            regionManager.RequestNavigate("ContentRegion", "Header");
        }





    }
}
