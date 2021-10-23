using DownKyi.Models;
using System.Collections.Generic;

namespace DownKyi.Services
{
    public interface IFavoritesService
    {
        Favorites GetFavorites(long mediaId);
        List<FavoritesMedia> GetFavoritesMediaList(long mediaId);
    }
}
