using DownKyi.ViewModels.PageViewModels;
using Prism.Events;
using System.Collections.ObjectModel;

namespace DownKyi.Services
{
    public interface IFavoritesService
    {
        Favorites GetFavorites(long mediaId);
        void GetFavoritesMediaList(long mediaId, ObservableCollection<FavoritesMedia> result, IEventAggregator eventAggregator);
    }
}
