namespace Downkyi.UI.Services;

public interface IDictionaryResource
{
    /// <summary>
    /// 从资源获取颜色的16进制字符串
    /// </summary>
    /// <param name="resourceKey"></param>
    /// <returns></returns>
    string GetColor(string resourceKey);

    /// <summary>
    /// 从资源获取字符串
    /// </summary>
    /// <param name="resourceKey"></param>
    /// <returns></returns>
    string GetString(string resourceKey);

    /// <summary>
    /// 根据languageCode切换界面语言
    /// </summary>
    /// <param name="languageCode"></param>
    void LoadLanguage(string languageCode);

    /// <summary>
    /// 切换主题
    /// </summary>
    /// <param name="theme"></param>
    void LoadTheme(string theme);
}