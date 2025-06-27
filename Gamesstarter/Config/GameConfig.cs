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
        public const string CHANNEL_LOGIN_URL_KU25 = "http://www.ku25.com/client/frxx";
        public const string CHANNEL_LOGIN_URL_37tang = "http://extra.37tang.com/url/weiduan.php?gn=frxx";
        public const string CHANNEL_LOGIN_URL_43u = "http://frxx.43u.com/client";
        public const string CHANNEL_LOGIN_URL_1912yx = "http://frxx.1912yx.com/client";
        public const string CHANNEL_LOGIN_URL_zixia = "https://apps.zixia.com/jlcqolhw/index.php";
        public const string CHANNEL_LOGIN_URL_52gg = "http://frxx.52gg.com/client";

        public const string CHANNEL_LOGIN_URL_1771wan = "http://frxx.1771wan.com/client";
        public const string CHANNEL_LOGIN_URL_yxa9 = "http://www.yxa9.com/client/frxx/index.php";
        public const string CHANNEL_LOGIN_URL_xingdie = "https://www.ufojoy.com/pc/index.phtml?game=frxx";
        public const string CHANNEL_LOGIN_URL_yiling = "https://pay.10hud.com/pay/wdpay/blgame/361";
        public const string CHANNEL_LOGIN_URL_45yx = "https://www.45yx.com/client/login/1254?w=590&h=480&t=2";

        public const string CHANNEL_LOGIN_URL_dy_1 = "http://play.no1yx.com/wd/wd-frxx/index.htm";
        public const string CHANNEL_LOGIN_URL_dy_2 = "http://www.30756.cn//client/common/index.php?";

        public const string CHANNEL_LOGIN_URL_8090 = "http://dlqxz.8090.com/frxx/login/index.php";
        public const string CHANNEL_LOGIN_URL_335wan = "https://web.28zhe.com/index/microgame/index?id=49";
        public const string CHANNEL_LOGIN_URL_shunwang = "https://gamesite.swjoy.com/embed/5852";
        public const string CHANNEL_LOGIN_URL_4yx = "http://www.youxilifang.com/min/frxx";

        public const string CHANNEL_LOGIN_URL_jiuhou = "http://api.9hou.com/api/client/?gid=1119";
        public const string CHANNEL_LOGIN_URL_flash = "";
        public const string CHANNEL_LOGIN_URL_aqy = "";
        public static string CHANNEL_LOGIN_URL{
            get {
                return CHANNEL_LOGIN_URL_jiuhou;
            }
        }
        /// <summary>
        /// 解压完是否删除压缩包
        /// </summary>
        public static bool DelZipAfterUnzip = true;
        public static string AppName = "凡人修仙";
        public static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "newAppInfo.json");
        public static string GameRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Frxx");
        public static string GameSavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Frxx/Win");
        public static string GameExe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Frxx/Win/Frxx.exe");
        public static string GameExeLnkPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), AppName + ".lnk");
        public static string LocaGameAppInfo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Frxx/Win/AppInfo.json");
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
