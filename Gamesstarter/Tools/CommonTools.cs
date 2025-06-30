using Gamesstarter;
using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

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
        public static void OpenCHannelWindow<T>() where T:Window
        {
            try
            {
                Window newWindow = Activator.CreateInstance<T>() as Window;
                newWindow.Show();
                foreach (Window window in App.Current.Windows)
                {
                    if (window != newWindow)
                    {
                        window.Close();
                    }
                }

            }
            catch (Exception e)
            {
                LogTool.Instance.Error($"OpenCHannelWindow {e.ToString()}");
            }
        }
        /// <summary>
        /// 在创建app的快捷方式,在中文路径会出现乱码
        /// </summary>
        public static void CreateAppShortCut()
        {
            try
            {
                if (System.IO.File.Exists(GameConfig.GameExeLnkPath))
                    System.IO.File.Delete(GameConfig.GameExeLnkPath);
                string processName = Process.GetCurrentProcess().ProcessName + ".exe";
                string linkName = GameConfig.AppName;
                string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                using (StreamWriter writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
                {
                    var pathBytes = Environment.CurrentDirectory;
                    string exepath = Path.Combine(pathBytes, processName);
                    LogTool.Instance.Info($"CreateAppShortCut ExePath:{exepath}");
                    writer.WriteLine("[InternetShortcut]");
                    writer.WriteLine($"URL=file:///{exepath}");
                    writer.WriteLine("IconIndex=0");
                    string icon = exepath.Replace('\\', '/');
                    writer.WriteLine("IconFile=" + icon);
                }
            }
            catch (Exception e)
            {
                LogTool.Instance.Error($"CreateAppShortCut {e.ToString()}");
            }
        }
        public static void CreateDesktopShortcut()
        {
                if (System.IO.File.Exists(GameConfig.GameExeLnkPath))
                    System.IO.File.Delete(GameConfig.GameExeLnkPath);

                // 2. 生成快捷方式名称（去除扩展名）
                var shortcutName = GameConfig.AppName;

                // 3. 确定快捷方式保存路径（桌面目录）
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                var shortcutPath = Path.Combine(desktopPath, $"{shortcutName}.lnk");

                // 4. 创建WSH对象并生成快捷方式
                var shell = new WshShell();
                var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);


                // 5. 配置快捷方式属性
                string processName = Process.GetCurrentProcess().ProcessName + ".exe";
                string exepath = Path.Combine(Environment.CurrentDirectory, processName);
                shortcut.TargetPath = exepath;         // 目标文件路径
                shortcut.WorkingDirectory = Environment.CurrentDirectory; // 工作目录
                shortcut.Description = "点击我进入凡人修仙游戏";   // 可选：快捷方式描述
                shortcut.Save();
        }
    }
}
