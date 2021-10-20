using System;
using System.Runtime.InteropServices;

namespace Brotli
{
    class Brolib64
    {
        static Brolib64()
        {
            var path = LibPathBootStrapper.LibPath;
            NativeLibraryLoader nl = new NativeLibraryLoader(path);
            #region set encoder
            nl.FillDelegate(out BrotliEncoderCreateInstance);
            nl.FillDelegate(out BrotliEncoderSetParameter);
            nl.FillDelegate(out BrotliEncoderCompressStream);
            nl.FillDelegate(out BrotliEncoderIsFinished);
            nl.FillDelegate(out BrotliEncoderDestroyInstance);
            nl.FillDelegate(out BrotliEncoderVersion);
            nl.FillDelegate(out BrotliEncoderVersion);
            nl.FillDelegate(out BrotliEncoderTakeOutput);
            #endregion
            #region set decoder
            nl.FillDelegate(out BrotliDecoderCreateInstance);
            nl.FillDelegate(out BrotliDecoderSetParameter);
            nl.FillDelegate(out BrotliDecoderDecompressStream);
            nl.FillDelegate(out BrotliDecoderDestroyInstance);
            nl.FillDelegate(out BrotliDecoderVersion);
            nl.FillDelegate(out BrotliDecoderIsUsed);
            nl.FillDelegate(out BrotliDecoderIsFinished);
            nl.FillDelegate(out BrotliDecoderGetErrorCode);
            nl.FillDelegate(out BrotliDecoderErrorString);
            nl.FillDelegate(out BrotliDecoderTakeOutput);
            #endregion
        }
        #region Encoder
        internal static Delegate64.BrotliEncoderCreateInstanceDelegate BrotliEncoderCreateInstance;
        internal static Delegate64.BrotliEncoderSetParameterDelegate BrotliEncoderSetParameter;
        //internal static Delegate64.BrotliEncoderSetCustomDictionaryDelegate BrotliEncoderSetCustomDictionary;
        internal static Delegate64.BrotliEncoderCompressStreamDelegate BrotliEncoderCompressStream;
        internal static Delegate64.BrotliEncoderIsFinishedDelegate BrotliEncoderIsFinished;
        internal static Delegate64.BrotliEncoderDestroyInstanceDelegate BrotliEncoderDestroyInstance;
        internal static Delegate64.BrotliEncoderVersionDelegate BrotliEncoderVersion;
        internal static Delegate64.BrotliEncoderTakeOutputDelegate BrotliEncoderTakeOutput;
        #endregion
        #region Decoder
        internal static Delegate64.BrotliDecoderCreateInstanceDelegate BrotliDecoderCreateInstance;
        internal static Delegate64.BrotliDecoderSetParameter BrotliDecoderSetParameter;
        //internal static Delegate64.BrotliDecoderSetCustomDictionary BrotliDecoderSetCustomDictionary;
        internal static Delegate64.BrotliDecoderDecompressStreamDelegate BrotliDecoderDecompressStream;

        internal static Delegate64.BrotliDecoderDestroyInstanceDelegate BrotliDecoderDestroyInstance;

        internal static Delegate64.BrotliDecoderVersionDelegate BrotliDecoderVersion;
        internal static Delegate64.BrotliDecoderIsUsedDelegate BrotliDecoderIsUsed;
        internal static Delegate64.BrotliDecoderIsFinishedDelegate BrotliDecoderIsFinished;
        internal static Delegate64.BrotliDecoderGetErrorCodeDelegate BrotliDecoderGetErrorCode;
        internal static Delegate64.BrotliDecoderErrorStringDelegate BrotliDecoderErrorString;
        internal static Delegate64.BrotliDecoderTakeOutputDelegate BrotliDecoderTakeOutput;

        #endregion
    }
}
