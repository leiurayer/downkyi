namespace Avalonia.Gif.Decoding
{
    internal enum BlockTypes
    {
        Empty = 0,
        Extension = 0x21,
        ImageDescriptor = 0x2C,
        Trailer = 0x3B,
    }
}