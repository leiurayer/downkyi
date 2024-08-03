using Downkyi.Core.Storage;
using SQLite;

namespace Downkyi.Core.Database.Login;

public class LoginDatabase
{
    private const SQLiteOpenFlags _flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    private readonly string _databasePath = Constant.Login;

    private SQLiteAsyncConnection? _database;

    private async Task Init()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(_databasePath, _flags);
        await _database.CreateTableAsync<Cookies>();
        await _database.CreateTableAsync<Users>();
    }

    // 增删查改

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