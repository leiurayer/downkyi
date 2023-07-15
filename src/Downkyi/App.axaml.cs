using AsyncImageLoader;
using AsyncImageLoader.Loaders;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CommunityToolkit.Mvvm.DependencyInjection;
using Downkyi.UI.Services;
using Downkyi.Views;

namespace Downkyi;

public partial class App : Application
{
    public App()
    {
    }

    public override void Initialize()
    {
        // Register handle task exception
        HandleTaskException.RegisterEvents();

        // Register services
        ServiceLocator.ConfigureServices();

        // …Ë÷√ÕºœÒª∫¥ÊŒ™¥≈≈Ã
        ImageLoader.AsyncImageLoader = new DiskCachedWebImageLoader();

        // …Ë÷√÷˜Ã‚        
        Ioc.Default.GetRequiredService<IDictionaryResource>().LoadTheme("Default");

        // «–ªª”Ô—‘
        Ioc.Default.GetRequiredService<IDictionaryResource>().LoadLanguage("Default");

        AvaloniaXamlLoader.Load(this);

        var accentColor = Color.Parse(Ioc.Default.GetRequiredService<IDictionaryResource>().GetColor("ColorPrimary"));
        Resources["SystemAccentColor"] = accentColor;
        //Resources["SystemAccentColorDark1"] = ChangeColorLuminosity(e.AccentColor1, -0.3);
        //Resources["SystemAccentColorDark2"] = ChangeColorLuminosity(e.AccentColor1, -0.5);
        //Resources["SystemAccentColorDark3"] = ChangeColorLuminosity(e.AccentColor1, -0.7);
        //Resources["SystemAccentColorLight1"] = ChangeColorLuminosity(e.AccentColor1, 0.3);
        //Resources["SystemAccentColorLight2"] = ChangeColorLuminosity(e.AccentColor1, 0.5);
        //Resources["SystemAccentColorLight3"] = ChangeColorLuminosity(e.AccentColor1, 0.7);

        //var hsl = Color.ToHsl(accentColor);
        //HslColor.FromHsl(hsl.H, hsl.S, 0.7);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            //ExpressionObserver.DataValidators.RemoveAll(x => x is DataAnnotationsValidationPlugin);
            //desktop.MainWindow = new MainWindow
            //{
            //    DataContext = ServiceLocator.MainWindowViewModel,
            //};
            desktop.MainWindow = new SplashWindow(() =>
            {
                var mainWindow = new MainWindow()
                {
                    DataContext = ServiceLocator.MainWindowViewModel,
                };

                mainWindow.Show();
                mainWindow.Focus();

                desktop.MainWindow = mainWindow;
            });
        }

        base.OnFrameworkInitializationCompleted();
    }
}