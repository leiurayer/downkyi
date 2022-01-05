using System;
using System.Data.SQLite;

namespace DownKyi.Core.Storage.Database
{
    public class DbHelper
    {
        private readonly SQLiteConnection conn;

        /// <summary>
        /// 创建一个数据库
        /// </summary>
        /// <param name="dbPath"></param>
        public DbHelper(string dbPath)
        {
            string connStr = $"Data Source={dbPath};Version=3;";
            conn = new SQLiteConnection(connStr);
        }

        /// <summary>
        /// 创建一个带密码的数据库
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="secretKey"></param>
        public DbHelper(string dbPath, string secretKey)
        {
            string connStr = $"Data Source={dbPath};Version=3;";
            conn = new SQLiteConnection(connStr);
            conn.SetPassword(secretKey);
        }

        /// <summary>
        /// 连接是否开启
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            return conn.State == System.Data.ConnectionState.Open;
        }

        /// <summary>
        /// 开启连接
        /// </summary>
        public void Open()
        {
            if (!IsOpen())
            {
                conn.Open();
            }
        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void Close()
        {
            if (IsOpen())
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteNonQuery(string sql, Action<SQLiteParameterCollection> action = null)
        {
            lock (conn)
            {
                Open();
                using (var tr = conn.BeginTransaction())
                {
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = sql;
                        // 添加参数
                        action?.Invoke(command.Parameters);
                        command.ExecuteNonQuery();
                    }
                    tr.Commit();
                }
            }
        }

        /// <summary>
        /// 执行一条SQL语句，并执行提供的操作，一般用于查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="action"></param>
        public void ExecuteQuery(string sql, Action<SQLiteDataReader> action)
        {
            lock (conn)
            {
                Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    var reader = command.ExecuteReader();
                    action(reader);
                }
            }
        }

    }
}
