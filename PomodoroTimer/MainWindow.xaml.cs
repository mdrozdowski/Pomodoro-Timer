using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PomodoroTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        private DispatcherTimer timer = new DispatcherTimer();
        int minutes = 0;
        int seconds = 5;
        Boolean isRunning = true;
        SoundPlayer player = new SoundPlayer(PomodoroTimer.Properties.Resources.polepole);

        void Timer_Tick(object sender, EventArgs e)
        {
            if (isRunning)
            {
                seconds--;
                if (seconds == -1)
                {
                    minutes--;
                    seconds = 59;
                }
                if (minutes < 0)
                {
                    tbTimer.Text = "Time is up!";
                    PlayAlarmSong();
                    isRunning = false;
                }
                else if (minutes < 10 && seconds < 10)
                    tbTimer.Text = "0" + minutes + ":0" + seconds;
                else if (minutes < 10)
                    tbTimer.Text = "0" + minutes + ":" + seconds;
                else if (seconds < 10)
                    tbTimer.Text = minutes + ":0" + seconds;


                else
                    tbTimer.Text = minutes + ":" + seconds;

            }
        }

        void PlayAlarmSong()
        {
            player.Play();
        }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void StartTimer(object sender, EventArgs e)
        {
            if (isRunning)
            {
                timer.Interval = TimeSpan.FromSeconds(1);

                timer.Tick += Timer_Tick;
                timer.Start();
            }
            isRunning = true;
        }




        private void StopTimer(object sender, RoutedEventArgs e)
        {
            isRunning = false;
           int _minutes = minutes;
           int _seconds = seconds;
            if (minutes < 0)
            ResetTimer(sender, e);

            else if (_minutes < 10 && _seconds < 10)
                tbTimer.Text = "0" + _minutes + ":0" + _seconds;
            else if (_minutes < 10)
                tbTimer.Text = "0" + _minutes + ":" + _seconds;
            else if (_seconds < 10)
                tbTimer.Text = _minutes + ":0" + _seconds;

            else
                tbTimer.Text = _minutes + ":" + _seconds;
            player.Stop();
        }

        private void ResetTimer(object sender, RoutedEventArgs e)
        {
            isRunning = false;
            minutes = 24;
            seconds = 60;
            tbTimer.Text = "25:00";
            player.Stop();

        }
    }
}
