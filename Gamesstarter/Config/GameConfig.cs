using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class GameConfig
    {
        /// <summary>
        /// 解压完是否删除压缩包
        /// </summary>
        public static bool DelZipAfterUnzip = true;
        public static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "newAppInfo.json");
        public static string GameRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Frxx");
        public static string GameSavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Frxx/Win");
        public static string GameExe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Frxx/Win/Frxx.exe");
        public static string LocaGameAppInfo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Frxx/Win/AppInfo.json");
        public const string newAppVerUrl = "http://taigu-360-self-cdn.cyygame.cn/game/tgsw3/qq/packages/qk/newAppInfo.json";
        public const int ProgressOfStartUp = 10;
        public const int ProgressOfDLZip = 60;
        public const int ProgressOfUnzip = 30;
    }
}
