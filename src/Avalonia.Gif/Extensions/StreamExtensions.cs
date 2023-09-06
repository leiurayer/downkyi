using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Avalonia.Gif.Extensions
{
    [DebuggerStepThrough]
    internal static class StreamExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort SpanToShort(Span<byte> b) => (ushort)(b[0] | (b[1] << 8));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Skip(this Stream stream, long count)
        {
            stream.Position += count;
        }

        /// <summary>
        /// Read a Gif block from stream while advancing the position.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadBlock(this Stream stream, byte[] tempBuf)
        {
            stream.Read(tempBuf, 0, 1);

            var blockLength = (int)tempBuf[0];

            if (blockLength > 0)
                stream.Read(tempBuf, 0, blockLength);

            // Guard against infinite loop.
            if (stream.Position >= stream.Length)
                throw new InvalidGifStreamException("Reach the end of the filestream without trailer block.");

            return blockLength;
        }

        /// <summary>
        /// Skips GIF blocks until it encounters an empty block.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SkipBlocks(this Stream stream, byte[] tempBuf)
        {
            int blockLength;
            do
            {
                stream.Read(tempBuf, 0, 1);

                blockLength = tempBuf[0];
                stream.Position += blockLength;

                // Guard against infinite loop.
                if (stream.Position >= stream.Length)
                    throw new InvalidGifStreamException("Reach the end of the filestream without trailer block.");

            } while (blockLength > 0);
        }

        /// <summary>
        /// Read a <see cref="ushort"/> from stream by providing a temporary buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUShortS(this Stream stream, byte[] tempBuf)
        {
            stream.Read(tempBuf, 0, 2);
            return SpanToShort(tempBuf);
        }

        /// <summary>
        /// Read a <see cref="ushort"/> from stream by providing a temporary buffer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReadByteS(this Stream stream, byte[] tempBuf)
        {
            stream.Read(tempBuf, 0, 1);
            var finalVal = tempBuf[0];
            return finalVal;
        }
    }
}