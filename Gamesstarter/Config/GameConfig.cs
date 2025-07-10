using System;
using System.IO;


namespace Gamesstarter
{
    public class Localver
    {
        public string version;
    }

    public class AppVer
    {
        public string version;
        public string url;
        public long size;
        public string md5;
    }
    public partial class GameConfig
    {
        /// <summary>
        /// 解压完是否删除压缩包
        /// </summary>
        public static bool DelZipAfterUnzip = true;
        public static string AppName = "凡人修仙";
        private static string GamePath
        {
            get
            {
                if (GameConfig.IsSW())
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                }
                else
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                }
            }

        }
        public static string GameRoot
        {
            get {
                return Path.Combine(GameConfig.GamePath, "Frxx");
            }
        }
        public static string GameSavePath
        {
            get {
                return Path.Combine(GameConfig.GamePath, "Frxx/Win");
            }
        }
        public static string GameExe {
            get
            {
                return Path.Combine(GameConfig.GamePath, "Frxx/Win/Frxx.exe");
            }
        }
        public static string GameExeLnkPath {
            get {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), AppName + ".lnk");
            }
        }
        public static string LocaGameAppInfo
        {
            get
            {
                return Path.Combine(GameConfig.GamePath, "Frxx/AppInfo.json");
            }

        }
        public static string GameAppInfo {

            get {
                return Path.Combine(GameConfig.GamePath, "Frxx/Win/AppInfo.json");
            }
        }
        public const string newAppVerUrl = "http://taigu-360-self-cdn.cyygame.cn/game/tgsw3/qq/packages/qk/newAppInfo.json";
        public const int ProgressOfStartUp = 10;
        public const int ProgressOfDLZip = 60;
        public const int ProgressOfUnzip = 30;
    }
    public class TIPS
    {
        public const string GET_VERSION_INFO = "正在获取版本信息...{0:f2}%";
        public const string IN_DOWNLOAD_ZIPFILE = "正在更新游戏版本...{0:f2}%";
        public const string IN_UNZIP = "正在解压文件...{0:f2}%";
        public const string START_GAME = "正在启动游戏...{0:f2}%";

    }
}
