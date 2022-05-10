using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace DownKyi.Utils
{
    /// <summary>
    /// 为窗口<see cref="Window"/>添加附加属性的辅助类
    /// 
    /// https://www.cnblogs.com/kybs0/p/9834023.html
    /// </summary>
    public class WindowAdaptation
    {
        #region 窗口宽度比例
        /// <summary>
        /// 窗口宽度比例 单位:小数(0 - 1.0]
        /// <para>窗口实际宽度=使用屏幕可显示区域（屏幕高度-任务栏高度）* 窗口宽度比例</para>
        /// </summary>
        public static readonly DependencyProperty WidthByScreenRatioProperty = DependencyProperty.RegisterAttached(
            "WidthByScreenRatio", typeof(double), typeof(WindowAdaptation), new PropertyMetadata(1.0, OnWidthByScreenRatioPropertyChanged));

        private static void OnWidthByScreenRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is double widthByScreenRatio)
            {
                if (widthByScreenRatio <= 0 || widthByScreenRatio > 1)
                {
                    throw new ArgumentException($"屏幕比例不支持{widthByScreenRatio}");
                }

                var screenDisplayArea = GetScreenSize(window);
                var screenRatioWidth = screenDisplayArea.Width * widthByScreenRatio;

                //if (!double.IsNaN(window.Width) && screenDisplayArea.Width > window.Width)
                //{
                //    window.Width = window.Width;
                //}
                //else
                //{
                //    window.Width = screenRatioWidth;
                //}

                if (!double.IsNaN(window.Width) && screenRatioWidth > window.Width)
                {
                    window.Width = window.Width;
                }
                else if (!double.IsNaN(window.MinWidth) && screenRatioWidth < window.MinWidth)
                {
                    window.Width = screenDisplayArea.Width;
                }
                else
                {
                    window.Width = screenRatioWidth;
                }
            }
        }

        public static void SetWidthByScreenRatio(DependencyObject element, double value)
        {
            element.SetValue(WidthByScreenRatioProperty, value);
        }

        public static double GetWidthByScreenRatio(DependencyObject element)
        {
            return (double)element.GetValue(WidthByScreenRatioProperty);
        }

        #endregion

        #region 窗口高度比例
        /// <summary>
        /// 窗口宽度比例 单位:小数(0 - 1.0]
        /// <para>窗口实际宽度=使用屏幕可显示区域（屏幕高度-任务栏高度）* 窗口宽度比例</para>
        /// </summary>
        public static readonly DependencyProperty HeightByScreenRatioProperty = DependencyProperty.RegisterAttached(
            "HeightByScreenRatio", typeof(double), typeof(WindowAdaptation), new PropertyMetadata(1.0, OnHeightByScreenRatioPropertyChanged));

        private static void OnHeightByScreenRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window && e.NewValue is double heightByScreenRatio)
            {
                if (heightByScreenRatio <= 0 || heightByScreenRatio > 1)
                {
                    throw new ArgumentException($"屏幕比例不支持{heightByScreenRatio}");
                }

                var screenDisplayArea = GetScreenSize(window);
                var screenRatioHeight = screenDisplayArea.Height * heightByScreenRatio;

                if (!double.IsNaN(window.Height) && screenDisplayArea.Height > window.Height)
                {
                    window.Height = window.Height;
                }
                else
                {
                    window.Height = screenRatioHeight;
                }

                //if (!double.IsNaN(window.Height) && screenRatioHeight > window.Height)
                //{
                //    window.Height = window.Height;
                //}
                //else if (!double.IsNaN(window.MinHeight) && screenRatioHeight < window.MinHeight)
                //{
                //    window.Height = screenDisplayArea.Height;
                //}
                //else
                //{
                //    window.Height = screenRatioHeight;
                //}
            }
        }

        public static void SetHeightByScreenRatio(DependencyObject element, double value)
        {
            element.SetValue(HeightByScreenRatioProperty, value);
        }

        public static double GetHeightByScreenRatio(DependencyObject element)
        {
            return (double)element.GetValue(HeightByScreenRatioProperty);
        }

        #endregion

        const int DpiPercent = 96;
        private static dynamic GetScreenSize(Window window)
        {
            var intPtr = new WindowInteropHelper(window).Handle;//获取当前窗口的句柄
            var screen = Screen.FromHandle(intPtr);//获取当前屏幕
            using (Graphics currentGraphics = Graphics.FromHwnd(intPtr))
            {
                //分别获取当前屏幕X/Y方向的DPI
                double dpiXRatio = currentGraphics.DpiX / DpiPercent;
                double dpiYRatio = currentGraphics.DpiY / DpiPercent;

                var width = screen.WorkingArea.Width / dpiXRatio;
                var height = screen.WorkingArea.Height / dpiYRatio;

                return new { Width = width, Height = height };
            }
        }
    }
}
