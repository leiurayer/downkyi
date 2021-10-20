using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Brotli
{
    public class Brolib
    {
        static bool UseX86 = IntPtr.Size == 4;
        #region Encoder
        public static IntPtr BrotliEncoderCreateInstance()
        {
            if (UseX86)
            {
                return Brolib32.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                return Brolib64.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }
        }


        public static IntPtr GetModuleHandle(String moduleName)
        {
            IntPtr r = IntPtr.Zero;
            foreach (ProcessModule mod in Process.GetCurrentProcess().Modules)
            {
                if (String.Compare(mod.ModuleName,moduleName,true)==0)
                {
                    r = mod.BaseAddress;
                    break;
                }
            }
            return r;
        }

        public static void FreeLibrary()
        {
           
            IntPtr libHandle = IntPtr.Zero;
            libHandle = GetModuleHandle(LibPathBootStrapper.LibPath);
            if (libHandle!=IntPtr.Zero)
            {
                NativeLibraryLoader.FreeLibrary(libHandle);
            }
        }

        public static bool BrotliEncoderSetParameter(IntPtr state, BrotliEncoderParameter parameter, UInt32 value)
        {
            if (UseX86)
            {
                return Brolib32.BrotliEncoderSetParameter(state, parameter, value);
            }
            else
            {
                return Brolib64.BrotliEncoderSetParameter(state, parameter, value);
            }
        }

        //@to rewrite using the following APIs
        //BrotliGetDictionary
        //BrotliGetTransforms
        //BrotliSetDictionaryData
        //BrotliTransformDictionaryWord
        //public static void BrotliEncoderSetCustomDictionary(IntPtr state, UInt32 size, IntPtr dict)
        //{
        //    if (UseX86)
        //    {
        //        Brolib32.BrotliEncoderSetCustomDictionary(state, size, dict);
        //    }
        //    else
        //    {
        //        Brolib64.BrotliEncoderSetCustomDictionary(state, size, dict);
        //    }
        //}

        public static bool BrotliEncoderCompressStream(
            IntPtr state, BrotliEncoderOperation op, ref UInt32 availableIn,
            ref IntPtr nextIn, ref UInt32 availableOut, ref IntPtr nextOut, out UInt32 totalOut)
        {
            if (UseX86)
            {
                return Brolib32.BrotliEncoderCompressStream(state, op, ref availableIn, ref nextIn, ref availableOut, ref nextOut, out totalOut);
            }
            else
            {
                UInt64 availableInL = availableIn;
                UInt64 availableOutL = availableOut;
                UInt64 totalOutL = 0;
                var r = Brolib64.BrotliEncoderCompressStream(state, op, ref availableInL, ref nextIn, ref availableOutL, ref nextOut, out totalOutL);
                availableIn = (UInt32)availableInL;
                availableOut = (UInt32)availableOutL;
                totalOut = (UInt32)totalOutL;
                return r;
            }
        }

        public static bool BrotliEncoderIsFinished(IntPtr state)
        {
            if (UseX86)
            {
                return Brolib32.BrotliEncoderIsFinished(state);
            }
            else
            {
                return Brolib64.BrotliEncoderIsFinished(state);
            }
        }

        public static void BrotliEncoderDestroyInstance(IntPtr state)
        {
            if (UseX86)
            {
                Brolib32.BrotliEncoderDestroyInstance(state);
            }
            else
            {
                Brolib64.BrotliEncoderDestroyInstance(state);
            }
        }

        public static UInt32 BrotliEncoderVersion()
        {
            if (UseX86)
            {
                return Brolib32.BrotliEncoderVersion();
            }
            else
            {
                return Brolib64.BrotliEncoderVersion();
            }
        }

        
        public static IntPtr BrotliDecoderTakeOutput(IntPtr state, ref UInt32 size)
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderTakeOutput(state, ref size);
            }
            else
            {
                UInt64 longSize = size;
                var r = Brolib64.BrotliDecoderTakeOutput(state, ref longSize);
                size = (UInt32)longSize;
                return r;
            }
        }



        #endregion
        #region Decoder
        public static IntPtr BrotliDecoderCreateInstance()
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }
            else
            {
                return Brolib64.BrotliDecoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public static bool BrotliDecoderSetParameter(IntPtr state, BrotliDecoderParameter param, UInt32 value)
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderSetParameter(state,param,value);
            }
            else
            {
                return Brolib64.BrotliDecoderSetParameter(state, param, value);
            }
        }


        //@to rewrite using the following APIs
        //BrotliGetDictionary
        //BrotliGetTransforms
        //BrotliSetDictionaryData
        //BrotliTransformDictionaryWord
        //public static void BrotliDecoderSetCustomDictionary(IntPtr state, UInt32 size, IntPtr dict)
        //{
        //    if (UseX86)
        //    {
        //        Brolib32.BrotliDecoderSetCustomDictionary(state, size, dict);
        //    }
        //    else
        //    {
        //        Brolib64.BrotliDecoderSetCustomDictionary(state, size, dict);
        //    }
        //}

        public static BrotliDecoderResult BrotliDecoderDecompressStream(
            IntPtr state, ref UInt32 availableIn,
            ref IntPtr nextIn, ref UInt32 availableOut, ref IntPtr nextOut, out UInt32 totalOut)
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderDecompressStream(state, ref availableIn, ref nextIn, ref availableOut, ref nextOut, out totalOut);
            }
            else
            {
                UInt64 availableInL = availableIn;
                UInt64 availableOutL = availableOut;
                UInt64 totalOutL = 0;
                var r = Brolib64.BrotliDecoderDecompressStream(state, ref availableInL, ref nextIn, ref availableOutL, ref nextOut, out totalOutL);
                availableIn = (UInt32)availableInL;
                availableOut = (UInt32)availableOutL;
                totalOut = (UInt32)totalOutL;
                return r;
            }
        }

        public static void BrotliDecoderDestroyInstance(IntPtr state)
        {
            if (UseX86)
            {
                Brolib32.BrotliDecoderDestroyInstance(state);
            }
            else
            {
                Brolib64.BrotliDecoderDestroyInstance(state);
            }
        }

        public static UInt32 BrotliDecoderVersion()
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderVersion();
            }
            else
            {
                return Brolib64.BrotliDecoderVersion();
            }
        }

        public static bool BrotliDecoderIsUsed(IntPtr state)
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderIsUsed(state);
            }
            else
            {
                return Brolib64.BrotliDecoderIsUsed(state);
            }
        }
        public static bool BrotliDecoderIsFinished(IntPtr state)
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderIsFinished(state);
            }
            else
            {
                return Brolib64.BrotliDecoderIsFinished(state);
            }

        }
        public static Int32 BrotliDecoderGetErrorCode(IntPtr state)
        {
            if (UseX86)
            {
                return Brolib32.BrotliDecoderGetErrorCode(state);
            }
            else
            {
                return Brolib64.BrotliDecoderGetErrorCode(state);
            }
        }

        public static String BrotliDecoderErrorString(Int32 code)
        {
            IntPtr r = IntPtr.Zero;
            if (UseX86)
            {
                r = Brolib32.BrotliDecoderErrorString(code);
            }
            else
            {
                r = Brolib64.BrotliDecoderErrorString(code);
            }

            if (r != IntPtr.Zero)
            {
                return Marshal.PtrToStringAnsi(r);
            }
            return String.Empty;


        }

        public static IntPtr BrotliEncoderTakeOutput(IntPtr state, ref UInt32 size)
        {
            if (UseX86)
            {
                return Brolib32.BrotliEncoderTakeOutput(state, ref size);
            }
            else
            {
                UInt64 longSize = size;
                var r = Brolib64.BrotliEncoderTakeOutput(state, ref longSize);
                size = (UInt32)longSize;
                return r;
            }
        }


        #endregion
    }
}
