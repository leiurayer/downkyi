using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Brotli
{
    internal class AsyncHelper
    {
        private static readonly TaskFactory _myTaskFactory = new
         TaskFactory(CancellationToken.None, TaskCreationOptions.None,
         TaskContinuationOptions.None, TaskScheduler.Default);

        internal static void RunSync(Func<Task> func, bool await = false)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            _myTaskFactory.StartNew(delegate
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap().ConfigureAwait(await).GetAwaiter().GetResult();
        }

        // Microsoft.AspNet.Identity.AsyncHelper
        internal static TResult RunSync<TResult>(Func<Task<TResult>> func, bool await = false)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return _myTaskFactory.StartNew<Task<TResult>>(delegate
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap<TResult>().ConfigureAwait(await).GetAwaiter().GetResult();
        }




        internal static TResult RunSync<TResult>(Func<object, Task<TResult>> func, object state, bool await = false)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return _myTaskFactory.StartNew<Task<TResult>>(delegate
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func(state);
            }, state).Unwrap<TResult>().ConfigureAwait(await).GetAwaiter().GetResult();
        }

        internal static TResult RunSync<TResult>(Func<object, Task<TResult>> func, object state, CancellationToken cancellationToken, bool await = false)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return _myTaskFactory.StartNew<Task<TResult>>(delegate
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func(state);
            }, state, cancellationToken).Unwrap<TResult>().ConfigureAwait(await).GetAwaiter().GetResult();
        }

    }
}
