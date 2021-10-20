using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brotli
{
    public static class BrotliExtensions
    {
        /// <summary>
        /// Compress the data with brotli
        /// </summary>
        /// <param name="rawData">inputData</param>
        /// <param name="quality">quality,0~11</param>
        /// <param name="window">compress window(10~24)</param>
        /// <returns>compressed bytes</returns>
        public static byte[] CompressToBrotli(this byte[] rawData, uint quality = 5, uint window = 22)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            using (var msInput = new System.IO.MemoryStream(rawData))
            {
                return CompressToBrotli(msInput, quality, window);
            }
        }

        /// <summary>
        /// Compress the data with brotli
        /// </summary>
        /// <param name="inStream">input stream</param>
        /// <param name="quality">quality,0~11</param>
        /// <param name="window">compress window(10~24)</param>
        /// <returns>compressed bytes</returns>
        public static byte[] CompressToBrotli(this System.IO.Stream inStream, uint quality = 5, uint window = 22)
        {
            using (System.IO.MemoryStream msOutput = new System.IO.MemoryStream())
            {
                CompressToBrotli(inStream, msOutput, quality, window);
                var output = msOutput.ToArray();
                return output;
            }
        }

        /// <summary>
        /// Compress the data with brotli
        /// </summary>
        /// <param name="inStream">input stream</param>
        /// <param name="destStream">dest output stream</param>
        /// <param name="quality">quality,0~11</param>
        /// <param name="window">compress window(10~24)</param>
        public static void CompressToBrotli(this System.IO.Stream inStream, System.IO.Stream destStream, uint quality = 5, uint window = 22)
        {
            using (BrotliStream bs = new BrotliStream(destStream, System.IO.Compression.CompressionMode.Compress))
            {
                bs.SetQuality(quality);
                bs.SetWindow(window);
                inStream.CopyTo(bs);
                bs.Close();
            }
        }

        public static byte[] DecompressFromBrotli(this byte[] rawData)
        {
            if (rawData == null) throw new ArgumentNullException(nameof(rawData));
            using (var msInput = new System.IO.MemoryStream(rawData))
            {
                return DecompressFromBrotli(msInput);
            }
        }

        public static byte[] DecompressFromBrotli(this System.IO.Stream inStream)
        {
            using (System.IO.MemoryStream msOutput = new System.IO.MemoryStream())
            {
                DecompressFromBrotli(inStream, msOutput);
                var output = msOutput.ToArray();
                return output;
            }
        }

        public static void DecompressFromBrotli(this System.IO.Stream inStream,System.IO.Stream destStream)
        {
            using (BrotliStream bs = new BrotliStream(inStream, System.IO.Compression.CompressionMode.Decompress))
            {
                bs.CopyTo(destStream);
                destStream.Flush();
            }
        }
    }
}