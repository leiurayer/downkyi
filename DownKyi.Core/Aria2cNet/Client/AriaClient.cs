using DownKyi.Core.Aria2cNet.Client.Entity;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.Core.Aria2cNet.Client
{
    /// <summary>
    /// http://aria2.github.io/manual/en/html/aria2c.html#methods
    /// </summary>
    public static class AriaClient
    {
        private static readonly string JSONRPC = "2.0";
        private static readonly string TOKEN = "downkyi";

        /// <summary>
        /// This method adds a new download.
        /// uris is an array of HTTP/FTP/SFTP/BitTorrent URIs (strings) pointing to the same resource.
        /// If you mix URIs pointing to different resources,
        /// then the download may fail or be corrupted without aria2 complaining.
        /// When adding BitTorrent Magnet URIs,
        /// uris must have only one element and it should be BitTorrent Magnet URI.
        /// options is a struct and its members are pairs of option name and value.
        /// See Options below for more details.
        /// If position is given, it must be an integer starting from 0.
        /// The new download will be inserted at position in the waiting queue.
        /// If position is omitted or position is larger than the current size of the queue,
        /// the new download is appended to the end of the queue.
        /// This method returns the GID of the newly registered download.
        /// </summary>
        /// <param name="uris"></param>
        /// <param name="dir"></param>
        /// <param name="outFile"></param>
        /// <returns></returns>
        public static async Task<AriaAddUri> AddUriAsync(List<string> uris, AriaSendOption option, int position = -1)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                uris,
                option
            };
            if (position > -1)
            {
                ariaParams.Add(position);
            }

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.addUri",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaAddUri>(ariaSend);
        }

        /// <summary>
        /// This method adds a BitTorrent download by uploading a ".torrent" file.
        /// If you want to add a BitTorrent Magnet URI, use the aria2.addUri() method instead.
        /// torrent must be a base64-encoded string containing the contents of the ".torrent" file.
        /// uris is an array of URIs (string).
        /// uris is used for Web-seeding.
        /// For single file torrents, the URI can be a complete URI pointing to the resource;
        /// if URI ends with /, name in torrent file is added.
        /// For multi-file torrents, name and path in torrent are added to form a URI for each file.
        /// options is a struct and its members are pairs of option name and value.
        /// See Options below for more details.
        /// If position is given, it must be an integer starting from 0.
        /// The new download will be inserted at position in the waiting queue.
        /// If position is omitted or position is larger than the current size of the queue,
        /// the new download is appended to the end of the queue.
        /// This method returns the GID of the newly registered download.
        /// If --rpc-save-upload-metadata is true,
        /// the uploaded data is saved as a file named as the hex string of SHA-1 hash of data plus ".torrent" in the directory specified by --dir option.
        /// E.g. a file name might be 0a3893293e27ac0490424c06de4d09242215f0a6.torrent.
        /// If a file with the same name already exists, it is overwritten!
        /// If the file cannot be saved successfully or --rpc-save-upload-metadata is false,
        /// the downloads added by this method are not saved by --save-session.
        /// </summary>
        /// <param name="torrent"></param>
        /// <param name="uris"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<AriaAddTorrent> AddTorrentAsync(string torrent, List<string> uris, AriaSendOption option, int position = -1)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                torrent,
                uris,
                option
            };
            if (position > -1)
            {
                ariaParams.Add(position);
            }

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.addTorrent",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaAddTorrent>(ariaSend);
        }

        /// <summary>
        /// This method adds a Metalink download by uploading a ".metalink" file.
        /// metalink is a base64-encoded string which contains the contents of the ".metalink" file.
        /// options is a struct and its members are pairs of option name and value.
        /// See Options below for more details.
        /// If position is given, it must be an integer starting from 0.
        /// The new download will be inserted at position in the waiting queue.
        /// If position is omitted or position is larger than the current size of the queue,
        /// the new download is appended to the end of the queue.
        /// This method returns an array of GIDs of newly registered downloads.
        /// If --rpc-save-upload-metadata is true,
        /// the uploaded data is saved as a file named hex string of SHA-1 hash of data plus ".metalink" in the directory specified by --dir option.
        /// E.g. a file name might be 0a3893293e27ac0490424c06de4d09242215f0a6.metalink.
        /// If a file with the same name already exists, it is overwritten!
        /// If the file cannot be saved successfully or --rpc-save-upload-metadata is false,
        /// the downloads added by this method are not saved by --save-session.
        /// </summary>
        /// <param name="metalink"></param>
        /// <param name="uris"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<AriaAddMetalink> AddMetalinkAsync(string metalink, List<string> uris, AriaSendOption option, int position = -1)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                metalink,
                uris,
                option
            };
            if (position > -1)
            {
                ariaParams.Add(position);
            }

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.addMetalink",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaAddMetalink>(ariaSend);
        }

        /// <summary>
        /// This method removes the download denoted by gid (string).
        /// If the specified download is in progress, it is first stopped.
        /// The status of the removed download becomes removed.
        /// This method returns GID of removed download.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaRemove> RemoveAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.remove",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaRemove>(ariaSend);
        }

        /// <summary>
        /// This method removes the download denoted by gid.
        /// This method behaves just like aria2.remove()
        /// except that this method removes the download without performing any actions which take time,
        /// such as contacting BitTorrent trackers to unregister the download first.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaRemove> ForceRemoveAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.forceRemove",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaRemove>(ariaSend);
        }

        /// <summary>
        /// This method pauses the download denoted by gid (string).
        /// The status of paused download becomes paused.
        /// If the download was active, the download is placed in the front of waiting queue.
        /// While the status is paused, the download is not started.
        /// To change status to waiting, use the aria2.unpause() method.
        /// This method returns GID of paused download.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaPause> PauseAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.pause",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaPause>(ariaSend);
        }

        /// <summary>
        /// This method is equal to calling aria2.pause() for every active/waiting download.
        /// This methods returns OK.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaPause> PauseAllAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.pauseAll",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaPause>(ariaSend);
        }

        /// <summary>
        /// This method pauses the download denoted by gid.
        /// This method behaves just like aria2.pause()
        /// except that this method pauses downloads without performing any actions which take time,
        /// such as contacting BitTorrent trackers to unregister the download first.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaPause> ForcePauseAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.forcePause",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaPause>(ariaSend);
        }

        /// <summary>
        /// This method is equal to calling aria2.forcePause() for every active/waiting download.
        /// This methods returns OK.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaPause> ForcePauseAllAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.forcePauseAll",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaPause>(ariaSend);
        }

        /// <summary>
        /// This method changes the status of the download denoted by gid (string) from paused to waiting,
        /// making the download eligible to be restarted.
        /// This method returns the GID of the unpaused download.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaPause> UnpauseAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.unpause",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaPause>(ariaSend);
        }

        /// <summary>
        /// This method is equal to calling aria2.unpause() for every paused download.
        /// This methods returns OK.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaPause> UnpauseAllAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.unpauseAll",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaPause>(ariaSend);
        }

        /// <summary>
        /// This method returns the progress of the download denoted by gid (string).
        /// keys is an array of strings.
        /// If specified, the response contains only keys in the keys array.
        /// If keys is empty or omitted, the response contains all keys.
        /// This is useful when you just want specific keys and avoid unnecessary transfers.
        /// For example, aria2.tellStatus("2089b05ecca3d829", ["gid", "status"]) returns the gid and status keys only.
        /// The response is a struct and contains following keys. Values are strings.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaTellStatus> TellStatus(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.tellStatus",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaTellStatus>(ariaSend);
        }

        /// <summary>
        /// This method returns the URIs used in the download denoted by gid (string).
        /// The response is an array of structs and it contains following keys.
        /// Values are string.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaGetUris> GetUrisAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getUris",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetUris>(ariaSend);
        }

        /// <summary>
        /// This method returns the file list of the download denoted by gid (string).
        /// The response is an array of structs which contain following keys.
        /// Values are strings.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaGetFiles> GetFilesAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getFiles",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetFiles>(ariaSend);
        }

        /// <summary>
        /// This method returns a list peers of the download denoted by gid (string).
        /// This method is for BitTorrent only.
        /// The response is an array of structs and contains the following keys.
        /// Values are strings.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaGetPeers> GetPeersAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getPeers",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetPeers>(ariaSend);
        }

        /// <summary>
        /// This method returns currently connected HTTP(S)/FTP/SFTP servers of the download denoted by gid (string).
        /// The response is an array of structs and contains the following keys.
        /// Values are strings.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaGetServers> GetServersAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getServers",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetServers>(ariaSend);
        }

        /// <summary>
        /// This method returns a list of active downloads.
        /// The response is an array of the same structs as returned by the aria2.tellStatus() method.
        /// For the keys parameter, please refer to the aria2.tellStatus() method.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaTellStatusList> TellActiveAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.tellActive",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaTellStatusList>(ariaSend);
        }

        /// <summary>
        /// This method returns a list of waiting downloads, including paused ones.
        /// offset is an integer and specifies the offset from the download waiting at the front.
        /// num is an integer and specifies the max.
        /// number of downloads to be returned.
        /// For the keys parameter, please refer to the aria2.tellStatus() method.
        /// <br/><br/>
        /// If offset is a positive integer,
        /// this method returns downloads in the range of [offset, offset + num).
        /// <br/><br/>
        /// offset can be a negative integer.
        /// offset == -1 points last download in the waiting queue and offset == -2 points the download before the last download, and so on.
        /// Downloads in the response are in reversed order then.
        /// <br/><br/>
        /// For example, imagine three downloads "A","B" and "C" are waiting in this order.
        /// aria2.tellWaiting(0, 1) returns ["A"].
        /// aria2.tellWaiting(1, 2) returns ["B", "C"].
        /// aria2.tellWaiting(-1, 2) returns ["C", "B"].
        /// <br/><br/>
        /// The response is an array of the same structs as returned by aria2.tellStatus() method.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static async Task<AriaTellStatusList> TellWaitingAsync(int offset, int num)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                offset,
                num
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.tellWaiting",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaTellStatusList>(ariaSend);
        }

        /// <summary>
        /// This method returns a list of stopped downloads.
        /// offset is an integer and specifies the offset from the least recently stopped download.
        /// num is an integer and specifies the max.
        /// number of downloads to be returned.
        /// For the keys parameter, please refer to the aria2.tellStatus() method.
        /// <br/><br/>
        /// offset and num have the same semantics as described in the aria2.tellWaiting() method.
        /// <br/><br/>
        /// The response is an array of the same structs as returned by the aria2.tellStatus() method.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static async Task<AriaTellStatusList> TellStoppedAsync(int offset, int num)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                offset,
                num
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.tellStopped",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaTellStatusList>(ariaSend);
        }

        /// <summary>
        /// This method changes the position of the download denoted by gid in the queue.
        /// pos is an integer.
        /// how is a string.
        /// If how is POS_SET, it moves the download to a position relative to the beginning of the queue.
        /// If how is POS_CUR, it moves the download to a position relative to the current position.
        /// If how is POS_END, it moves the download to a position relative to the end of the queue.
        /// If the destination position is less than 0 or beyond the end of the queue,
        /// it moves the download to the beginning or the end of the queue respectively.
        /// The response is an integer denoting the resulting position.
        /// 
        /// For example, if GID#2089b05ecca3d829 is currently in position 3,
        /// aria2.changePosition('2089b05ecca3d829', -1, 'POS_CUR') will change its position to 2.
        /// Additionally aria2.changePosition('2089b05ecca3d829', 0, 'POS_SET') will change its position to 0 (the beginning of the queue).
        /// 
        /// The following examples move the download GID#2089b05ecca3d829 to the front of the queue.
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="pos"></param>
        /// <param name="how"></param>
        /// <returns></returns>
        public static async Task<AriaChangePosition> ChangePositionAsync(string gid, int pos, HowChangePosition how)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid,
                pos,
                how.ToString("G")
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.changePosition",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaChangePosition>(ariaSend);
        }

        /// <summary>
        /// This method removes the URIs in delUris from and appends the URIs in addUris to download denoted by gid.
        /// delUris and addUris are lists of strings.
        /// A download can contain multiple files and URIs are attached to each file.
        /// fileIndex is used to select which file to remove/attach given URIs.
        /// fileIndex is 1-based.
        /// position is used to specify where URIs are inserted in the existing waiting URI list.
        /// position is 0-based.
        /// When position is omitted, URIs are appended to the back of the list.
        /// This method first executes the removal and then the addition.
        /// position is the position after URIs are removed, not the position when this method is called.
        /// When removing an URI, if the same URIs exist in download,
        /// only one of them is removed for each URI in delUris.
        /// In other words,
        /// if there are three URIs http://example.org/aria2 and you want remove them all,
        /// you have to specify (at least) 3 http://example.org/aria2 in delUris.
        /// This method returns a list which contains two integers.
        /// The first integer is the number of URIs deleted. The second integer is the number of URIs added.
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="fileIndex"></param>
        /// <param name="delUris"></param>
        /// <param name="addUris"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static async Task<AriaChangeUri> ChangeUriAsync(string gid, int fileIndex, List<string> delUris, List<string> addUris, int position = -1)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid,
                fileIndex,
                delUris,
                addUris
            };
            if (position > -1)
            {
                ariaParams.Add(position);
            }

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.changePosition",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaChangeUri>(ariaSend);
        }

        /// <summary>
        /// This method returns options of the download denoted by gid.
        /// The response is a struct where keys are the names of options.
        /// The values are strings.
        /// Note that this method does not return options which have no default value and have not been set on the command-line,
        /// in configuration files or RPC methods.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaGetOption> GetOptionAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getOption",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetOption>(ariaSend);
        }

        /// <summary>
        /// This method changes options of the download denoted by gid (string) dynamically.
        /// options is a struct.
        /// The options listed in Input File subsection are available, except for following options:
        /// <br/>
        /// dry-run metalink-base-uri parameterized-uri pause piece-length rpc-save-upload-metadata
        /// <br/>
        /// Except for the following options,
        /// changing the other options of active download makes it restart
        /// (restart itself is managed by aria2, and no user intervention is required):
        /// <br/>
        /// bt-max-peers bt-request-peer-speed-limit bt-remove-unselected-file force-save max-download-limit max-upload-limit
        /// <br/>
        /// This method returns OK for success.
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<AriaChangeOption> ChangeOptionAsync(string gid, object option)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid,
                option
            };

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.changeOption",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaChangeOption>(ariaSend);
        }

        /// <summary>
        /// This method returns the global options.
        /// The response is a struct.
        /// Its keys are the names of options.
        /// Values are strings.
        /// Note that this method does not return options which have no default value and have not been set on the command-line,
        /// in configuration files or RPC methods.
        /// Because global options are used as a template for the options of newly added downloads,
        /// the response contains keys returned by the aria2.getOption() method.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaGetOption> GetGlobalOptionAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getGlobalOption",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetOption>(ariaSend);
        }

        /// <summary>
        /// This method changes global options dynamically.
        /// options is a struct.
        /// The following options are available:
        /// <br/>
        /// bt-max-open-files download-result keep-unfinished-download-result log log-level
        /// max-concurrent-downloads max-download-result max-overall-download-limit max-overall-upload-limit
        /// optimize-concurrent-downloads save-cookies save-session server-stat-of
        /// <br/>
        /// In addition, options listed in the Input File subsection are available,
        /// except for following options: checksum, index-out, out, pause and select-file.
        /// With the log option, you can dynamically start logging or change log file.
        /// To stop logging, specify an empty string("") as the parameter value.
        /// Note that log file is always opened in append mode.
        /// This method returns OK for success.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public static async Task<AriaChangeOption> ChangeGlobalOptionAsync(object option)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                option
            };

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.changeGlobalOption",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaChangeOption>(ariaSend);
        }

        /// <summary>
        /// This method returns global statistics such as the overall download and upload speeds.
        /// The response is a struct and contains the following keys. Values are strings.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaGetGlobalStat> GetGlobalStatAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getGlobalStat",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetGlobalStat>(ariaSend);
        }

        /// <summary>
        /// This method purges completed/error/removed downloads to free memory.
        /// This method returns OK.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaRemove> PurgeDownloadResultAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.purgeDownloadResult",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaRemove>(ariaSend);
        }

        /// <summary>
        /// This method removes a completed/error/removed download denoted by gid from memory.
        /// This method returns OK for success.
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<AriaRemove> RemoveDownloadResultAsync(string gid)
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                gid
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.removeDownloadResult",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaRemove>(ariaSend);
        }

        /// <summary>
        /// This method returns the version of aria2 and the list of enabled features.
        /// The response is a struct and contains following keys.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaVersion> GetAriaVersionAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getVersion",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaVersion>(ariaSend);
        }

        /// <summary>
        /// This method returns session information.
        /// The response is a struct and contains following key.
        /// <br/><br/>
        /// Session ID, which is generated each time when aria2 is invoked.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaGetSessionInfo> GetSessionInfoAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.getSessionInfo",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaGetSessionInfo>(ariaSend);
        }

        /// <summary>
        /// This method shuts down aria2.
        /// This method returns OK.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaShutdown> ShutdownAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.shutdown",
                Params = ariaParams
            };
            var re = await GetRpcResponseAsync<AriaShutdown>(ariaSend);
            return re;
        }

        /// <summary>
        /// This method shuts down aria2().
        /// This method behaves like :func:'aria2.shutdown` without performing any actions which take time,
        /// such as contacting BitTorrent trackers to unregister downloads first.
        /// This method returns OK.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaShutdown> ForceShutdownAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.forceShutdown",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaShutdown>(ariaSend);
        }

        /// <summary>
        /// This method saves the current session to a file specified by the --save-session option.
        /// This method returns OK if it succeeds.
        /// </summary>
        /// <returns></returns>
        public static async Task<AriaSaveSession> SaveSessionAsync()
        {
            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.saveSession",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<AriaSaveSession>(ariaSend);
        }

        /// <summary>
        /// This methods encapsulates multiple method calls in a single request.
        /// methods is an array of structs. The structs contain two keys: methodName and params.
        /// methodName is the method name to call and params is array containing parameters to the method call.
        /// This method returns an array of responses.
        /// The elements will be either a one-item array containing the return value of the method call or a struct of fault element if an encapsulated method call fails.
        /// </summary>
        /// <param name="systemMulticallMathods"></param>
        /// <returns></returns>
        public static async Task<List<SystemMulticall>> MulticallAsync(List<SystemMulticallMathod> systemMulticallMathods)
        {
            List<object> ariaParams = new List<object>
            {
                systemMulticallMathods
            };
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "system.multicall",
                Params = ariaParams
            };
            return await GetRpcResponseAsync<List<SystemMulticall>>(ariaSend);
        }

        /// <summary>
        /// This method returns all the available RPC methods in an array of string.
        /// Unlike other methods, this method does not require secret token.
        /// This is safe because this method just returns the available method names.
        /// </summary>
        /// <returns></returns>
        public static async Task<SystemListMethods> ListMethodsAsync()
        {
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "system.listMethods"
            };
            return await GetRpcResponseAsync<SystemListMethods>(ariaSend);
        }

        /// <summary>
        /// This method returns all the available RPC notifications in an array of string.
        /// Unlike other methods, this method does not require secret token.
        /// This is safe because this method just returns the available notifications names.
        /// </summary>
        /// <returns></returns>
        public static async Task<SystemListNotifications> ListNotificationsAsync()
        {
            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "system.listNotifications"
            };
            return await GetRpcResponseAsync<SystemListNotifications>(ariaSend);
        }

        /// <summary>
        /// 获取jsonrpc的地址
        /// </summary>
        /// <returns></returns>
        private static string GetRpcUri(int listenPort = 6800)
        {
            return $"http://localhost:{listenPort}/jsonrpc";
        }

        /// <summary>
        /// 发送http请求，并将返回的json反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ariaSend"></param>
        /// <returns></returns>
        private async static Task<T> GetRpcResponseAsync<T>(AriaSendData ariaSend)
        {
            // 去掉null
            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            // 转换为json字符串
            string sendJson = JsonConvert.SerializeObject(ariaSend, Formatting.Indented, jsonSetting);
            // 向服务器请求数据
            string result = string.Empty;
            await Task.Run(() =>
            {
                result = Request(GetRpcUri(), sendJson);
            });
            if (result == null) { return default; }

            // 反序列化
            var aria = JsonConvert.DeserializeObject<T>(result);
            return aria;
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        private static string Request(string url, string parameters, int retry = 3)
        {
            // 重试次数
            if (retry <= 0) { return null; }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Timeout = 5 * 1000;
                request.ContentType = "application/json";
                byte[] postData = Encoding.UTF8.GetBytes(parameters);
                request.ContentLength = postData.Length;
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(postData, 0, postData.Length);
                    reqStream.Close();
                }

                string html = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                }
                return html;
            }
            catch (WebException e)
            {
                Utils.Debugging.Console.PrintLine("Request()发生Web异常: {0}", e);
                LogManager.Error("AriaClient", e);
                //return Request(url, parameters, retry - 1);

                string html = string.Empty;
                var response = (HttpWebResponse)e.Response;
                if (response == null) { return null; }
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        html = reader.ReadToEnd();
                    }
                }

                Console.WriteLine($"本次请求使用的参数：{parameters}");
                Console.WriteLine($"返回的web数据：{html}");
                return html;
            }
            catch (IOException e)
            {
                Utils.Debugging.Console.PrintLine("Request()发生IO异常: {0}", e);
                LogManager.Error("AriaClient", e);
                return Request(url, parameters, retry - 1);
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("Request()发生其他异常: {0}", e);
                LogManager.Error("AriaClient", e);
                return Request(url, parameters, retry - 1);
            }
        }

    }
}
