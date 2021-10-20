using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Brotli
{
    internal class NativeLibraryLoader
    {
        internal static bool IsWindows = false;
        internal static bool IsLinux = false;
        internal static bool IsMacOSX = false;
        internal static bool IsNetCore = false;
        internal static bool Is64Bit = false;
        static NativeLibraryLoader()
        {
#if NET35 || NET40
            IsWindows=true;
#else
            IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            IsMacOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            IsNetCore = RuntimeInformation.FrameworkDescription.StartsWith(".NET Core");
#endif
            if (!IsWindows && !IsLinux && !IsMacOSX)
            {
                throw new InvalidOperationException("Unsupported platform.");
            }
            Is64Bit = IntPtr.Size == 8;
        }

        // dlopen flags
        const int RtldLazy = 1;
        const int RtldGlobal = 8;

        readonly string _libraryPath;
        internal IntPtr Handle { get; private set; }

        internal NativeLibraryLoader(string libraryPath)
        {
            _libraryPath = libraryPath;
            Handle = LoadLibrary(_libraryPath, out var errorMsg);
            if (!String.IsNullOrEmpty(errorMsg))
            {
                throw new BrotliException($"unable to load library {libraryPath}");
            }
        }

        public static IntPtr GetWin32ModuleHandle(String moduleName)
        {
            IntPtr r = IntPtr.Zero;
            foreach (ProcessModule mod in Process.GetCurrentProcess().Modules)
            {
                if (mod.ModuleName == moduleName)
                {
                    r = mod.BaseAddress;
                    break;
                }
            }
            return r;
        }

        /// <summary>
        /// Loads method in the library
        /// </summary>
        /// <param name="symbolName">The methold of the library</param>
        /// <returns>method address</returns>
        private IntPtr LoadSymbol(string symbolName)
        {
            if (IsWindows)
            {
              return WindowsLoader.GetProcAddress(Handle, symbolName);
            }
            if (IsLinux)
            {

                if (IsNetCore)
                {
                    return CoreCLRLoader.dlsym(Handle, symbolName);
                }
                return LinuxLoader.dlsym(Handle, symbolName);
            }
            if (IsMacOSX)
            {
                return MacOSXLoader.dlsym(Handle, symbolName);
            }
            throw new InvalidOperationException("Unsupported platform.");
        }

        public void FillDelegate<T>(out T delegateType) where T : class
        {
            var typeName = typeof(T).Name;
            var kt = "Delegate";
            if (typeName.EndsWith(kt))
            {
                typeName = typeName.Substring(0, typeName.Length - kt.Length);
            }
            delegateType = GetNativeMethodDelegate<T>(typeName);
        }


        public T GetNativeMethodDelegate<T>(string methodName) where T : class
        {
            var ptr = LoadSymbol(methodName);
            if (ptr == IntPtr.Zero)
            {
                throw new MissingMethodException(string.Format("The native method \"{0}\" does not exist", methodName));
            }

            return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T)) as T;  // generic version not available in .NET45
        }

        internal static string[] GetPossibleRuntimeDirectories()
        {
#if NET35 || NET40
            var assemblyDirectory = Path.GetDirectoryName(typeof(LibPathBootStrapper).Assembly.Location);
#else
            var assemblyDirectory = Path.GetDirectoryName(typeof(LibPathBootStrapper).GetTypeInfo().Assembly.Location);
#endif
            //var platform = "win";
            //if (IsLinux)
            //{
            //    platform= "linux";
            //}
            //if (IsMacOSX)
            //{
            //    platform= "osx";
            //}
            //string runtimesDirectory = string.Format("runtimes/{0}/native", platform);
            string runtimesDirectory;
            if (Is64Bit)
            {
                runtimesDirectory = "x64"; // string.Format("x64/{0}", platform);
            }
            else
            {
                runtimesDirectory = "x86"; // string.Format("x86/{0}", platform);
            }

            string runtimesFullDirectory = Path.Combine(assemblyDirectory,runtimesDirectory);
            var iisBaseDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}bin/{runtimesDirectory}";
            var execAssemblyDirectory= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\","")+"/"+ runtimesDirectory;            
#if NET35
            var netCoreAppStyleDirectory = Path.Combine(Path.Combine(assemblyDirectory, "../.."), runtimesDirectory);
#else
            var netCoreAppStyleDirectory = Path.Combine(assemblyDirectory, "../..", runtimesDirectory);
#endif
            string[] paths = new[] { assemblyDirectory, runtimesFullDirectory, runtimesDirectory, netCoreAppStyleDirectory,iisBaseDirectory,execAssemblyDirectory };
            paths = paths.Select(v => v.Replace('/', Path.DirectorySeparatorChar)).ToArray();
            return paths;
        }

        internal static bool FreeLibrary(IntPtr handle)
        {
            string errorMsg = null;
            if (IsWindows)
            {
                return WindowsLoader.FreeLibrary(handle);
            }
            if (IsLinux)
            {
                if (IsNetCore)
                {
                    return UnloadLibraryPosix(CoreCLRLoader.dlclose, CoreCLRLoader.dlerror, handle, out errorMsg);
                }
                return UnloadLibraryPosix(LinuxLoader.dlclose, LinuxLoader.dlerror, handle, out errorMsg);
            }
            if (IsMacOSX)
            {
                return UnloadLibraryPosix(MacOSXLoader.dlclose, MacOSXLoader.dlerror, handle, out errorMsg);
            }
            throw new InvalidOperationException("Unsupported platform.");
        }

        /// <summary>
        /// Load library
        /// </summary>
        internal static IntPtr LoadLibrary(string libraryPath, out string errorMsg)
        {
            if (IsWindows)
            {
                errorMsg = null;
                //var handle = GetWin32ModuleHandle(libraryPath);
                //if (handle != IntPtr.Zero) return handle;
                var handle= WindowsLoader.LoadLibrary(libraryPath);
                if (handle== IntPtr.Zero)
                {
                    throw new System.ComponentModel.Win32Exception($"failed to load library {libraryPath}");
                }
                return handle;
            }
            if (IsLinux)
            {
                if (IsNetCore)
                {
                    return LoadLibraryPosix(CoreCLRLoader.dlopen, CoreCLRLoader.dlerror, libraryPath, out errorMsg);
                }
                return LoadLibraryPosix(LinuxLoader.dlopen, LinuxLoader.dlerror, libraryPath, out errorMsg);
            }
            if (IsMacOSX)
            {
                return LoadLibraryPosix(MacOSXLoader.dlopen, MacOSXLoader.dlerror, libraryPath, out errorMsg);
            }
            throw new InvalidOperationException("Unsupported platform.");
        }

        static IntPtr LoadLibraryPosix(Func<string, int, IntPtr> dlopenFunc, Func<IntPtr> dlerrorFunc, string libraryPath, out string errorMsg)
        {
            errorMsg = null;
            IntPtr ret = dlopenFunc(libraryPath, RtldGlobal + RtldLazy);
            if (ret == IntPtr.Zero)
            {
                errorMsg = Marshal.PtrToStringAnsi(dlerrorFunc());
            }
            return ret;
        }

        static bool UnloadLibraryPosix(Func<IntPtr,int> dlcloseFunc, Func<IntPtr> dlerrorFunc, IntPtr handle,out string errorMsg)
        {
            errorMsg = null;
            var r = dlcloseFunc(handle);
            if (r!=0)
            {
                errorMsg= Marshal.PtrToStringAnsi(dlerrorFunc());
                return false;
            }
            return true;
        }
    }
}
