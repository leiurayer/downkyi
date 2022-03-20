using DownKyi.Core.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DownKyi.Core.Storage.Database
{
    public class DbHelper
    {
        private readonly string connStr;
        private readonly SQLiteConnection conn;

        private static readonly Dictionary<string, SQLiteConnection> database = new Dictionary<string, SQLiteConnection>();

        /// <summary>
        /// 创建一个数据库
        /// </summary>
        /// <param name="dbPath"></param>
        public DbHelper(string dbPath)
        {
            connStr = $"Data Source={dbPath};Version=3;";

            if (database.ContainsKey(connStr))
            {
                conn = database[connStr];

                if (conn != null)
                {
                    return;
                }
            }

            conn = new SQLiteConnection(connStr);
            database.Add(connStr, conn);
        }

        /// <summary>
        /// 创建一个带密码的数据库
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="secretKey"></param>
        public DbHelper(string dbPath, string secretKey)
        {
            connStr = $"Data Source={dbPath};Version=3;";

            if (database.ContainsKey(connStr))
            {
                conn = database[connStr];

                if (conn != null)
                {
                    return;
                }
            }

            conn = new SQLiteConnection(connStr);
            conn.SetPassword(secretKey);
            database.Add(connStr, conn);
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

                database.Remove(connStr);
            }
        }

        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteNonQuery(string sql, Action<SQLiteParameterCollection> action = null)
        {
            try
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
            catch (SQLiteException e)
            {
                Utils.Debugging.Console.PrintLine("DbHelper ExecuteNonQuery()发生异常: {0}", e);
                LogManager.Error("DbHelper ExecuteNonQuery()", e);
            }
        }

        /// <summary>
        /// 执行一条SQL语句，并执行提供的操作，一般用于查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="action"></param>
        public void ExecuteQuery(string sql, Action<SQLiteDataReader> action)
        {
            try
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
            catch (SQLiteException e)
            {
                Utils.Debugging.Console.PrintLine("DbHelper ExecuteQuery()发生异常: {0}", e);
                LogManager.Error("DbHelper ExecuteQuery()", e);
            }
        }

    }
}
