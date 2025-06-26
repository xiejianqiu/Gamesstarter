using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
namespace Gamesstarter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int toProgress = 0;
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

                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 100)
                };
                timer.Tick += Update;
                timer.Start();
            };
            Index.Inst.DownloadProgressEvent += OnDLGameAppProgress;
            Index.Inst.Start();
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

            }
        }
        private void OnDLGameAppProgress(int progress)
        {
            this.toProgress = progress;
        }
    }
}
