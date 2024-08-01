namespace Downkyi.Core.Bili;

public static class BiliLocator
{
    private static ILogin? _login;
    public static ILogin Login
    {
        get
        {
            _login ??= new Web.Login();
            return _login;
        }
    }

    private static IUser? _user;
    public static IUser User
    {
        get
        {
            _user ??= new Web.User();
            return _user;
        }
    }

    private static IVideo? _video;
    public static IVideo Video(string input)
    {
        _video ??= new Web.Video(input);

        if (_video.Input() != input)
        {
            _video = new Web.Video(input);
        }

        return _video;
    }

}