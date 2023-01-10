using DownKyi.Core.Aria2cNet.Client;
using DownKyi.Core.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.Core.Aria2cNet.Server
{
    public static class AriaServer
    {
        public static int ListenPort; // 服务器端口
        private static Process Server;

        /// <summary>
        /// 启动aria2c服务器
        /// </summary>
        /// <param name="listenPort"></param>
        /// <param name="output"></param>
        /// <param name="window"></param>
        /// <returns></returns>
        public static async Task<bool> StartServerAsync(AriaConfig config, Action<string> action)
        {
            // aria端口
            ListenPort = config.ListenPort;
            // aria目录
            string ariaDir = Environment.CurrentDirectory + "\\aria\\";
            //string ariaDir = StorageManager.GetAriaDir();
            // 会话文件
#if DEBUG
            string sessionFile = Path.Combine(ariaDir, "aira.session");

#else
            string sessionFile =Path.Combine(ariaDir, "aira.session.gz");
#endif
            // 日志文件
            string logFile = Path.Combine(ariaDir, "aira.log");
            // 自动保存会话文件的时间间隔
            int saveSessionInterval = 30;

            // --enable-rpc --rpc-listen-all=true --rpc-allow-origin-all=true --continue=true
            await Task.Run(() =>
            {
                // 创建目录和文件
                if (!Directory.Exists(ariaDir))
                {
                    Directory.CreateDirectory(ariaDir);
                }
                if (!File.Exists(sessionFile))
                {
                    var stream = File.Create(sessionFile);
                    stream.Close();
                }
                if (!File.Exists(logFile))
                {
                    var stream = File.Create(logFile);
                    stream.Close();
                }
                else
                {
                    // 日志文件存在，如果大于100M，则删除
                    try
                    {
                        var stream = File.Open(logFile, FileMode.Open);
                        if (stream.Length >= 10 * 1024 * 1024L)
                        {
                            stream.SetLength(0);
                        }
                        stream.Close();
                    }
                    catch (Exception e)
                    {
                        Utils.Debugging.Console.PrintLine("StartServerAsync()发生其他异常: {0}", e);
                        LogManager.Error("AriaServer", e);
                    }
                }

                // header 解析
                string headers = string.Empty;
                if (config.Headers != null)
                {
                    foreach (var header in config.Headers)
                    {
                        headers += $"--header=\"{header}\" ";
                    }
                }

                ExcuteProcess("aria2c.exe",
                    $"--enable-rpc --rpc-listen-all=true --rpc-allow-origin-all=true " +
                    $"--check-certificate=false " + // 解决问题 SSL/TLS handshake failure
                    $"--rpc-listen-port={config.ListenPort} " +
                    $"--rpc-secret={config.Token} " +
                    $"--input-file=\"{sessionFile}\" --save-session=\"{sessionFile}\" " +
                    $"--save-session-interval={saveSessionInterval} " +
                    $"--log=\"{logFile}\" --log-level={config.LogLevel.ToString("G").ToLower()} " + // log-level: 'debug' 'info' 'notice' 'warn' 'error'
                    $"--max-concurrent-downloads={config.MaxConcurrentDownloads} " + // 最大同时下载数(任务数)
                    $"--max-connection-per-server={config.MaxConnectionPerServer} " + // 同服务器连接数
                    $"--split={config.Split} " + // 单文件最大线程数
                                                 //$"--max-tries={config.MaxTries} retry-wait=3 " + // 尝试重连次数
                    $"--min-split-size={config.MinSplitSize}M " + // 最小文件分片大小, 下载线程数上限取决于能分出多少片, 对于小文件重要
                    $"--max-overall-download-limit={config.MaxOverallDownloadLimit} " + // 下载速度限制
                    $"--max-download-limit={config.MaxDownloadLimit} " + // 下载单文件速度限制
                    $"--max-overall-upload-limit={config.MaxOverallUploadLimit} " + // 上传速度限制
                    $"--max-upload-limit={config.MaxUploadLimit} " + // 上传单文件速度限制
                    $"--continue={config.ContinueDownload.ToString().ToLower()} " + // 断点续传
                    $"--allow-overwrite=true " + // 允许复写文件
                    $"--auto-file-renaming=false " +
                    $"--file-allocation={config.FileAllocation.ToString("G").ToLower()} " + // 文件预分配, none prealloc
                    $"{headers}" + // header
                    "",
                    null, (s, e) =>
                    {
                        if (e.Data == null || e.Data == "" || e.Data.Replace(" ", "") == "") { return; }

                        Utils.Debugging.Console.PrintLine(e.Data);
                        LogManager.Debug("AriaServer", e.Data);

                        action.Invoke(e.Data);
                    });
            });

            return true;
        }

        /// <summary>
        /// 关闭aria2c服务器，异步方法
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CloseServerAsync()
        {
            await AriaClient.ShutdownAsync();
            // 等待进程结束
            await Task.Run(() =>
            {
                Server.WaitForExit(30000);
                try
                {
                    Server.Kill();
                }
                catch (Exception)
                {
                }
            });
            return true;
        }

        /// <summary>
        /// 强制关闭aria2c服务器，异步方法
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> ForceCloseServerAsync()
        {
            //await Task.Run(() =>
            //{
            //    if (Server == null) { return; }

            //    Server.Kill();
            //    Server = null; // 将Server指向null
            //});
            //return true;
            await AriaClient.ForceShutdownAsync();
            return true;
        }

        /// <summary>
        /// 关闭aria2c服务器
        /// </summary>
        /// <returns></returns>
        public static bool CloseServer()
        {
            var task = AriaClient.ShutdownAsync();
            if (task.Result != null && task.Result.Result != null && task.Result.Result == "OK")
            {
                // 等待进程结束
                Server.WaitForExit(30000);
                try
                {
                    Server.Kill();
                }
                catch (Exception)
                {
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 强制关闭aria2c服务器
        /// </summary>
        /// <returns></returns>
        public static bool ForceCloseServer()
        {
            var task = AriaClient.ForceShutdownAsync();
            if (task.Result != null && task.Result.Result != null && task.Result.Result == "OK")
            { return true; }
            return false;
        }

        /// <summary>
        /// 杀死Aria进程
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool KillServer(string processName = "aria2c")
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch (Exception e)
                {
                    Utils.Debugging.Console.PrintLine("KillServer()发生异常: {0}", e);
                    LogManager.Error("AriaServer", e);
                }
            }
            return true;
        }


        private static void ExcuteProcess(string exe, string arg, string workingDirectory, DataReceivedEventHandler output)
        {
            var p = new Process();
            Server = p;

            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = arg;

            // 工作目录
            if (workingDirectory != null)
            {
                p.StartInfo.WorkingDirectory = workingDirectory;
            }

            // 输出信息重定向
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;

            // 将 StandardErrorEncoding 改为 UTF-8 才不会出现中文乱码
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            p.StartInfo.StandardErrorEncoding = Encoding.UTF8;

            p.OutputDataReceived += output;
            p.ErrorDataReceived += output;

            // 启动线程
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
        }

    }
}
