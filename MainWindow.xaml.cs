using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Threading;
using AutoClickIt.Function;
using NHotkey.Wpf;
using NHotkey;

namespace AutoClickIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Thread Clicker;
        public MainWindow()
        {
            InitializeComponent();

            HotkeyManager.Current.AddOrReplace("Run/Stop", Key.F6, ModifierKeys.None, OnHotkeyPressed);
            HotkeyManager.Current.AddOrReplace("Run/Stop Alt", Key.F6, ModifierKeys.Control, OnHotkeyPressed);

            Closing += MainWindow_Closing;
            Loaded += MainWindow_Load;
        }

        private void OnHotkeyPressed(object sender, HotkeyEventArgs e)
        {

            if (!Autoclick.EnableClicker)
            {
                Startf();

            }
            else
            {
                Stopf();
            }


        }

        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            Stopf();
            Updater.CheckForUpdates();
            Thread.Sleep(1000);

            Clicker = new Thread(Autoclick.Click);
            Clicker.ApartmentState = ApartmentState.STA;
            Clicker.IsBackground = true;
            Clicker.Start();
        }



        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void Startf()
        {
            Thread.Sleep(300);
            StopBtn.BorderBrush = SystemColors.ControlLightBrush;
            StartBtn.BorderBrush = Brushes.Green;
            StartBtn.Opacity = 0.5;
            StartBtn.IsEnabled = false;
            StopBtn.IsEnabled = true;
            StopBtn.Opacity = 1;

            try
            {
                Autoclick.ClickInterval = Convert.ToInt32(ClickInterval.Text);
                Autoclick.Times = Convert.ToInt32(ClickTimes.Text);
                Autoclick.IsRepeatTimes = RepeatFE.IsChecked.Value;
                Autoclick.Repeater = RepeatTS.IsChecked.Value;
            }
            catch { }
            if (Autoclick.ClickInterval <= 50)
            {
                MessageBoxResult result = MessageBox.Show("Your click Interval is more than 20 clicks per sec if your clicks are too fast ur pc might run into problems do you wish to continue?", "! WARNING !", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (!Autoclick.EnableClicker)
                    {
                        Autoclick.EnableClicker = true;
                    }
                }
            }
            else if (!Autoclick.EnableClicker)
            {
                Autoclick.EnableClicker = true;
            }


        }
        public void Stopf()
        {
            Thread.Sleep(300);
            StartBtn.BorderBrush = SystemColors.ControlLightBrush;
            StopBtn.BorderBrush = Brushes.Red;
            StopBtn.Opacity = 0.5;
            StopBtn.IsEnabled = false;
            StartBtn.IsEnabled = true;
            StartBtn.Opacity = 1;

            if (Autoclick.EnableClicker)
            {
                Autoclick.EnableClicker = false;
            }

        }
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            Startf();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            Stopf();
        }
        private void RepeatTS_Click(object sender, RoutedEventArgs e)
        {
            if (RepeatFE.IsChecked.Value)
            {
                RepeatFE.IsChecked = false;
            }
        }

        private void RepeatFE_Click(object sender, RoutedEventArgs e)
        {
            if (RepeatTS.IsChecked.Value)
            {
                RepeatTS.IsChecked = false;
            }
        }
        private void ClickInterval_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                CPS.Content = $"= {(double)1000 / Convert.ToInt32(ClickInterval.Text)} CPS";
            }
            catch { CPS.Content = "= ?? CPS"; }

        }

        private void MoveOnDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }


    }
}
