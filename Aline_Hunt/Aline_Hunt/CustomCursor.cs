using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Aline_Hunt
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotSpot;
        public int yHotSpot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    internal class CustomCursor
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);
        public static System.Windows.Forms.Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotSpot = xHotSpot;
            tmp.yHotSpot = yHotSpot;
            tmp.fIcon = false;
            ptr = CreateIconIndirect(ref tmp);
            return new System.Windows.Forms.Cursor(ptr);
        }
    }
}
