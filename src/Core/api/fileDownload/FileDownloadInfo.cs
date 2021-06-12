using System;
using System.IO;

namespace Core.api.fileDownload
{
    public class FileDownloadInfo
    {


        // 下载地址 
        public string DownLoadUrl { get; set; }

        private string _localSaveFolder;
        // 本地保存路径
        public string LocalSaveFolder
        {
            get { return _localSaveFolder; }
            set
            { _localSaveFolder = value; }
        }

        // 包含文件名的完整保存路径
        private string _savePath;
        public string SavePath
        {
            get
            {
                if (_savePath == null)
                {
                    if (_localSaveFolder == null)
                    {
                        throw new Exception("本地保存路径不能为空");
                    }

                    _savePath = Path.Combine(_localSaveFolder, Path.GetFileName(DownLoadUrl));

                    if (File.Exists(_savePath))
                    {
                        if (IsNew)
                        {
                            if (IsOver)
                            {
                                File.Delete(_savePath);
                            }
                            else
                            {
                                _savePath = _savePath.Substring(0, _savePath.LastIndexOf(".")) + "(2)" + _savePath.Substring(_savePath.LastIndexOf("."));
                            }
                        }
                    }
                }

                return _savePath;
            }
            set
            {
                _savePath = value;
            }
        }

        private int _threadCount = 1;
        ////// 线程数
        public int ThreadCount
        {
            get { return _threadCount; }
            set { _threadCount = value; }
        }

        ////// 是否覆盖已存在的文件
        public bool IsOver { get; set; }

        ////// 是否重新下载
        public bool IsNew { get; set; }


    }
}
