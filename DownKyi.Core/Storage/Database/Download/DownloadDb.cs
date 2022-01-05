using DownKyi.Core.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DownKyi.Core.Storage.Database.Download
{
    public class DownloadDb
    {
        private const string key = "bdb8eb69-3698-4af9-b722-9312d0fba623";
        private readonly DbHelper dbHelper = new DbHelper(StorageManager.GetDownload());
        //private readonly DbHelper dbHelper = new DbHelper(StorageManager.GetDownload(), key);
        protected string tableName = "download";

        //public DownloadDb()
        //{
        //    CreateTable();
        //}

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
        /// <param name="obj"></param>
        public void Insert(string uuid, object obj)
        {
            // 定义一个流
            Stream stream = new MemoryStream();
            // 定义一个格式化器
            BinaryFormatter formatter = new BinaryFormatter();
            // 序列化
            formatter.Serialize(stream, obj);

            byte[] array = null;
            array = new byte[stream.Length];

            //将二进制流写入数组
            stream.Position = 0;
            stream.Read(array, 0, (int)stream.Length);

            //关闭流
            stream.Close();

            try
            {
                string sql = $"insert into {tableName}(id, data) values (@id, @data)";
                dbHelper.ExecuteNonQuery(sql, new Action<SQLiteParameterCollection>((para) =>
                {
                    para.Add("@id", DbType.String).Value = uuid;
                    para.Add("@data", DbType.Binary).Value = array;
                }));
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Insert()发生异常: {0}", e);
                LogManager.Error("DownloadingDb", e);
            }
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Dictionary<string, object> QueryAll(string sql)
        {
            Dictionary<string, object> objects = new Dictionary<string, object>();

            dbHelper.ExecuteQuery(sql, reader =>
            {
                while (reader.Read())
                {
                    objects.Add((string)reader["id"], reader["data"]);
                }
            });
            return objects;
        }


        /// <summary>
        /// 如果表不存在则创建表
        /// </summary>
        protected void CreateTable()
        {
            string sql = $"create table if not exists {tableName} (id varchar(255), data blob)";
            dbHelper.ExecuteNonQuery(sql);
        }
    }
}
