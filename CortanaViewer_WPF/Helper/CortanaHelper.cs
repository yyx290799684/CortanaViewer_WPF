using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaViewer_WPF.Helper
{
    public class CortanaHelper
    {
        public static void SetCortanaText(string text)
        {
            if (text.Length == 0 || text == GetCortanaText())
            {
                return;
            }
            IntPtr intPtr = dllHelper.FindWindow("Shell_TrayWnd", null);
            IntPtr intPtr1 = dllHelper.FindWindowExA(intPtr, IntPtr.Zero, "TrayDummySearchControl", null);
            IntPtr intPtr2 = dllHelper.FindWindowExA(intPtr1, IntPtr.Zero, "Static", null);
            //dllHelper.SendMessage(intPtr2, 14, 0, 0);
            dllHelper.SendMessage(intPtr2, 12, 0, text);
            //dllHelper.SendMessage(intPtr2, 245, 0, text);
        }

        public static string GetCortanaText()
        {
            IntPtr intPtr = dllHelper.FindWindow("Shell_TrayWnd", null);
            IntPtr intPtr1 = dllHelper.FindWindowExA(intPtr, IntPtr.Zero, "TrayDummySearchControl", null);
            IntPtr intPtr2 = dllHelper.FindWindowExA(intPtr1, IntPtr.Zero, "Static", null);
            StringBuilder stringBuilder = new StringBuilder(dllHelper.SendMessage(intPtr2, 14, 0, 0) + 1);
            dllHelper.GetWindowText(intPtr2, stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }

        public static void RestoreCortanaText()
        {
            SetCortanaText("在这里输入你要搜索的内容");
        }
    }
}
