using DownKyi.ViewModels.PageViewModels;
using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace DownKyi.Services
{
    public interface IFavoritesService
    {
        Favorites GetFavorites(long mediaId);
        //void GetFavoritesMediaList(long mediaId, ObservableCollection<FavoritesMedia> result, IEventAggregator eventAggregator, CancellationToken cancellationToken);
        //void GetFavoritesMediaList(long mediaId, int pn, int ps, ObservableCollection<FavoritesMedia> result, IEventAggregator eventAggregator, CancellationToken cancellationToken);
        void GetFavoritesMediaList(List<Core.BiliApi.Favorites.Models.FavoritesMedia> medias, ObservableCollection<FavoritesMedia> result, IEventAggregator eventAggregator, CancellationToken cancellationToken);

        void GetCreatedFavorites(long mid, ObservableCollection<TabHeader> tabHeaders, CancellationToken cancellationToken);
        void GetCollectedFavorites(long mid, ObservableCollection<TabHeader> tabHeaders, CancellationToken cancellationToken);
    }
}
