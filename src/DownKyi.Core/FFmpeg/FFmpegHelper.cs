using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DownKyi.Core.FFmpeg
{
    public static class FFmpegHelper
    {
        private const string Tag = "FFmpegHelper";

        /// <summary>
        /// 合并音频和视频
        /// </summary>
        /// <param name="video1">音频</param>
        /// <param name="video2">视频</param>
        /// <param name="destVideo"></param>
        public static bool MergeVideo(string video1, string video2, string destVideo)
        {
            string param = $"-i \"{video1}\" -i \"{video2}\" -acodec copy -vcodec copy -f mp4 \"{destVideo}\"";
            if (video1 == null || !File.Exists(video1))
            {
                param = $"-i \"{video2}\" -acodec copy -vcodec copy -f mp4 \"{destVideo}\"";
            }
            if (video2 == null || !File.Exists(video2))
            {
                param = $"-i \"{video1}\" -acodec copy -f aac \"{destVideo}\"";
            }
            if (!File.Exists(video1) && !File.Exists(video2)) { return false; }

            // 如果存在
            try { File.Delete(destVideo); }
            catch (IOException e)
            {
                Console.WriteLine("MergeVideo()发生IO异常: {0}", e);
                Logging.LogManager.Error(Tag, e);
                return false;
            }

            ExcuteProcess("ffmpeg.exe", param, null, (s, e) => Console.WriteLine(e.Data));

            try
            {
                if (video1 != null) { File.Delete(video1); }
                if (video2 != null) { File.Delete(video2); }
            }
            catch (IOException e)
            {
                Console.WriteLine("MergeVideo()发生IO异常: {0}", e);
                Logging.LogManager.Error(Tag, e);
            }

            return true;
        }

        /// <summary>
        /// 拼接多个视频
        /// </summary>
        /// <param name="workingDirectory"></param>
        /// <param name="flvFiles"></param>
        /// <param name="destVideo"></param>
        /// <returns></returns>
        public static bool ConcatVideo(string workingDirectory, List<string> flvFiles, string destVideo)
        {
            // contact的文件名，不包含路径
            string concatFileName = Guid.NewGuid().ToString("N") + "_concat.txt";
            try
            {
                string contact = "";
                foreach (string flv in flvFiles)
                {
                    contact += $"file '{flv}'\n";
                }

                FileStream fileStream = new FileStream(workingDirectory + "/" + concatFileName, FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                //开始写入
                streamWriter.Write(contact);
                //清空缓冲区
                streamWriter.Flush();
                //关闭流
                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ConcatVideo()发生异常: {0}", e);
                Logging.LogManager.Error(Tag, e);
                return false;
            }

            // ffmpeg -f concat -safe 0 -i filelist.txt -c copy output.mkv
            // 加上-y，表示如果有同名文件，则默认覆盖
            string param = $"-f concat -safe 0 -i {concatFileName} -c copy \"{destVideo}\" -y";
            ExcuteProcess("ffmpeg.exe", param, workingDirectory, (s, e) => Console.WriteLine(e.Data));

            // 删除临时文件
            try
            {
                // 删除concat文件
                File.Delete(workingDirectory + "/" + concatFileName);

                foreach (string flv in flvFiles)
                {
                    File.Delete(flv);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ConcatVideo()发生异常: {0}", e);
                Logging.LogManager.Error(Tag, e);
            }

            return true;
        }

        /// <summary>
        /// 去水印，非常消耗cpu资源
        /// </summary>
        /// <param name="video"></param>
        /// <param name="destVideo"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="action"></param>
        public static void Delogo(string video, string destVideo, int x, int y, int width, int height, Action<string> action)
        {
            // ffmpeg -i "video.mp4" -vf delogo=x=1670:y=50:w=180:h=70:show=1 "delogo.mp4"
            string param = $"-i \"{video}\" -vf delogo=x={x}:y={y}:w={width}:h={height}:show=0 \"{destVideo}\" -hide_banner -y";
            ExcuteProcess("ffmpeg.exe", param, null, (s, e) =>
            {
                Console.WriteLine(e.Data);
                action.Invoke(e.Data);
            });
        }

        /// <summary>
        /// 从一个视频中仅提取音频
        /// </summary>
        /// <param name="video">源视频</param>
        /// <param name="audio">目标音频</param>
        /// <param name="action">输出信息</param>
        public static void ExtractAudio(string video, string audio, Action<string> action)
        {
            // 抽取音频命令
            // ffmpeg -i 3.mp4 -vn -y -acodec copy 3.aac
            // ffmpeg -i 3.mp4 -vn -y -acodec copy 3.m4a
            string param = $"-i \"{video}\" -vn -y -acodec copy \"{audio}\" -hide_banner";
            ExcuteProcess("ffmpeg.exe", param,
                null, (s, e) =>
                {
                    Console.WriteLine(e.Data);
                    action.Invoke(e.Data);
                });
        }

        /// <summary>
        /// 从一个视频中仅提取视频
        /// </summary>
        /// <param name="video">源视频</param>
        /// <param name="destVideo">目标视频</param>
        /// <param name="action">输出信息</param>
        public static void ExtractVideo(string video, string destVideo, Action<string> action)
        {
            // 提取视频 （Extract Video）
            // ffmpeg -i Life.of.Pi.has.subtitles.mkv -vcodec copy –an videoNoAudioSubtitle.mp4
            string param = $"-i \"{video}\" -y -vcodec copy -an \"{destVideo}\" -hide_banner";
            ExcuteProcess("ffmpeg.exe", param,
                null, (s, e) =>
                {
                    Console.WriteLine(e.Data);
                    action.Invoke(e.Data);
                });
        }

        /// <summary>
        /// 提取视频的帧，输出为图片
        /// </summary>
        /// <param name="video"></param>
        /// <param name="image"></param>
        /// <param name="number"></param>
        public static void ExtractFrame(string video, string image, uint number)
        {
            // 提取帧
            // ffmpeg -i caiyilin.wmv -vframes 1 wm.bmp
            string param = $"-i \"{video}\" -y -vframes {number} \"{image}\"";
            ExcuteProcess("ffmpeg.exe", param, null, (s, e) => Console.WriteLine(e.Data));
        }


        /// <summary>
        /// 执行一个控制台程序
        /// </summary>
        /// <param name="exe">程序名称</param>
        /// <param name="arg">参数</param>
        /// <param name="workingDirectory">工作路径</param>
        /// <param name="output">输出重定向</param>
        private static void ExcuteProcess(string exe, string arg, string workingDirectory, DataReceivedEventHandler output)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = exe;
                p.StartInfo.Arguments = arg;

                // 工作目录
                if (workingDirectory != null)
                {
                    p.StartInfo.WorkingDirectory = workingDirectory;
                }

                p.StartInfo.UseShellExecute = false;    //输出信息重定向
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;

                // 将 StandardErrorEncoding 改为 UTF-8 才不会出现中文乱码
                p.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
                p.StartInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;

                p.OutputDataReceived += output;
                p.ErrorDataReceived += output;

                p.Start();                    //启动线程
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.WaitForExit();            //等待进程结束
            }
        }

    }
}
