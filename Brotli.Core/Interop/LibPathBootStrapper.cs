using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Brotli
{
    internal class LibPathBootStrapper
    {
        internal static string  LibPath { get; private set; }

        static LibPathBootStrapper()
        {
                
            
            string fileName = null;
            if (NativeLibraryLoader.IsWindows)
            {
                if (NativeLibraryLoader.Is64Bit)
                {
                    fileName = "brolib_x64.dll";
                }
                else
                {
                    fileName = "brolib_x86.dll";
                }
            } else if (NativeLibraryLoader.IsLinux)
            {
                if (NativeLibraryLoader.Is64Bit)
                {
                    fileName = "brolib_x64.so";
                }
                else
                {
                    fileName = "brolib_x86.so";
                }
            } else if (NativeLibraryLoader.IsMacOSX)
            {
                if (NativeLibraryLoader.Is64Bit)
                {
                    fileName = "brolib_x64.dylib";
                }
            }
            if (string.IsNullOrEmpty(fileName)) throw new NotSupportedException($"OS not supported:{Environment.OSVersion.ToString()}");
            var paths = NativeLibraryLoader.GetPossibleRuntimeDirectories();
            var libFound = false;
            foreach(var path in paths)
            {                
                var fullPath = Path.Combine(path, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    LibPath = fullPath;
                    libFound = true;
                    break;
                }
            }

            if (!libFound) throw new NotSupportedException($"Unable to find library {fileName}");
        }
    }
}
