using Avalonia.Animation;
using Avalonia.Gif.Decoding;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Avalonia.Gif
{
    public class GifInstance : IDisposable
    {
        public IterationCount IterationCount { get; set; }
        public bool AutoStart { get; private set; } = true;
        private readonly GifDecoder _gifDecoder;
        private readonly WriteableBitmap? _targetBitmap;
        private TimeSpan _totalTime;
        private readonly List<TimeSpan> _frameTimes;
        private uint _iterationCount;
        private int _currentFrameIndex;
        private readonly List<ulong> _colorTableIdList;

        public CancellationTokenSource CurrentCts { get; }

        internal GifInstance(object newValue) : this(newValue switch
        {
            Stream s => s,
            Uri u => GetStreamFromUri(u),
            string str => GetStreamFromString(str),
            _ => throw new InvalidDataException("Unsupported source object")
        })
        { }

        public GifInstance(string uri) : this(GetStreamFromString(uri))
        { }

        public GifInstance(Uri uri) : this(GetStreamFromUri(uri))
        { }

        public GifInstance(Stream currentStream)
        {
            if (!currentStream.CanSeek)
                throw new InvalidDataException("The provided stream is not seekable.");

            if (!currentStream.CanRead)
                throw new InvalidOperationException("Can't read the stream provided.");

            currentStream.Seek(0, SeekOrigin.Begin);

            CurrentCts = new CancellationTokenSource();

            _gifDecoder = new GifDecoder(currentStream, CurrentCts.Token);
            var pixSize = new PixelSize(_gifDecoder.Header.Dimensions.Width, _gifDecoder.Header.Dimensions.Height);

            _targetBitmap = new WriteableBitmap(pixSize, new Vector(96, 96), PixelFormat.Bgra8888, AlphaFormat.Opaque);
            GifPixelSize = pixSize;

            _totalTime = TimeSpan.Zero;

            _frameTimes = _gifDecoder.Frames.Select(frame =>
            {
                _totalTime = _totalTime.Add(frame.FrameDelay);
                return _totalTime;
            }).ToList();

            _gifDecoder.RenderFrame(0, _targetBitmap);
        }

        private static Stream GetStreamFromString(string str)
        {
            if (!Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out var res))
            {
                throw new InvalidCastException("The string provided can't be converted to URI.");
            }

            return GetStreamFromUri(res);
        }

        private static Stream GetStreamFromUri(Uri uri)
        {
            var uriString = uri.OriginalString.Trim();

            if (!uriString.StartsWith("resm") && !uriString.StartsWith("avares"))
                throw new InvalidDataException(
                    "The URI provided is not currently supported.");

            return AssetLoader.Open(uri);
        }

        public int GifFrameCount => _frameTimes.Count;

        public PixelSize GifPixelSize { get; }

        public void Dispose()
        {
            IsDisposed = true;
            CurrentCts.Cancel();
            _targetBitmap?.Dispose();
        }

        public bool IsDisposed { get; private set; }


        public WriteableBitmap? ProcessFrameTime(TimeSpan stopwatchElapsed)
        {
            if (!IterationCount.IsInfinite && _iterationCount > IterationCount.Value)
            {
                return null;
            }

            if (CurrentCts.IsCancellationRequested || _targetBitmap is null)
            {
                return null;
            }

            var elapsedTicks = stopwatchElapsed.Ticks;
            var timeModulus = TimeSpan.FromTicks(elapsedTicks % _totalTime.Ticks);
            var targetFrame = _frameTimes.FirstOrDefault(x => timeModulus < x);
            var currentFrame = _frameTimes.IndexOf(targetFrame);
            if (currentFrame == -1) currentFrame = 0;

            if (_currentFrameIndex == currentFrame)
                return _targetBitmap;

            _iterationCount = (uint)(elapsedTicks / _totalTime.Ticks);

            return ProcessFrameIndex(currentFrame);
        }

        internal WriteableBitmap ProcessFrameIndex(int frameIndex)
        {
            _gifDecoder.RenderFrame(frameIndex, _targetBitmap);
            _currentFrameIndex = frameIndex;

            return _targetBitmap;
        }
    }
}