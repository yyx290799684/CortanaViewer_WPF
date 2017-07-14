using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CortanaViewer_WPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static System.Windows.Forms.NotifyIcon _NotifyIcon = new System.Windows.Forms.NotifyIcon();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow _MainWindow = new MainWindow();
            ShowNotifyIcon();
        }

        /// <summary>
        /// 显示系统托盘
        /// </summary>
        private void ShowNotifyIcon()
        {
            _NotifyIcon.Text = "CortanaViewer";
            _NotifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            _NotifyIcon.Visible = true;

            //添加右键菜单
            System.Windows.Forms.MenuItem exitMenuItem = new System.Windows.Forms.MenuItem("退出");
            exitMenuItem.Click += new EventHandler(exit_Click);
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { exitMenuItem };
            _NotifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            RemoveTrayIcon();
            this.Shutdown();
        }

        private void RemoveTrayIcon()
        {
            if (_NotifyIcon != null)
            {
                _NotifyIcon.Visible = false;
                _NotifyIcon.Dispose();
                _NotifyIcon = null;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
