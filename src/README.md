# 哔哩下载姬

哔哩下载姬（DownKyi）是一个简单易用的哔哩哔哩视频下载工具，具有简洁的界面，流畅的操作逻辑。哔哩下载姬可以下载几乎所有的B站视频，并输出mp4格式的文件；采用Aria下载器多线程下载，采用FFmpeg对视频进行混流、提取音视频等操作。

- [x] 支持二维码登录

- [x] 支持视频、番剧、剧集、电影、课程下载

- [x] **支持8K、4K、HDR、杜比视界**

- [x] **支持AVC、HEVC、AV1视频编码**

- [x] **支持杜比全景声、Hi-Res无损音质**

- [x] **支持用户收藏夹、订阅、稍后再看、历史记录下载**

- [x] 支持弹幕下载、样式设置

- [x] 支持字幕下载

- [x] 支持封面下载

- [x] 支持自定义文件命名

- [x] 支持断点续传

- [x] 内置Aria2c服务器，支持自定义Aria2c服务器

- [x] 支持下载历史记录保存

- [x] 支持av、BV互转

- [x] 支持弹幕发送者查询

- [x] 支持音视频分离

- [x] 支持去水印

- [x] 支持检查更新

  ……

## 使用方法

### 环境

要求 .NET Framework >= 4.7.2

- 安装 [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472)

- [.NET Framework 4.7.2 在线安装程序](https://download.microsoft.com/download/0/5/C/05C1EC0E-D5EE-463B-BFE3-9311376A6809/NDP472-KB4054531-Web.exe)

- [.NET Framework 4.7.2 离线安装程序](https://download.microsoft.com/download/6/E/4/6E48E8AB-DC00-419E-9704-06DD46E5F81D/NDP472-KB4054530-x86-x64-AllOS-ENU.exe)

- 也可以安装 [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

### 检索

哔哩下载姬支持多种复制于浏览器或APP的网址格式，在程序主页输入并按回车键即可开始检索。

- 检索只获取视频的基本信息，视频下载链接需点击解析。
- 监听剪贴板，复制即可开始检索。
- 视频详情页中，先选中视频再下载，如果该视频已经在下载队列或者已下载列表中，则不会被添加。
- 用户收藏夹、订阅、稍后再看、历史记录中，点击下载后，会默认下载选中视频的所有分P。

目前已支持的有：

- [x] av号：av170001，<https://www.bilibili.com/video/av170001>
- [x] BV号：BV17x411w7KC，<https://www.bilibili.com/video/BV17x411w7KC>, <https://b23.tv/BV17x411w7KC>
- [x] 番剧（电影、电视剧）ss号：<https://www.bilibili.com/bangumi/play/ss32982>
- [x] 番剧（电影、电视剧）ep号：<https://www.bilibili.com/bangumi/play/ep317925>
- [x] 番剧（电影、电视剧）md号：<https://www.bilibili.com/bangumi/media/md28228367>
- [x] 课程ss号：<https://www.bilibili.com/cheese/play/ss205>
- [x] 课程ep号：<https://www.bilibili.com/cheese/play/ep3489>
- [x] 收藏夹：ml1329019876，ML1329019876，<https://www.bilibili.com/medialist/detail/ml1329019876>, <https://www.bilibili.com/medialist/play/ml1329019876/>
- [x] 用户空间：uid928123，UID:928123，<https://space.bilibili.com/928123>

`注：因为番剧和课程都有ss号和ep号，因此暂时不能直接输入ss号和ep号。`

### 设置

#### 文件命名格式

单击`可选字段`向文件名中添加命名模块，在`文件名`中右键单击删除选中的命名模块，支持拖拽排序。
