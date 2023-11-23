using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        private Grid mainGrid;
        DispatcherTimer timer;   //  Generation timer
        private int genCounter;
        private List<AdWindow> adWindow;


        public MainWindow()
        {
            InitializeComponent();
            mainGrid = new Grid(MainCanvas);

            timer = new DispatcherTimer();
            timer.Tick += OnTimer;
            timer.Interval = TimeSpan.FromMilliseconds(200);
        }


        private void StartAd()
        {
            adWindow = new List<AdWindow>();
            for (int i = 0; i < 2; i++)
            {
                var window = new AdWindow(this);
                window.Closed += AdWindowOnClosed;
                window.Top = this.Top + (330 * i) + 70;
                window.Left = this.Left + 240;
                window.Show();
                adWindow.Add(window);
            }
        }

        private void AdWindowOnClosed(object sender, EventArgs eventArgs)
        {
            var c = adWindow.FirstOrDefault(x => x == sender);
            c.Dispose();
            adWindow.Remove(c);
        }


        private void Button_OnClick(object sender, EventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                ButtonStart.Content = "Stop";
                StartAd();
            }
            else
            {
                timer.Stop();
                ButtonStart.Content = "Start";
                if (adWindow.Any())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        adWindow.FirstOrDefault().Close();
                    }
                }
            }
        }

        private void OnTimer(object sender, EventArgs e)
        {
            mainGrid.Update();
            genCounter++;
            lblGenCount.Content = "Generations: " + genCounter;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Clear();
        }


    }
}
