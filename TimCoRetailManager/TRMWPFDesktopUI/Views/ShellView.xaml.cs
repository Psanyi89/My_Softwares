using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Squirrel;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

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
            CheckForUpdates();
        }
        private async Task CheckForUpdates()
        {
                    MetroDialogSettings metroDialogSettings = new MetroDialogSettings
                    {
                        AffirmativeButtonText = "Yes",
                        NegativeButtonText = "No",
                        DialogTitleFontSize=32,                         
                          DefaultButtonFocus=MessageDialogResult.Affirmative,
                           DialogMessageFontSize=18
                    };
          
             UpdateInfo updateInfo;
            MessageDialogResult firstMessageBox=MessageDialogResult.Negative;
            using (WebClient client= new WebClient())
            {
                using (var manager = new UpdateManager("https://api.github.com/repos/Psanyi89/My_Softwares/releases/latest"))
                {
                    updateInfo = await manager.CheckForUpdate();
                    if (updateInfo.ReleasesToApply.Any())
                    {
                        var newversion = updateInfo.FutureReleaseEntry.Version;
                        firstMessageBox = await this.ShowMessageAsync("Update available",
                       $"New version available {newversion}!\n Would you like to download it?"
                                          , MessageDialogStyle.AffirmativeAndNegative
                                          , metroDialogSettings);
                        if (firstMessageBox == MessageDialogResult.Affirmative)
                        {
                            var darkwindow = new Window()
                            {
                                Background = Brushes.Black,
                                Opacity = 0.4,
                                AllowsTransparency = true,
                                WindowStyle = WindowStyle.None,
                                WindowState = WindowState.Maximized,
                                Topmost = true
                            };
                            darkwindow.Show();
                            Popup popup = new Popup
                            {
                                Placement = PlacementMode.Center,
                                PlacementTarget = this,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                AllowsTransparency = true,
                                Opacity = 0.5
                            };
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = "Downloading... ";
                            textBlock.FontWeight = FontWeights.Bold;
                            textBlock.FontSize = 24;
                            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Background = Brushes.Transparent;
                            stackPanel.Opacity = 0.5;
                            stackPanel.Orientation = Orientation.Vertical;
                            popup.Child = stackPanel;
                            stackPanel.Children.Add(textBlock);
                            ProgressRing progressBar = new ProgressRing();
                            progressBar.Height = 300;
                            progressBar.Width = 300;
                            progressBar.IsActive = true;
                            stackPanel.Children.Add(progressBar);
                            popup.IsOpen = true;
                            await manager.DownloadReleases(updateInfo.ReleasesToApply);
                            textBlock.Text = "Beginning to apply release..";
                            await manager.ApplyReleases(updateInfo);
                            textBlock.Text = "Creating uninstaller...";
                            await manager.CreateUninstallerRegistryEntry();
                            textBlock.Text = "All Done!";
                            popup.IsOpen = false;
                            darkwindow.Close();
                        }
                    }
                }
            }
            if (updateInfo.ReleasesToApply.Any())
            {

            if (firstMessageBox == MessageDialogResult.Affirmative && await this.ShowMessageAsync("Update Finished!"
                                       , "Would you like to restart application to run the updated version?"
                                       ,  MessageDialogStyle.AffirmativeAndNegative
                                       , metroDialogSettings) == MessageDialogResult.Affirmative)
            {
                UpdateManager.RestartApp();
            }
            }
        }
    }
}
