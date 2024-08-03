using Avalonia;
using NLog;
using System;

namespace Downkyi;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) =>
        BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    //[STAThread]
    //public static void Main(string[] args)
    //{
    //    try
    //    {
    //        // prepare and run your App here
    //        BuildAvaloniaApp()
    //            .StartWithClassicDesktopLifetime(args);
    //    }
    //    catch (Exception e)
    //    {
    //        // here we can work with the exception, for example add it to our log file
    //        Log.Logger.Fatal(e, "Something very bad happened");
    //    }
    //    finally
    //    {
    //        // This block is optional. 
    //        // Use the finally-block if you need to clean things up or similar
    //        LogManager.Shutdown();
    //    }
    //}

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        //GC.KeepAlive(typeof(SvgImageExtension).Assembly);
        //GC.KeepAlive(typeof(Avalonia.Svg.Skia.Svg).Assembly);

        return AppBuilder.Configure<App>()
                 .UsePlatformDetect()
                 .LogToTrace();
    }
}