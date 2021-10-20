using DownKyi.Core.Storage.Database;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseManager.ViewModels
{
    public class ViewHeaderViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Header> headerList;
        public ObservableCollection<Header> HeaderList
        {
            get { return headerList; }
            set { SetProperty(ref headerList, value); }
        }

        public ViewHeaderViewModel()
        {
            HeaderList = new ObservableCollection<Header>();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await Task.Run(() =>
            {
                HeaderDb headerDb = new HeaderDb();
                var headers = headerDb.QueryAll();
                if (headers == null)
                {
                    return;
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    HeaderList.Clear();
                    foreach (var cover in headers)
                    {
                        HeaderList.Add(cover);
                    }
                }));

            });
        }
    }
}
