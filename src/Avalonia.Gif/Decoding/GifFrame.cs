using System;

namespace Avalonia.Gif.Decoding
{
    public class GifFrame
    {
        public bool HasTransparency, IsInterlaced, IsLocalColorTableUsed;
        public byte TransparentColorIndex;
        public int LzwMinCodeSize, LocalColorTableSize;
        public long LzwStreamPosition;
        public TimeSpan FrameDelay;
        public FrameDisposal FrameDisposalMethod;
        public bool ShouldBackup;
        public GifRect Dimensions;
        public GifColor[] LocalColorTable;
    }
}