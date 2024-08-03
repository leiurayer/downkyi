namespace Downkyi.UI.Services.VideoInfo;

public interface IVideoInfoServiceFactory
{
    IVideoInfoService Create(string input);
}