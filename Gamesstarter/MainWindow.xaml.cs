﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Tools;

namespace Gamesstarter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int toProgress = 0;
        private string tip = string.Empty;
        private bool CanOpenLoginWindow = false;
        public MainWindow()
        {
            InitializeComponent();
            Init();
            Loaded += (f, s) =>
            {
                MouseDown += (x, y) =>
                {
                    if (y.LeftButton != MouseButtonState.Pressed)
                        return;
                    DragMove();
                };
            };
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 100)
            };
            timer.Tick += Update;
            timer.Start();
            Index.Inst.DownloadProgressEvent += OnDLGameAppProgress;
            Index.Inst.StepChangeEvent += OnStepChangeEvent;
            Index.Inst.OpenChannelLoginWindow += OpenChannelLoginWindow;
            Index.Inst.Start();
            CommonTools.CreateAppShortCut();
            LogTool.Instance.Info("MainWindow Start");
        }

        private void OpenChannelLoginWindow()
        {
            CanOpenLoginWindow = true;
        }

        private void OnStepChangeEvent(string msg)
        {
            this.tip = msg;
        }
        private void UpdateTipLab(string msg)
        {
            this.TipLab.Content =  msg;
        }

        private void Init()
        {
            this.ProgressBar.Value = 0;
        }
        public void Update(object source, EventArgs e)
        {
            var curProgress = this.ProgressBar.Value;
            var spd = (toProgress - curProgress) * 1f / 10;
            if (curProgress < toProgress)
            {
                this.ProgressBar.Value += 1 * spd;
                this.UpdateTipLab(string.Format(tip, this.ProgressBar.Value));

            }
            else
            {
                UpdateTipLab(this.tip);
            }

            if (CanOpenLoginWindow)
            {
                CommonTools.OpenCHannelWindow<ChannelLoginWindow>();
                CanOpenLoginWindow = false;
            }
        }
        private void OnDLGameAppProgress(int progress)
        {
            this.toProgress = progress;
        }

        private void OnCloseBtnClicked(object sender, RoutedEventArgs e)
        {
            CommonTools.Exit();
        }
    }
}
