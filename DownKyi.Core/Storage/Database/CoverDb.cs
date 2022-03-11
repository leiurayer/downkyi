using DownKyi.Core.Logging;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.Storage.Database
{
    public class CoverDb
    {
        private const string key = "b5018ecc-09d1-4da2-aa49-4625e41e623e";
        private readonly DbHelper dbHelper = new DbHelper(StorageManager.GetCoverIndex(), key);
        private readonly string tableName = "cover";

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
            try
            {
                string sql = $"insert into {tableName} values ({cover.Avid}, '{cover.Bvid}', {cover.Cid}, '{cover.Url}', '{cover.Md5}')";
                dbHelper.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Insert()发生异常: {0}", e);
                LogManager.Error("CoverDb", e);
            }
        }

        /// <summary>
        /// 更新数据，以url检索
        /// </summary>
        /// <param name="cover"></param>
        public void Update(Cover cover)
        {
            try
            {
                string sql = $"update {tableName} set avid={cover.Avid}, bvid='{cover.Bvid}', cid={cover.Cid}, md5='{cover.Md5}' where url glob '{cover.Url}'";
                dbHelper.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Update()发生异常: {0}", e);
                LogManager.Error("CoverDb", e);
            }

        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Cover> QueryAll()
        {
            string sql = $"select * from {tableName}";
            return Query(sql);
        }

        /// <summary>
        /// 查询url对应的数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Cover QueryByUrl(string url)
        {
            string sql = $"select * from {tableName} where url glob '{url}'";
            List<Cover> query = Query(sql);
            return query.Count > 0 ? query[0] : null;
        }

        /// <summary>
        /// 查询md5对应的数据
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public Cover QueryByMd5(string md5)
        {
            string sql = $"select * from {tableName} where md5 glob '{md5}'";
            List<Cover> query = Query(sql);
            return query.Count > 0 ? query[0] : null;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<Cover> Query(string sql)
        {
            List<Cover> covers = new List<Cover>();

            try
            {
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
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Query()发生异常: {0}", e);
                LogManager.Error($"{tableName}", e);
            }

            return covers;
        }

        /// <summary>
        /// 如果表不存在则创建表
        /// </summary>
        private void CreateTable()
        {
            string sql = $"create table if not exists {tableName} (avid unsigned big int, bvid varchar(20), cid unsigned big int, url varchar(255) unique, md5 varchar(32) unique)";
            dbHelper.ExecuteNonQuery(sql);
        }

    }
}
