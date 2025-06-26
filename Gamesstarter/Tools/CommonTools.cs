using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Tools
{
    public static class CommonTools
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        public static void Exit()
        {
            Environment.Exit(0);
        }
        public static void RunExe(string exePath, string arguments)
        {
            var startIinfo = new ProcessStartInfo(exePath, arguments)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Normal
            };
            using (var process = Process.Start(startIinfo))
            {
                process.WaitForInputIdle();
                SetForegroundWindow(process.MainWindowHandle);
            }
        }
        public static void RunURL()
        {
            Process.Start("https://jq.qq.com/?_wv=1027&k=FMIcacvI");
        }
        public static void DelAndCreate(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }
    }
}
