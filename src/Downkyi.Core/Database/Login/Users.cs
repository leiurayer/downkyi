using SQLite;

namespace Downkyi.Core.Database.Login;

[Table("users")]
public class Users
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public long Id { get; set; }

    [Indexed(Unique = true)]
    [Column("uid")]
    public long Uid { get; set; }

    [Column("update_time")]
    public DateTime UpdateTime { get; set; } = DateTime.Now;

    [Column("create_time")]
    public DateTime CreateTime { get; set; } = DateTime.Now;
}