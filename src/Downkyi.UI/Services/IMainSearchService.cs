namespace Downkyi.UI.Services;

public interface IMainSearchService
{
    /// <summary>
    /// 解析支持的输入，
    /// 支持的格式有：<para/>
    /// av号：av170001, AV170001, https://www.bilibili.com/video/av170001 <para/>
    /// BV号：BV17x411w7KC, https://www.bilibili.com/video/BV17x411w7KC, https://b23.tv/BV17x411w7KC <para/>
    /// 番剧（电影、电视剧）ss号：ss32982, SS32982, https://www.bilibili.com/bangumi/play/ss32982 <para/>
    /// 番剧（电影、电视剧）ep号：ep317925, EP317925, https://www.bilibili.com/bangumi/play/ep317925 <para/>
    /// 番剧（电影、电视剧）md号：md28228367, MD28228367, https://www.bilibili.com/bangumi/media/md28228367 <para/>
    /// 课程ss号：https://www.bilibili.com/cheese/play/ss205 <para/>
    /// 课程ep号：https://www.bilibili.com/cheese/play/ep3489 <para/>
    /// 收藏夹：ml1329019876, ML1329019876, https://www.bilibili.com/medialist/detail/ml1329019876, https://www.bilibili.com/medialist/play/ml1329019876/ <para/>
    /// 用户空间：uid928123, UID928123, uid:928123, UID:928123, https://space.bilibili.com/928123
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    bool BiliInput(string input);

    /// <summary>
    /// 搜索关键词
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    bool SearchKey(string input);
}