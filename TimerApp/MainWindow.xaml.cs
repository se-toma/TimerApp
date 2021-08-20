using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

namespace TimerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;
        Stopwatch stopwatch = new Stopwatch();
        Boolean isBreak = false;
        int counter = 0;
        private System.Media.SoundPlayer player = null;

        public MainWindow()
        {
            InitializeComponent();
            this.timer = new DispatcherTimer(DispatcherPriority.Normal);
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.timer.Tick += timer_Tick;
            this.timer.Start();

        }

        // start
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Start();
        }

        // stop
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stopwatch.Stop();
        }

        // reset
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            stopwatch.Reset();
            isBreak = false;
            counter = 0;
            count.Text = counter.ToString();
        }

        /// <summary>
        /// タイマー生成処理
        /// </summary>
        /// <returns>生成したタイマー</returns>
        void timer_Tick(object sender, EventArgs e)
        {
            if(stopwatch.IsRunning == false)
            {
                status.Text = "停止中";
            }
            else
            {
                //　休憩かつ、5分経過(休憩終了)
                if (isBreak && stopwatch.Elapsed.Minutes >= 5)
                    //if (isBreak && stopwatch.Elapsed.Seconds >= 5)
                    {
                    isBreak = false;
                    stopwatch.Reset();
                    stopwatch.Start();
                    status.Text = "作業中";
                    this.Activate();
                    playSound("start.wav");
                    MessageBox.Show("作業開始。");
                } //　作業中かつ、25分経過(作業終了)
                else if (!isBreak && stopwatch.Elapsed.Minutes >= 25)
                    //else if (!isBreak && stopwatch.Elapsed.Seconds >= 10)
                        {
                    isBreak = true;
                    stopwatch.Reset();
                    stopwatch.Start();
                    counter++;
                    count.Text = counter.ToString();
                    status.Text = "休憩中";
                    this.Activate();
                    playSound("end.wav");
                    MessageBox.Show("休憩しましょう。");
                }                
                else if (isBreak)
                {
                    status.Text = "休憩中";
                } 
                else if (!isBreak)
                {
                    status.Text = "作業中";
                }

            }

            
            textBlock.Text = String.Format("{0:00}:{1:00}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds);
        }        

        private void playSound(String soundFile)
        {
            player = new System.Media.SoundPlayer(soundFile);
            player.Play();
        }
    }
}
