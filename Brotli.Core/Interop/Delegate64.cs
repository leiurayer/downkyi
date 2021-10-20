using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Brotli
{
    internal class Delegate64
    {
        #region Encoder
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr BrotliEncoderCreateInstanceDelegate(IntPtr allocFunc, IntPtr freeFunc, IntPtr opaque);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool BrotliEncoderSetParameterDelegate(IntPtr state, BrotliEncoderParameter parameter, UInt32 value);

        //delegate void BrotliEncoderSetCustomDictionary(IntPtr state, UInt32 size, IntPtr dict);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool BrotliEncoderCompressStreamDelegate(
            IntPtr state, BrotliEncoderOperation op, ref UInt64 availableIn,
            ref IntPtr nextIn, ref UInt64 availableOut, ref IntPtr nextOut, out UInt64 totalOut);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool BrotliEncoderIsFinishedDelegate(IntPtr state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void BrotliEncoderDestroyInstanceDelegate(IntPtr state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate UInt32 BrotliEncoderVersionDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr BrotliEncoderTakeOutputDelegate(IntPtr state, ref UInt64 size);

        #endregion
        #region Decoder
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr BrotliDecoderCreateInstanceDelegate(IntPtr allocFunc, IntPtr freeFunc, IntPtr opaque);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool BrotliDecoderSetParameter(IntPtr state, BrotliDecoderParameter param, UInt32 value);
        //delegate void BrotliDecoderSetCustomDictionary(IntPtr state, UInt64 size, IntPtr dict);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate BrotliDecoderResult BrotliDecoderDecompressStreamDelegate(
            IntPtr state, ref UInt64 availableIn, ref IntPtr nextIn,
            ref UInt64 availableOut, ref IntPtr nextOut, out UInt64 totalOut);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void BrotliDecoderDestroyInstanceDelegate(IntPtr state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate UInt32 BrotliDecoderVersionDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool BrotliDecoderIsUsedDelegate(IntPtr state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate bool BrotliDecoderIsFinishedDelegate(IntPtr state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate Int32 BrotliDecoderGetErrorCodeDelegate(IntPtr state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr BrotliDecoderErrorStringDelegate(Int32 code);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate IntPtr BrotliDecoderTakeOutputDelegate(IntPtr state, ref UInt64 size);

        #endregion
    }
}
