using System.Collections.Generic;

namespace DownKyi.Core.Storage.Database
{
    public class HeaderDb
    {
        private const string key = "7c1f1f40-7cdf-4d11-ad28-f0137a3c5308";
        private readonly DbHelper dbHelper = new DbHelper(StorageManager.GetHeaderIndex(), key);

        public HeaderDb()
        {
            CreateTable();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            dbHelper.Close();
        }

        /// <summary>
        /// 插入新的数据
        /// </summary>
        /// <param name="header"></param>
        public void Insert(Header header)
        {
            string sql = $"insert into header values ({header.Mid}, '{header.Name}', '{header.Url}', '{header.Md5}')";
            dbHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="header"></param>
        public void Update(Header header)
        {
            string sql = $"update header set name='{header.Name}', url='{header.Url}', md5='{header.Md5}' where mid={header.Mid}";
            dbHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Header> QueryAll()
        {
            string sql = $"select * from header";
            return Query(sql);
        }

        /// <summary>
        /// 查询mid对应的数据
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public Header QueryByMid(long mid)
        {
            string sql = $"select * from header where mid={mid}";
            var query = Query(sql);

            if (query.Count > 0)
            {
                return query[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<Header> Query(string sql)
        {
            List<Header> headers = new List<Header>();

            dbHelper.ExecuteQuery(sql, reader =>
            {
                while (reader.Read())
                {
                    Header header = new Header
                    {
                        Mid = (long)reader["mid"],
                        Name = (string)reader["name"],
                        Url = (string)reader["url"],
                        Md5=(string)reader["md5"]
                    };
                    headers.Add(header);
                }
            });
            return headers;
        }

        /// <summary>
        /// 如果表不存在则创建表
        /// </summary>
        private void CreateTable()
        {
            string sql = "create table if not exists header (mid unsigned big int unique, name varchar(255), url varchar(255), md5 varchar(32))";
            dbHelper.ExecuteNonQuery(sql);
        }

    }
}
