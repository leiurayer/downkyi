using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DownKyi.ui.CustomControl
{
    /// <summary>
    /// CustomPager.xaml 的交互逻辑
    /// </summary>
    public partial class CustomPager : UserControl
    {
        public CustomPager()
        {
            InitializeComponent();
            DataContext = this;
        }

        // Current修改的回调
        public delegate void CurrentChangedHandler(int current);
        public event CurrentChangedHandler CurrentChanged;
        protected virtual void OnCurrentChanged(int current)
        {
            CurrentChanged?.Invoke(current);
        }

        // Count修改的回调
        public delegate void CountChangedHandler(int current);
        public event CountChangedHandler CountChanged;
        protected virtual void OnCountChanged(int count)
        {
            CountChanged?.Invoke(count);
        }

        private int current;
        public int Current
        {
            get
            {
                var value = GetValue(CurrentProperty);
                if (value == null) { return 1; }

                if ((int)value < 1)
                {
                    return 1;
                }
                return (int)value;
            }
            set
            {
                SetValue(CurrentProperty, value);

                if (Count > 0 && (value > Count || value < 1))
                {
                    //throw new Exception("数值不在允许的范围内。");
                }
                else
                {
                    current = value;
                    SetView();

                    OnCurrentChanged(current);
                }
            }
        }

        public static readonly DependencyProperty CurrentProperty =
        DependencyProperty.Register(
            "Current",
            typeof(object),
            typeof(CustomPager),
            new PropertyMetadata(default, OnCurrentPropertyChanged));

        private static void OnCurrentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //CustomPager source = d as CustomPager;
        }

        //private int count;
        public int Count
        {
            get
            {
                return (int)GetValue(CountProperty);
            }
            set
            {
                SetValue(CountProperty, value);

                //if (value < Current || value < 1)
                //{
                //    //throw new Exception("数值不在允许的范围内。");
                //}
                //else
                //{
                //    count = value;
                //    if (count == 1) { Visibility = Visibility.Hidden; }
                //    else { Visibility = Visibility.Visible; }

                //    SetView();

                //    OnCountChanged(count);
                //}
            }
        }

        public static readonly DependencyProperty CountProperty =
        DependencyProperty.Register(
            "Count",
            typeof(object),
            typeof(CustomPager),
            new PropertyMetadata(default, OnCountPropertyChanged));

        private static void OnCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomPager source = d as CustomPager;

            if ((int)e.NewValue < source.Current || (int)e.NewValue < 1)
            {
                //throw new Exception("数值不在允许的范围内。");
            }
            else
            {
                //source.Count = (int)e.NewValue;
                if (source.Count == 1) { source.Visibility = Visibility.Hidden; }
                else { source.Visibility = Visibility.Visible; }

                source.SetView();

                source.OnCountChanged(source.Count);
            }
        }

        private void Previous_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current -= 1;
        }

        private void First_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current = 1;
        }

        private void PreviousSecond_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current -= 2;
        }

        private void PreviousFirst_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current -= 1;
        }

        private void Current_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { }

        private void NextFirst_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current += 1;
        }

        private void NextSecond_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current += 2;
        }

        private void Last_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current = Count;
        }

        private void Next_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Current += 1;
        }

        /// <summary>
        /// 控制显示，暴力实现，以后重构
        /// </summary>
        protected void SetView()
        {
            nameFirstText.Text = "1";
            namePreviousSecondText.Text = (Current - 2).ToString();
            namePreviousFirstText.Text = (Current - 1).ToString();
            nameCurrentText.Text = Current.ToString();
            nameNextFirstText.Text = (Current + 1).ToString();
            nameNextSecondText.Text = (Current + 2).ToString();
            nameLastText.Text = Count.ToString();

            // 控制Current左边的控件
            if (Current == 1)
            {
                namePrevious.Visibility = Visibility.Collapsed;
                nameFirst.Visibility = Visibility.Collapsed;
                nameLeftJump.Visibility = Visibility.Collapsed;
                namePreviousSecond.Visibility = Visibility.Collapsed;
                namePreviousFirst.Visibility = Visibility.Collapsed;
            }
            else if (Current == 2)
            {
                namePrevious.Visibility = Visibility.Visible;
                nameFirst.Visibility = Visibility.Collapsed;
                nameLeftJump.Visibility = Visibility.Collapsed;
                namePreviousSecond.Visibility = Visibility.Collapsed;
                namePreviousFirst.Visibility = Visibility.Visible;
            }
            else if (Current == 3)
            {
                namePrevious.Visibility = Visibility.Visible;
                nameFirst.Visibility = Visibility.Collapsed;
                nameLeftJump.Visibility = Visibility.Collapsed;
                namePreviousSecond.Visibility = Visibility.Visible;
                namePreviousFirst.Visibility = Visibility.Visible;
            }
            else if (Current == 4)
            {
                namePrevious.Visibility = Visibility.Visible;
                nameFirst.Visibility = Visibility.Visible;
                nameLeftJump.Visibility = Visibility.Collapsed;
                namePreviousSecond.Visibility = Visibility.Visible;
                namePreviousFirst.Visibility = Visibility.Visible;
            }
            else
            {
                namePrevious.Visibility = Visibility.Visible;
                nameFirst.Visibility = Visibility.Visible;
                nameLeftJump.Visibility = Visibility.Visible;
                namePreviousSecond.Visibility = Visibility.Visible;
                namePreviousFirst.Visibility = Visibility.Visible;
            }

            // 控制Current右边的控件
            if (Current == Count)
            {
                nameNextFirst.Visibility = Visibility.Collapsed;
                nameNextSecond.Visibility = Visibility.Collapsed;
                nameRightJump.Visibility = Visibility.Collapsed;
                nameLast.Visibility = Visibility.Collapsed;
                nameNext.Visibility = Visibility.Collapsed;
            }
            else if (Current == Count - 1)
            {
                nameNextFirst.Visibility = Visibility.Visible;
                nameNextSecond.Visibility = Visibility.Collapsed;
                nameRightJump.Visibility = Visibility.Collapsed;
                nameLast.Visibility = Visibility.Collapsed;
                nameNext.Visibility = Visibility.Visible;
            }
            else if (Current == Count - 2)
            {
                nameNextFirst.Visibility = Visibility.Visible;
                nameNextSecond.Visibility = Visibility.Visible;
                nameRightJump.Visibility = Visibility.Collapsed;
                nameLast.Visibility = Visibility.Collapsed;
                nameNext.Visibility = Visibility.Visible;
            }
            else if (Current == Count - 3)
            {
                nameNextFirst.Visibility = Visibility.Visible;
                nameNextSecond.Visibility = Visibility.Visible;
                nameRightJump.Visibility = Visibility.Collapsed;
                nameLast.Visibility = Visibility.Visible;
                nameNext.Visibility = Visibility.Visible;
            }
            else
            {
                nameNextFirst.Visibility = Visibility.Visible;
                nameNextSecond.Visibility = Visibility.Visible;
                nameRightJump.Visibility = Visibility.Visible;
                nameLast.Visibility = Visibility.Visible;
                nameNext.Visibility = Visibility.Visible;
            }
        }

    }
}
