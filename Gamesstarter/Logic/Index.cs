using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using Tools;

namespace Gamesstarter
{
    /// <summary>
    /// 1.获取CDN上最新版本信息
    /// 2.和本地版本对比，如果比大就走版本更新流程
    /// 3.版本更新完后显示渠道选服界面
    /// </summary>
    public class Index
    {
        private static Index _Index;
        public static Index Inst {
            get {
                if (null == _Index)
                {
                    _Index = new Index();
                }
                return _Index;
            }
        }
        public event Action<int> DownloadProgressEvent;
        void UpdateProgerss(int progress)
        {
            DownloadProgressEvent?.Invoke(progress);
        }
        public void Start()
        {
            UpdateProgerss(GameConfig.ProgressOfStartUp);
            HttpUtil.DownloadFile(GameConfig.newAppVerUrl,OnGetNewAppVerInfo);
        }

        private void OnGetNewAppVerInfo(bool result, string jsonStr)
        {
            try
            {
                if (!result)
                {
                    MessageBox.Show("获取资源版本信息失败，请检查网络是否畅通!");
                    return;
                }
                bool NeedUpdate = false;
                var newAppVer = JsonConvert.DeserializeObject<AppVer>(jsonStr);
                if (File.Exists(GameConfig.LocaGameAppInfo))
                {
                    var localAppVer = JsonConvert.DeserializeObject<Localver>(File.ReadAllText(GameConfig.LocaGameAppInfo, Encoding.UTF8));
                    if (IsNewAppVersion(newAppVer.version, localAppVer.version))
                    {
                        NeedUpdate = true;
                    }
                }
                else
                {
                    NeedUpdate = true;
                }
                if (!NeedUpdate)
                {
                    ShowGameServerUI();
                }
                else
                {
                    
                    StartGameUpdate(newAppVer);
                }
            }
            catch (Exception e)
            {
                LogTool.Instance.Error(e.ToString());
                MessageBox.Show($"游戏更新出现异常，{e.ToString()}");
            }
        }
        /// <summary>
        /// 开始游戏更新
        /// </summary>
        void StartGameUpdate(AppVer newAppVer)
        {
            
            var savePath = $"{newAppVer.version}{newAppVer.md5}_{newAppVer.size}.zip";
            savePath = Path.Combine(GameConfig.GameRoot,savePath);
            if (File.Exists(savePath))
            {
                UnzipNewAppVers(savePath, GameConfig.GameSavePath);
                return;
            }
            //CommonTools.DelAndCreate(GameConfig.GameRoot);
            HttpUtil.DownBigFile(newAppVer.url, savePath, newAppVer.md5, OnNewGameVerDL, OnDLGameZipProgress);
        }
        /// <summary>
        /// 游戏新版本下载进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDLGameZipProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            var progress = GameConfig.ProgressOfStartUp + GameConfig.ProgressOfDLZip * (e.ProgressPercentage * 1f / 100);
            UpdateProgerss(Convert.ToInt16(progress));
        }
        /// <summary>
        /// App版本压缩包下载成功
        /// </summary>
        /// <param name="result"></param>
        /// <param name="savePath"></param>
        void OnNewGameVerDL(bool result, string savePath)
        {
            if (!result)
            {
                LogTool.Instance.Error("OnNewGameVerDL Fail");
                CommonTools.Exit();
                return;
            }
            UnzipNewAppVers(savePath, GameConfig.GameSavePath);
        }
        /// <summary>
        /// 将资源解压缩到游戏目录
        /// </summary>
        /// <param name="zipFile"></param>
        /// <param name="gamePath"></param>
        void UnzipNewAppVers(string zipFile, string gamePath)
        {
            Installer.UnZip(zipFile, gamePath, OnUnzipProgresssHandler, OnUnzipAppResult);
            //ShowGameServerUI();
        }
        void OnUnzipProgresssHandler(float progress)
        {
            UpdateProgerss(Convert.ToInt16(GameConfig.ProgressOfStartUp + GameConfig.ProgressOfDLZip + GameConfig.ProgressOfUnzip * progress));
        }
        void OnUnzipAppResult(bool result, string zipFile)
        {
            if (!result)
            {
                LogTool.Instance.Error("OnUnzipAppResult Fail");
                CommonTools.Exit();
                return;
            }
            if (GameConfig.DelZipAfterUnzip)
            {
                File.Delete(zipFile);
            }
            ShowGameServerUI();
        }
        /// <summary>
        /// 显示渠道区服界面
        /// </summary>
        void ShowGameServerUI()
        {
            CommonTools.RunExe(GameConfig.GameExe, "platform=52gg server_num=889 uid=52gg_630690");
            CommonTools.Exit();
        }
        bool IsNewAppVersion(string oldVersion, string newVersion)
        {
            return VersionCheck(oldVersion, newVersion, 0) || VersionCheck(oldVersion, newVersion, 1) || VersionCheck(oldVersion, newVersion, 2) || VersionCheck(oldVersion, newVersion, 3);
        }
        bool VersionCheck(string oldVersion, string newVersion, int nIdx)
        {

            bool bRet = false;

            string[] vecOld = oldVersion.Split('.');
            string[] vecNew = newVersion.Split('.');
            try
            {
                if (nIdx < vecOld.Length && nIdx < vecNew.Length)
                {
                    if (int.Parse(vecNew[nIdx]) > int.Parse(vecOld[nIdx]))
                    {
                        bRet = true;
                    }
                }
            }
            catch (Exception e)
            {
                var msg = string.Format("oldVer:{0}, newVer:{1}, idx:{2}", oldVersion, newVersion, nIdx);
                LogTool.Instance.Error(msg);
            }
            return bRet;
        }
    }
}
