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
        int minutes = 25;
        int seconds = 0;
        int startMinutes = 25;        
        Boolean isRunning = false;
        
        SoundPlayer player = new SoundPlayer(Properties.Resources.polepole);

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
                    btnUpMinute.Visibility = Visibility.Hidden;
                    btnDownMinute.Visibility = Visibility.Hidden;

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
            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += Timer_Tick;
            timer.Start();
            
            InitializeComponent();
        }


        private void StartStopTimer(object sender, RoutedEventArgs e)
        {
           
            
            if (!isRunning)
            {
                
                isRunning = true;
                btnStartStop.Content = "Stop";
            }
            else
            {
                
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
                isRunning = false;
                btnStartStop.Content = "Start";
            }
        }


        private void ResetTimer(object sender, RoutedEventArgs e)
        {

            minutes = startMinutes;
            isRunning = false;
            //minutes = 25;
            seconds = 0;
            
            
            btnStartStop.Content = "Start";
            player.Stop();

            btnUpMinute.Visibility = Visibility.Visible;
            btnDownMinute.Visibility = Visibility.Visible;
            tbTimer.Text = minutes.ToString() + ":00";
            
          

        }

        private void BtnUpMinute_Click(object sender, RoutedEventArgs e)
        {
           

            isRunning = false;
            minutes++;
            startMinutes++;
            seconds = 0;
            btnStartStop.Content = "Start";

            if(minutes<10)
                tbTimer.Text = "0"+minutes.ToString() + ":00";
            else
                tbTimer.Text = minutes.ToString() + ":00";
        }

        private void BtnDownMinute_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
            seconds = 0;
            btnStartStop.Content = "Start";
            
            if(minutes<10)
                tbTimer.Text = "0" + minutes.ToString() + ":00";
            else
                tbTimer.Text = minutes.ToString() + ":00";

            if (minutes > 1)
            {
                minutes--;
                startMinutes--;
                seconds = 60;
            }
            
        }

       
    }
}
