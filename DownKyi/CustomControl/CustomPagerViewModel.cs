using System.ComponentModel;
using System.Windows;

namespace DownKyi.CustomControl
{
    public class CustomPagerViewModel : INotifyPropertyChanged
    {
        public CustomPagerViewModel(int current, int count)
        {
            Current = current;
            Count = count;

            SetView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Current修改的回调
        public delegate void CurrentChangedHandler(int current);
        public event CurrentChangedHandler CurrentChanged;
        protected virtual void OnCurrentChanged(int current)
        {
            CurrentChanged?.Invoke(current);
        }

        // Count修改的回调
        public delegate void CountChangedHandler(int count);
        public event CountChangedHandler CountChanged;
        protected virtual void OnCountChanged(int count)
        {
            CountChanged?.Invoke(count);
        }

        #region 绑定属性

        private Visibility visibility;
        public Visibility Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Visibility"));
            }
        }

        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value < Current || value < 1)
                {
                    //throw new Exception("数值不在允许的范围内。");
                    System.Console.WriteLine(value.ToString());
                }
                else
                {
                    count = value;

                    if (count == 1) { Visibility = Visibility.Hidden; }
                    else { Visibility = Visibility.Visible; }

                    //SetView();

                    OnCountChanged(count);

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
                }
            }
        }

        private int current;
        public int Current
        {
            get
            {
                if (current < 1) { current = 1; }
                return current;
            }
            set
            {
                if (Count > 0 && (value > Count || value < 1))
                {
                    //throw new Exception("数值不在允许的范围内。");
                }
                else
                {
                    current = value;

                    //SetView();

                    OnCurrentChanged(current);

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Current"));
                }
            }
        }

        private int first;
        public int First
        {
            get { return first; }
            set
            {
                first = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("First"));
            }
        }

        private int previousSecond;
        public int PreviousSecond
        {
            get { return previousSecond; }
            set
            {
                previousSecond = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviousSecond"));
            }
        }

        private int previousFirst;
        public int PreviousFirst
        {
            get { return previousFirst; }
            set
            {
                previousFirst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviousFirst"));
            }
        }

        private int nextFirst;
        public int NextFirst
        {
            get { return nextFirst; }
            set
            {
                nextFirst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NextFirst"));
            }
        }

        private int nextSecond;
        public int NextSecond
        {
            get { return nextSecond; }
            set
            {
                nextSecond = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NextSecond"));
            }
        }

        // 控制Current左边的控件
        private Visibility previousVisibility;
        public Visibility PreviousVisibility
        {
            get { return previousVisibility; }
            set
            {
                previousVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviousVisibility"));
            }
        }

        private Visibility firstVisibility;
        public Visibility FirstVisibility
        {
            get { return firstVisibility; }
            set
            {
                firstVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstVisibility"));
            }
        }

        private Visibility leftJumpVisibility;
        public Visibility LeftJumpVisibility
        {
            get { return leftJumpVisibility; }
            set
            {
                leftJumpVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LeftJumpVisibility"));
            }
        }

        private Visibility previousSecondVisibility;
        public Visibility PreviousSecondVisibility
        {
            get { return previousSecondVisibility; }
            set
            {
                previousSecondVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviousSecondVisibility"));
            }
        }

        private Visibility previousFirstVisibility;
        public Visibility PreviousFirstVisibility
        {
            get { return previousFirstVisibility; }
            set
            {
                previousFirstVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviousFirstVisibility"));
            }
        }

        // 控制Current右边的控件
        private Visibility nextFirstVisibility;
        public Visibility NextFirstVisibility
        {
            get { return nextFirstVisibility; }
            set
            {
                nextFirstVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NextFirstVisibility"));
            }
        }

        private Visibility nextSecondVisibility;
        public Visibility NextSecondVisibility
        {
            get { return nextSecondVisibility; }
            set
            {
                nextSecondVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NextSecondVisibility"));
            }
        }

        private Visibility rightJumpVisibility;
        public Visibility RightJumpVisibility
        {
            get { return rightJumpVisibility; }
            set
            {
                rightJumpVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RightJumpVisibility"));
            }
        }

        private Visibility lastVisibility;
        public Visibility LastVisibility
        {
            get { return lastVisibility; }
            set
            {
                lastVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastVisibility"));
            }
        }

        private Visibility nextVisibility;
        public Visibility NextVisibility
        {
            get { return nextVisibility; }
            set
            {
                nextVisibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NextVisibility"));
            }
        }

        #endregion


        private MyDelegateCommand previousCommand;
        public MyDelegateCommand PreviousCommand => previousCommand ?? (previousCommand = new MyDelegateCommand(PreviousExecuted));

        public void PreviousExecuted(object obj)
        {
            Current -= 1;

            SetView();
        }

        private MyDelegateCommand firstCommand;
        public MyDelegateCommand FirstCommand => firstCommand ?? (firstCommand = new MyDelegateCommand(FirstExecuted));

        public void FirstExecuted(object obj)
        {
            Current = 1;

            SetView();
        }

        private MyDelegateCommand previousSecondCommand;
        public MyDelegateCommand PreviousSecondCommand => previousSecondCommand ?? (previousSecondCommand = new MyDelegateCommand(PreviousSecondExecuted));

        public void PreviousSecondExecuted(object obj)
        {
            Current -= 2;

            SetView();
        }

        private MyDelegateCommand previousFirstCommand;
        public MyDelegateCommand PreviousFirstCommand => previousFirstCommand ?? (previousFirstCommand = new MyDelegateCommand(PreviousFirstExecuted));

        public void PreviousFirstExecuted(object obj)
        {
            Current -= 1;

            SetView();
        }

        private MyDelegateCommand nextFirstCommand;
        public MyDelegateCommand NextFirstCommand => nextFirstCommand ?? (nextFirstCommand = new MyDelegateCommand(NextFirstExecuted));

        public void NextFirstExecuted(object obj)
        {
            Current += 1;

            SetView();
        }

        private MyDelegateCommand nextSecondCommand;
        public MyDelegateCommand NextSecondCommand => nextSecondCommand ?? (nextSecondCommand = new MyDelegateCommand(NextSecondExecuted));

        public void NextSecondExecuted(object obj)
        {
            Current += 2;

            SetView();
        }

        private MyDelegateCommand lastCommand;
        public MyDelegateCommand LastCommand => lastCommand ?? (lastCommand = new MyDelegateCommand(LastExecuted));

        public void LastExecuted(object obj)
        {
            Current = Count;

            SetView();
        }

        private MyDelegateCommand nextCommand;
        public MyDelegateCommand NextCommand => nextCommand ?? (nextCommand = new MyDelegateCommand(NextExecuted));

        public void NextExecuted(object obj)
        {
            Current += 1;

            SetView();
        }

        /// <summary>
        /// 控制显示，暴力实现，以后重构
        /// </summary>
        private void SetView()
        {
            First = 1;
            PreviousSecond = Current - 2;
            PreviousFirst = Current - 1;
            NextFirst = Current + 1;
            NextSecond = Current + 2;

            // 控制Current左边的控件
            if (Current == 1)
            {
                PreviousVisibility = Visibility.Collapsed;
                FirstVisibility = Visibility.Collapsed;
                LeftJumpVisibility = Visibility.Collapsed;
                PreviousSecondVisibility = Visibility.Collapsed;
                PreviousFirstVisibility = Visibility.Collapsed;
            }
            else if (Current == 2)
            {
                PreviousVisibility = Visibility.Visible;
                FirstVisibility = Visibility.Collapsed;
                LeftJumpVisibility = Visibility.Collapsed;
                PreviousSecondVisibility = Visibility.Collapsed;
                PreviousFirstVisibility = Visibility.Visible;
            }
            else if (Current == 3)
            {
                PreviousVisibility = Visibility.Visible;
                FirstVisibility = Visibility.Collapsed;
                LeftJumpVisibility = Visibility.Collapsed;
                PreviousSecondVisibility = Visibility.Visible;
                PreviousFirstVisibility = Visibility.Visible;
            }
            else if (Current == 4)
            {
                PreviousVisibility = Visibility.Visible;
                FirstVisibility = Visibility.Visible;
                LeftJumpVisibility = Visibility.Collapsed;
                PreviousSecondVisibility = Visibility.Visible;
                PreviousFirstVisibility = Visibility.Visible;
            }
            else
            {
                PreviousVisibility = Visibility.Visible;
                FirstVisibility = Visibility.Visible;
                LeftJumpVisibility = Visibility.Visible;
                PreviousSecondVisibility = Visibility.Visible;
                PreviousFirstVisibility = Visibility.Visible;
            }

            // 控制Current右边的控件
            if (Current == Count)
            {
                NextFirstVisibility = Visibility.Collapsed;
                NextSecondVisibility = Visibility.Collapsed;
                RightJumpVisibility = Visibility.Collapsed;
                LastVisibility = Visibility.Collapsed;
                NextVisibility = Visibility.Collapsed;
            }
            else if (Current == Count - 1)
            {
                NextFirstVisibility = Visibility.Visible;
                NextSecondVisibility = Visibility.Collapsed;
                RightJumpVisibility = Visibility.Collapsed;
                LastVisibility = Visibility.Collapsed;
                NextVisibility = Visibility.Visible;
            }
            else if (Current == Count - 2)
            {
                NextFirstVisibility = Visibility.Visible;
                NextSecondVisibility = Visibility.Visible;
                RightJumpVisibility = Visibility.Collapsed;
                LastVisibility = Visibility.Collapsed;
                NextVisibility = Visibility.Visible;
            }
            else if (Current == Count - 3)
            {
                NextFirstVisibility = Visibility.Visible;
                NextSecondVisibility = Visibility.Visible;
                RightJumpVisibility = Visibility.Collapsed;
                LastVisibility = Visibility.Visible;
                NextVisibility = Visibility.Visible;
            }
            else
            {
                NextFirstVisibility = Visibility.Visible;
                NextSecondVisibility = Visibility.Visible;
                RightJumpVisibility = Visibility.Visible;
                LastVisibility = Visibility.Visible;
                NextVisibility = Visibility.Visible;
            }
        }





    }
}
