using CortanaViewer_WPF.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaViewer_WPF.Utils
{
    public class MusicUtils
    {
        public string GetMusicName()
        {
            try
            {
                Process[] CloudmusicProcesses = Process.GetProcessesByName("cloudmusic");
                Process[] QQMusicprocess = Process.GetProcessesByName("QQMusic");
                if (CloudmusicProcesses.Length == 0 && QQMusicprocess.Length == 0)
                {
                    return string.Empty;
                }
                else if (QQMusicprocess.Length != 0)
                {
                    return GetQQMusicText();
                }
                else if (CloudmusicProcesses.Length != 0)
                {
                    return GetCloudMusicText();
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception exception)
            {
                return string.Empty;
            }
        }

        private static string GetCloudMusicText()
        {
            IntPtr intPtr = dllHelper.FindWindow("OrpheusBrowserHost", null);
            StringBuilder stringBuilder = new StringBuilder(dllHelper.GetWindowTextLength(intPtr) + 1);
            dllHelper.GetWindowText(intPtr, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }

        private static string GetQQMusicText()
        {
            IntPtr intPtr = dllHelper.FindWindow("QQMusic_Daemon_Wnd", null);
            StringBuilder stringBuilder = new StringBuilder(dllHelper.GetWindowTextLength(intPtr) + 1);
            dllHelper.GetWindowText(intPtr, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }
    }
}
