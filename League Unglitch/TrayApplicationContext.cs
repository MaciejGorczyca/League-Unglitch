using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace League_Unglitch
{
    public class TrayApplicationContext : Form
    {
        private LeagueConnection lc;

        private NotifyIcon Icon;
        private ContextMenu trayMenu;
        private MenuItem leagueConnectMenuItem;
        private MenuItem leagueConnectStatusMenuItem;

        public TrayApplicationContext()
        {
            // Create a connection with LCU.
            lc = new LeagueConnection();

            // Create a simple tray menu.
            initializeContextMenu();

            // Create a tray icon.
            // Add menu to tray icon and show it.
            initializeTrayIcon();
        }

        private void initializeContextMenu()
        {
            trayMenu = new ContextMenu();

            trayMenu.MenuItems.Add("Reload UI (fix bugs) - built-in function method", KillAndRestartUx);
            trayMenu.MenuItems.Add("Reload UI (fix bugs) - kill league process method", KillLeagueProcess);
            trayMenu.MenuItems.Add("Close UI (background mode)", KillUx);
            trayMenu.MenuItems.Add("Restore UI (quickly open League)", LaunchUx);
            trayMenu.MenuItems.Add("Exit", OnExit);
        }

        private void initializeTrayIcon()
        {
            Icon = new NotifyIcon();
            Icon.Text = "League Unglitch";
            Icon.Icon = Properties.Resources.icon;
            Icon.ContextMenu = trayMenu;
            Icon.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            base.OnLoad(e);
        }

        private void KillAndRestartUx(object sender, EventArgs e)
        {
            ExecutePost("/riotclient/kill-and-restart-ux");
        }

        private void KillLeagueProcess(object sender, EventArgs e)
        {
            EndProcessTree("LeagueClientUxRender.exe");
        }

        private void KillUx(object sender, EventArgs e)
        {
            ExecutePost("/riotclient/kill-ux");
        }

        private void LaunchUx(object sender, EventArgs e)
        {
            ExecutePost("/riotclient/launch-ux");
        }

        private async void ExecutePost(String url)
        {
            try
            {
                await lc.Post(url, "");
            }
            catch (Exception e)
            {
                ShowErrorMessage();
            }
        }

        private void ShowErrorMessage()
        {
            MessageBox.Show("Client session not found");
        }

        private void OnExit(object sender, EventArgs e)
        {
            Icon.Visible = false;
            Application.Exit();
        }
        
        private void EndProcessTree(string imageName)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = $"/im {imageName} /f /t",
                CreateNoWindow = true,
                UseShellExecute = false
            }).WaitForExit();
        }

    }
}