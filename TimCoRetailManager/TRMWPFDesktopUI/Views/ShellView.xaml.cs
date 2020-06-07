using MahApps.Metro.Controls;
using Squirrel;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TRMWPFDesktopUI.Models;

namespace TRMWPFDesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
      
        public ShellView()
        {
            InitializeComponent();
#if !DEBUG

            var updateTask = CheckForUpdates();
            updateTask.Start();
#endif
        }
        public static Task CheckForUpdates()
        {
           
            bool cancel = false;
            return new Task(() =>
            {
                while (!cancel)
                {
                   CheckAndApplyUpdates(cancel);
                    Thread.Sleep(3);
                }
            }, TaskCreationOptions.LongRunning);
        }
        private static string GetReleases(string username, string repoName)
        {
            const string GITHUB_API = "https://api.github.com/repos/{0}/{1}/releases";
            WebClient webClient = new WebClient();
            // Added user agent
            webClient.Headers.Add("User-Agent", "TimCoRetailManager");
            Uri uri = new Uri(string.Format(GITHUB_API, username, repoName));
            string releases = webClient.DownloadString(uri);
            return releases;
        }
        public static void CheckAndApplyUpdates(bool cancel)
        {
             
            bool shouldRestart = false;

            GitHubJson[] gitHubJson = GitHubJson.FromJson(GetReleases("Psanyi89", "My_Softwares"));
            var downloadurl = gitHubJson.Select(x => x.Assets[0].BrowserDownloadUrl).FirstOrDefault().ToString().Replace("/RELEASES","");
            using (UpdateManager manager = new UpdateManager(downloadurl))
            {

                var updateInfo = manager.CheckForUpdate().Result;

                if (updateInfo.CurrentlyInstalledVersion==null || updateInfo.CurrentlyInstalledVersion.Version < updateInfo.FutureReleaseEntry.Version)
                {

                    var firstMessageBox = MessageBox.Show(
                    $"New version available {updateInfo.FutureReleaseEntry.Version}!\n Would you like to download it?"
                                       , "Update available", 
                   MessageBoxButton.YesNo
                                       );
                    if (firstMessageBox == MessageBoxResult.Yes)
                    {
                        shouldRestart = true;

                        manager.DownloadReleases(updateInfo.ReleasesToApply).Wait();
                        
                        manager.ApplyReleases(updateInfo).Wait();
                       
                        manager.CreateUninstallerRegistryEntry().Wait();

                    }
                    else if (firstMessageBox ==MessageBoxResult.No)
                    {
                        cancel = true;
                    }
                }
            }

            if (shouldRestart)
            {

                if (MessageBox.Show("Would you like to restart application to run the updated version?"
                                           , "Update Finished!"
                                           ,  MessageBoxButton.YesNo
                                           ) == MessageBoxResult.Yes)
                {
                    UpdateManager.RestartApp();
                }

            }
        }
    }
}
