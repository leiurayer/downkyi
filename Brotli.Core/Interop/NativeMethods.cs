using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Brotli
{

    static class WindowsLoader
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string dllFilePath);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);
    }

    static class LinuxLoader
    {
        [DllImport("libdl.so")]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libdl.so")]
        internal static extern IntPtr dlerror();

        [DllImport("libdl.so")]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libdl.so")]
        internal static extern int dlclose(IntPtr handle);
    }

    static class MacOSXLoader
    {
        [DllImport("libSystem.dylib")]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libSystem.dylib")]
        internal static extern IntPtr dlerror();

        [DllImport("libSystem.dylib")]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libSystem.dylib")]
        internal static extern int dlclose(IntPtr handle);

    }


    /// <summary>
    /// Similarly as for Mono on Linux, we load symbols for
    /// dlopen and dlsym from the "libcoreclr.so",
    /// to avoid the dependency on libc-dev Linux.
    /// </summary>
    static class CoreCLRLoader
    {
        [DllImport("libcoreclr.so")]
        internal static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libcoreclr.so")]
        internal static extern IntPtr dlerror();

        [DllImport("libcoreclr.so")]
        internal static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libcoreclr.so")]
        internal static extern int dlclose(IntPtr handle);
    }
}
