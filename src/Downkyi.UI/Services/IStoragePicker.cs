namespace Downkyi.UI.Services;

public interface IStoragePicker
{
    /// <summary>
    /// 弹出选择文件夹弹窗
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    Task<string> FolderPicker(string directory = "");

    /// <summary>
    /// 选择视频dialog
    /// </summary>
    /// <returns></returns>
    Task<string> SelectVideoFileAsync();

    /// <summary>
    /// 选择多个视频dialog
    /// </summary>
    /// <returns></returns>
    Task<List<string>> SelectMultiVideoFileAsync();
}