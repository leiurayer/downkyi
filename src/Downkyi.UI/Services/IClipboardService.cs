namespace Downkyi.UI.Services;

public interface IClipboardService
{
    Task<string> GetTextAsync();
}