using System;
using System.Runtime.InteropServices;

namespace Brotli
{
    class Brolib32
    {
        static Brolib32()
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
        internal static Delegate32.BrotliEncoderCreateInstanceDelegate BrotliEncoderCreateInstance;
        internal static Delegate32.BrotliEncoderSetParameterDelegate BrotliEncoderSetParameter;
        //internal static Delegate32.BrotliEncoderSetCustomDictionaryDelegate BrotliEncoderSetCustomDictionary;
        internal static Delegate32.BrotliEncoderCompressStreamDelegate BrotliEncoderCompressStream;
        internal static Delegate32.BrotliEncoderIsFinishedDelegate BrotliEncoderIsFinished;
        internal static Delegate32.BrotliEncoderDestroyInstanceDelegate BrotliEncoderDestroyInstance;
        internal static Delegate32.BrotliEncoderVersionDelegate BrotliEncoderVersion;
        internal static Delegate32.BrotliEncoderTakeOutputDelegate BrotliEncoderTakeOutput;
        #endregion
        #region Decoder
        internal static Delegate32.BrotliDecoderCreateInstanceDelegate BrotliDecoderCreateInstance;
        internal static Delegate32.BrotliDecoderSetParameter BrotliDecoderSetParameter;
        //internal static Delegate32.BrotliDecoderSetCustomDictionary BrotliDecoderSetCustomDictionary;
        internal static Delegate32.BrotliDecoderDecompressStreamDelegate BrotliDecoderDecompressStream;

        internal static Delegate32.BrotliDecoderDestroyInstanceDelegate BrotliDecoderDestroyInstance;

        internal static Delegate32.BrotliDecoderVersionDelegate BrotliDecoderVersion;
        internal static Delegate32.BrotliDecoderIsUsedDelegate BrotliDecoderIsUsed;
        internal static Delegate32.BrotliDecoderIsFinishedDelegate BrotliDecoderIsFinished;
        internal static Delegate32.BrotliDecoderGetErrorCodeDelegate BrotliDecoderGetErrorCode;
        internal static Delegate32.BrotliDecoderErrorStringDelegate BrotliDecoderErrorString;
        internal static Delegate32.BrotliDecoderTakeOutputDelegate BrotliDecoderTakeOutput;

        #endregion
    }
}
