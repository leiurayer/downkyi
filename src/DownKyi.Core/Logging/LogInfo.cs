using System;

namespace DownKyi.Core.Logging
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// 线程id
        /// </summary>
        public int ThreadId { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 异常源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常对象
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 客户端代理
        /// </summary>
        public string UserAgent { get; set; }
    }
}