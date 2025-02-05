using SQLite;

namespace Downkyi.Core.Database.Login;

[Table("cookies")]
public class Cookies
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public long Id { get; set; }

    [Column("uid")]
    public long Uid { get; set; }

    [Column("key")]
    public string Key { get; set; } = string.Empty;

    [Column("value")]
    public string Value { get; set; } = string.Empty;
}