using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CortanaViewer_WPF.Helper
{
    public class dllHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr FindWindowExA(IntPtr hWndParent, IntPtr hWndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool SetCursorPos(int X, int Y);
        [Flags]
        public enum MouseEventFlag : uint
        {
            Move = 1,
            LeftDown = 2,
            LeftUp = 4,
            RightDown = 8,
            RightUp = 16,
            MiddleDown = 32,
            MiddleUp = 64,
            XDown = 128,
            XUp = 256,
            Wheel = 2048,
            VirtualDesk = 16384,
            Absolute = 32768
        }
    }
}
