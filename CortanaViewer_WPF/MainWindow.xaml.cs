using CortanaViewer_WPF.Helper;
using CortanaViewer_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace CortanaViewer_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Timer timer;
        private PerformanceUtils performanceUtils;
        private NetworkUtils networkUtils;
        private MusicUtils musicUtils;

        int status = 0;
        public MainWindow()
        {
            InitializeComponent();

            if ((int)Process.GetProcessesByName("CortanaViewer").Length >= 2)
            {
                System.Windows.Application.Current.Shutdown();
                return;
            }

            performanceUtils = new PerformanceUtils();
            //networkUtils = new NetworkUtils("Intel[R] Dual Band Wireless-AC 3165");
            networkUtils = new NetworkUtils();
            musicUtils = new MusicUtils();

            init();
        }

        private void init()
        {
            //App._NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(Icon_Click);
            timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Icon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowWindow();
            }
        }

        private void ShowWindow()
        {
            this.ShowInTaskbar = true;
            this.Activate();
            this.Visibility = Visibility.Visible;
            this.Show();
            this.WindowState = this.WindowState == WindowState.Minimized ? WindowState.Normal : this.WindowState;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (status == 0)
            {
                SetCPUInfo();
            }
            else if (status == 1)
            {
                SetNetworkInfo();
            }
            else if (status == 2)
            {
                if (string.IsNullOrWhiteSpace(SetMusicInfo()))
                {
                    status = 0;
                    SetCPUInfo();
                }
            }
            status = status == 2 ? 0 : status + 1;
        }
        private void SetCPUInfo()
        {
            string cpuinfoTem = "CPU：{0}%  内存：{1}%";
            string cpuinfo = string.Format(cpuinfoTem, string.Format("{0:F}", performanceUtils.CPUPercent), string.Format("{0:F}", performanceUtils.MemoryPercent));
            CortanaHelper.SetCortanaText(cpuinfo);
        }

        private void SetNetworkInfo()
        {
            string netinfoTem = "上传：{0}  下载：{1}";
            string netinfo = string.Format(netinfoTem, networkUtils.GetUploadSpeed(), networkUtils.GetDownloadSpeed());
            CortanaHelper.SetCortanaText(netinfo);
        }

        private string SetMusicInfo()
        {
            string musicinfo = musicUtils.GetMusicName();
            CortanaHelper.SetCortanaText(musicinfo);
            return musicinfo;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
            e.Cancel = true;
        }
        protected override void OnClosed(EventArgs e)
        {
            CortanaHelper.RestoreCortanaText();
            base.OnClosed(e);
        }
    }
}
