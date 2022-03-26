# 哔哩下载姬

![Alipay](https://croire.gitee.io/app/DownKyi/images/app/index.png)

哔哩下载姬（DownKyi）是一个简单易用的哔哩哔哩视频下载工具，具有简洁的界面，流畅的操作逻辑。哔哩下载姬可以下载几乎所有的B站视频，并输出mp4格式的文件；采用Aria下载器多线程下载，采用FFmpeg对视频进行混流、提取音视频等操作。

[更多详情](src/README.md)

## 更新日志

[全部更新日志](CHANGELOG.md)

* `2022/03/26` v1.5.0-alpha7
    1. [优化] 批量下载时过滤UGC、其他季或花絮内容。
    2. [优化] 保存下载视频内容到设置。
    3. [优化] 更新ffmpeg为gpl版本。
    4. [优化] 默认的aria的文件预分配改为NONE。
    5. [新增] “解析后自动下载已解析视频”设置。
    6. [修复] 只下载音频失败的问题。
    7. [修复] Settings被占用无法读取的问题。
    8. [修复] 因路径导致无法下载的问题。
    9. [修复] 无法正确关闭数据库的问题。

## 下载

* [哔哩下载姬最新版](https://github.com/FlySelfLog/downkyi/releases/download/v1.5.0-alpha7/DownKyi-1.5.0-alpha7.zip)

* [下载页面](https://github.com/FlySelfLog/downkyi/releases)

## 赞助

如果这个项目对您有很大帮助，并且您希望支持该项目的开发和维护，请随时扫描一下二维码进行捐赠。非常感谢您的捐款，谢谢！

![Alipay](https://croire.gitee.io/app/DownKyi/images/Alipay.png)![WeChat](https://croire.gitee.io/app/DownKyi/images/WeChat.png)

## 开发

### 相关项目

* [哔哩哔哩-API收集整理](https://github.com/SocialSisterYi/bilibili-API-collect)
* [Prism](https://github.com/PrismLibrary/Prism)

## 免责申明

1. 本软件只提供视频解析，不提供任何资源上传、存储到服务器的功能。
2. 本软件仅解析来自B站的内容，不会对解析到的音视频进行二次编码，部分视频会进行有限的格式转换、拼接等操作。
3. 本软件解析得到的所有内容均来自B站UP主上传、分享，其版权均归原作者所有。内容提供者、上传者（UP主）应对其提供、上传的内容承担全部责任。
4. **本软件提供的所有内容，仅可用作学习交流使用，未经原作者授权，禁止用于其他用途。请在下载24小时内删除。为尊重作者版权，请前往资源的原始发布网站观看，支持原创，谢谢。**
5. 因使用本软件产生的版权问题，软件作者概不负责。
