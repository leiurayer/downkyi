namespace Downkyi.UI.Services;

public interface IClipboardService
{
    Task<string> GetTextAsync();

    Task SetTextAsync(string text);

    Task SetImageAsync(object obj);
}