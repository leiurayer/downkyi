namespace Brotli
{
    public enum BrotliEncoderParameter : int
    {
        Mode = 0,
        /// <summary>
        ///  Controls the compression-speed vs compression-density tradeoffs. The higher
        ///  the quality, the slower the compression. Range is 0 to 11.
        /// </summary>
        Quality = 1,
        /// <summary>
        /// Base 2 logarithm of the sliding window size. Range is 10 to 24. 
        /// </summary>
        LGWin = 2,

        /// <summary>
        /// Base 2 logarithm of the maximum input block size. Range is 16 to 24.
        /// If set to 0, the value will be set based on the quality.
        /// </summary>
        LGBlock = 3
    };
}
