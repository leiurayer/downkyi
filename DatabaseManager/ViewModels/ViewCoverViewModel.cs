using DownKyi.Core.Storage.Database;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseManager.ViewModels
{
    public class ViewCoverViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Cover> coverList;
        public ObservableCollection<Cover> CoverList
        {
            get { return coverList; }
            set { SetProperty(ref coverList, value); }
        }

        public ViewCoverViewModel()
        {
            CoverList = new ObservableCollection<Cover>();
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await Task.Run(() =>
            {
                CoverDb coverDb = new CoverDb();
                var covers = coverDb.QueryAll();
                if (covers == null)
                {
                    return;
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    CoverList.Clear();
                    foreach (var cover in covers)
                    {
                        //CoverModel newCover = new CoverModel
                        //{
                        //    Avid = cover.Avid,
                        //    Bvid = cover.Bvid,
                        //    Cid = cover.Cid,
                        //    Url = cover.Url,
                        //    Md5 = cover.Md5
                        //};

                        CoverList.Add(cover);
                    }
                }));

            });
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
