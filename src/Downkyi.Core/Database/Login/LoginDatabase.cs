using SQLite;

namespace Downkyi.Core.Database.Login;

public class LoginDatabase
{
    private readonly string _databasePath = Storage.StorageManager.GetLogin();

    private SQLiteAsyncConnection? _database;

    // 私有构造函数防止外部实例化
    private LoginDatabase()
    {
    }

    // 单例模式
    private static LoginDatabase? _instance;
    private static readonly object _lock = new();

    /// <summary>
    /// 获取LoginDatabase唯一实例
    /// </summary>
    public static LoginDatabase Instance
    {
        get
        {
            // 双重检查锁定
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new LoginDatabase();
                }
            }

            return _instance;
        }
    }

    private async Task Init()
    {
        if (_database != null)
            return;

        var options = new SQLiteConnectionString(_databasePath, true, key: "Bu1rj3jc");
        _database = new SQLiteAsyncConnection(options);
        await _database.CreateTableAsync<Cookies>();
        await _database.CreateTableAsync<Users>();
    }

    public async Task<int> AddCookiesAsync(Cookies cookies)
    {
        await Init();

        var user = await _database!.Table<Users>()
            .Where(i => i.Uid == cookies.Uid)
            .FirstOrDefaultAsync();
        if (user != null)
        {
            user.UpdateTime = DateTime.Now;
            await _database.UpdateAsync(user);
        }
        else
        {
            var newUser = new Users
            {
                Uid = cookies.Uid,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now,
            };
            await _database.InsertAsync(newUser);
        }

        if (cookies.Id != 0)
            return await _database.UpdateAsync(cookies);
        else
            return await _database.InsertAsync(cookies);
    }

    public async Task<int> DeleteCookiesAsync(Cookies cookies)
    {
        await Init();
        return await _database!.DeleteAsync(cookies);
    }

    public async Task<int> DeleteCookiesByUidAsync(long uid)
    {
        await Init();
        return await _database!.Table<Cookies>()
            .DeleteAsync(i => i.Uid == uid);
    }

    public async Task<List<Cookies>> GetCookiesAsync(long uid)
    {
        await Init();
        return await _database!.Table<Cookies>()
            .Where(i => i.Uid == uid)
            .ToListAsync();
    }
}