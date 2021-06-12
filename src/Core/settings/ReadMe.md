# 设置

Settings类位于`Core.settings`命名空间中，采用单例模式调用。设置属性使用json格式保存，并将加密后的字符串保存到文件。

| 文件名              | 内容                                   |
| ------------------- | -------------------------------------- |
| Settings.cs         | Settings类的基本属性，单例模式的代码等 |
| Settings.class.cs   | SettingsEntity类等                     |
| Settings.base.cs    | `基本`设置的代码                       |
| Settings.network.cs | `网络`设置的代码                       |
| Settings.video.cs   | `视频`设置的代码                       |
| Settings.about.cs   | `关于`设置的代码                       |

## 基本

获取/设置下载完成后的操作。

```c#
public AfterDownloadOperation GetAfterDownloadOperation();
public bool SetAfterDownloadOperation(AfterDownloadOperation afterDownload);
```

## 网络

获取/设置是否解除地区限制。

```c#
public LiftingOfRegion IsLiftingOfRegion();
public bool IsLiftingOfRegion(LiftingOfRegion isLiftingOfRegion);
```

获取/设置Aria服务器的端口号。

```c#
public int GetAriaListenPort();
public bool SetAriaListenPort(int ariaListenPort);
```

获取/设置Aria日志等级。

```c#
public AriaConfigLogLevel GetAriaLogLevel();
public bool SetAriaLogLevel(AriaConfigLogLevel ariaLogLevel);
```

获取/设置Aria最大同时下载数(任务数)。

```c#
public int GetAriaMaxConcurrentDownloads();
public bool SetAriaMaxConcurrentDownloads(int ariaMaxConcurrentDownloads);
```

获取/设置Aria单文件最大线程数。

```c#
public int GetAriaSplit();
public bool SetAriaSplit(int ariaSplit);
```

获取/设置Aria下载速度限制。

```c#
public int GetAriaMaxOverallDownloadLimit();
public bool SetAriaMaxOverallDownloadLimit(int ariaMaxOverallDownloadLimit);
```

获取/设置Aria下载单文件速度限制。

```c#
public int GetAriaMaxDownloadLimit();
public bool SetAriaMaxDownloadLimit(int ariaMaxDownloadLimit);
```

获取/设置Aria文件预分配。

```c#
public AriaConfigFileAllocation GetAriaFileAllocation();
public bool SetAriaFileAllocation(AriaConfigFileAllocation ariaFileAllocation);
```

获取/设置是否开启Aria http代理。

```c#
public AriaHttpProxy IsAriaHttpProxy();
public bool IsAriaHttpProxy(AriaHttpProxy isAriaHttpProxy);
```

获取/设置Aria的http代理的地址。

```c#
public string GetAriaHttpProxy();
public bool SetAriaHttpProxy(string ariaHttpProxy);
```

获取/设置Aria的http代理的端口。

```c#
public int GetAriaHttpProxyListenPort();
public bool SetAriaHttpProxyListenPort(int ariaHttpProxyListenPort);
```

## 视频

获取/设置优先下载的视频编码。

```c#
public VideoCodecs GetVideoCodecs();
public bool SetVideoCodecs(VideoCodecs videoCodecs);
```

获取/设置优先下载画质。

```c#
public int GetQuality();
public bool SetQuality(int quality);
```

获取/设置是否给视频增加序号。

```c#
public AddOrder IsAddOrder();
public bool IsAddOrder(AddOrder isAddOrder);
```

获取/设置是否下载flv视频后转码为mp4。

```c#
public TranscodingFlvToMp4 IsTranscodingFlvToMp4();
public bool IsTranscodingFlvToMp4(TranscodingFlvToMp4 isTranscodingFlvToMp4);
```

获取/设置下载目录。

```c#
public string GetSaveVideoRootPath();
public bool SetSaveVideoRootPath(string path);
```

获取/设置是否使用默认下载目录。

```c#
public UseSaveVideoRootPath IsUseSaveVideoRootPath();
public bool IsUseSaveVideoRootPath(UseSaveVideoRootPath isUseSaveVideoRootPath);
```

获取/设置是否为不同视频分别创建文件夹。

```c#
public CreateFolderForMedia IsCreateFolderForMedia();
public bool IsCreateFolderForMedia(CreateFolderForMedia isCreateFolderForMedia);
```

## 关于

获取/设置是否接收测试版更新。

```c#
public ReceiveBetaVersion IsReceiveBetaVersion();
public bool IsReceiveBetaVersion(ReceiveBetaVersion isReceiveBetaVersion);
```

获取/设置是否允许启动时检查更新。

```c#
public AutoUpdateWhenLaunch GetAutoUpdateWhenLaunch();
public bool SetAutoUpdateWhenLaunch(AutoUpdateWhenLaunch autoUpdateWhenLaunch);
```
