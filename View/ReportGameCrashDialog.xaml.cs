using System.Windows;
using Microsoft.Win32;
using System.IO;

namespace MMSaveEditor.View
{
    /// <summary>
    /// Interaction logic for ReportGameCrashDialog.xaml
    /// </summary>
    public partial class ReportGameCrashDialog : Window
    {
        private const string MMDir = @"steamapps\common\Motorsport Manager\MM_Data";

        public string LogPath { get; set; }

        public ReportGameCrashDialog()
        {
            InitializeComponent();
            LogPath = null;
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            string steamPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamPath", null);
            if (steamPath == null)
            {
                MessageBoxResult result = MessageBox.Show("Could not find the path to Steam on your computer. ", "No Steam", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            string mmPath = Path.Combine(steamPath, MMDir);
            if (Directory.Exists(mmPath))
            {
                string logPath = Path.Combine(mmPath, "output_log.txt");
                if (File.Exists(logPath))
                {
                    LogPath = logPath;
                    Close();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("There is no output log file in the game directory.", "No Log", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Could not find the game directory in your Steam library.", "No Game", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
    }
}
