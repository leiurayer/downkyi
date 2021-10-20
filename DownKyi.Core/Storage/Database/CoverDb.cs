using System.Collections.Generic;

namespace DownKyi.Core.Storage.Database
{
    public class CoverDb
    {
        private const string key = "b5018ecc-09d1-4da2-aa49-4625e41e623e";
        private readonly DbHelper dbHelper = new DbHelper(StorageManager.GetCoverIndex(), key);

        public CoverDb()
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
        /// <param name="cover"></param>
        public void Insert(Cover cover)
        {
            string sql = $"insert into cover values ({cover.Avid}, '{cover.Bvid}', {cover.Cid}, '{cover.Url}', '{cover.Md5}')";
            dbHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新数据，以url检索
        /// </summary>
        /// <param name="cover"></param>
        public void Update(Cover cover)
        {
            string sql = $"update cover set avid={cover.Avid}, bvid='{cover.Bvid}', cid={cover.Cid}, md5='{cover.Md5}' where url glob '{cover.Url}'";
            dbHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Cover> QueryAll()
        {
            string sql = $"select * from cover";
            return Query(sql);
        }

        /// <summary>
        /// 查询url对应的数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Cover QueryByUrl(string url)
        {
            string sql = $"select * from cover where url glob '{url}'";
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
        /// 查询md5对应的数据
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public Cover QueryByMd5(string md5)
        {
            string sql = $"select * from cover where md5 glob '{md5}'";
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
        private List<Cover> Query(string sql)
        {
            List<Cover> covers = new List<Cover>();

            dbHelper.ExecuteQuery(sql, reader =>
            {
                while (reader.Read())
                {
                    Cover cover = new Cover
                    {
                        Avid = (long)reader["avid"],
                        Bvid = (string)reader["bvid"],
                        Cid = (long)reader["cid"],
                        Url = (string)reader["url"],
                        Md5 = (string)reader["md5"]
                    };
                    covers.Add(cover);
                }
            });
            return covers;
        }

        /// <summary>
        /// 如果表不存在则创建表
        /// </summary>
        private void CreateTable()
        {
            string sql = "create table if not exists cover (avid unsigned big int, bvid varchar(20), cid unsigned big int, url varchar(255) unique, md5 varchar(32) unique)";
            dbHelper.ExecuteNonQuery(sql);
        }

    }
}
