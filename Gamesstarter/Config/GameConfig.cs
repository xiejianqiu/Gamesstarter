﻿using System;
using System.IO;
using TinyJson;
using Tools;

namespace Gamesstarter
{
    public class CacheFile
    {
        public string dataPath;

        public CacheFile(string dataPath)
        {
            this.dataPath = dataPath;
        }
    }
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
        public static string AppCachePath
        {
            get {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Frxx");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        private static string CacheFile
        {
            get
            {
                return Path.Combine(AppCachePath, "cache.txt");
            }
        }
        /// <summary>
        /// 解压完是否删除压缩包
        /// </summary>
        public static bool DelZipAfterUnzip = true;
        public static string AppName = "凡人修仙";
        public const string GameFolderName = "Frxx";
        private static string GameDataPath
        {
            get
            {
                if (File.Exists(GameConfig.CacheFile))
                {
                    try
                    {
                        var jsonStr = File.ReadAllText(GameConfig.CacheFile, System.Text.Encoding.UTF8);
                        var cacheFile = jsonStr.FromJson<CacheFile>();
                        return cacheFile.dataPath;
                    }
                    catch (Exception e)
                    {
                        LogTool.Instance.Error(e.ToString());
                    }
                }
                {
                    var DataPath = string.Empty;
                    if (GameConfig.IsSW())
                    {
                        DataPath = Path.Combine(Environment.CurrentDirectory, GameFolderName);
                    }
                    else
                    {
                        DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), GameFolderName);
                    }

                    CacheFile file = new CacheFile(DataPath);
                    var json = file.ToJson();
                    File.WriteAllText(GameConfig.CacheFile, json, System.Text.Encoding.UTF8);
                    return DataPath;
                }
            }

        }
        public static string GameRoot
        {
            get {
                return GameConfig.GameDataPath;
            }
        }
        public static string GameSavePath
        {
            get {
                return Path.Combine(GameConfig.GameDataPath, "Win");
            }
        }
        public static string GameExe {
            get
            {
                return Path.Combine(GameConfig.GameDataPath, "Win/Frxx.exe");
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
                return Path.Combine(GameConfig.GameDataPath, "AppInfo.json");
            }

        }
        public static string GameAppInfo {

            get {
                return Path.Combine(GameConfig.GameDataPath, "Win/AppInfo.json");
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
