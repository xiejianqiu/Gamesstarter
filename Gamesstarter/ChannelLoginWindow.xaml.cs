using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tools;

namespace Gamesstarter
{
    /// <summary>
    /// ChannelLoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChannelLoginWindow : Window
    {
        public ChannelLoginWindow()
        {
            LogTool.Instance.Info($"ChannelLoginWindow");
            InitializeComponent();
            this.WBroswer.Navigated += OnWebBrowserNavigated;
            LoadWebPage(GameConfig.CHANNEL_LOGIN_URL);
        }

        private void OnWebBrowserNavigated(object sender, NavigationEventArgs avg)
        {
            try
            {
                string[] array = avg.Uri.Query.TrimStart(new char[]
                {
                '?'
                }).Split(new char[]
                {
                '&'
                }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string[] array3 = array2[i].Split(new char[]
                    {
                        '='
                    });
                    if (array3.Length == 2)
                    {
                        dictionary[array3[0]] = array3[1];
                    }
                }
                LogTool.Instance.Info($"OnWebBrowserNavigated: {avg.Uri}");
                if (dictionary.ContainsKey("server_num") && dictionary.ContainsKey("uid") && dictionary.ContainsKey("platform"))
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (var kv in dictionary)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append($" ");
                        }
                        builder.Append($"{kv.Key}={kv.Value}");
                    }
                    var args = builder.ToString();
                    CommonTools.RunExe(GameConfig.GameExe, args);
                    CommonTools.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception occurred: " + ex.Message, "Exception Alert");
            }
        }
        private void LoadWebPage(string url)
        {
            this.WBroswer.Navigate(new Uri(url));
        }
    }
}
