using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Tools;

namespace Gamesstarter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += Application_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledExceptionEventHandler;
            base.OnStartup(e);
        }

        private void OnUnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e)
        {
            LogTool.Instance.Error(e.ExceptionObject.ToString());
            CommonTools.Exit();
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var text = e.Exception.Message + "_" + e.Exception.StackTrace.Replace('\r', '_').Replace('\n', '|');
            LogTool.Instance.Error(text);
            CommonTools.Exit();
        }
    }
}
