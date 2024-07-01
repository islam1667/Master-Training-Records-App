using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MasterTrainingRecordsApp
{
    public partial class UserControlHelp : UserControl
    {
        public UserControlHelp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets default browser file path, wrote by gpt
        /// </summary>
        /// <returns>File path of default browser</returns>
        static string GetDefaultBrowserPath()
        {
            string browserPath = string.Empty;
            try
            {
                using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
                {
                    if (userChoiceKey != null)
                    {
                        string progId = userChoiceKey.GetValue("ProgId").ToString();
                        using (RegistryKey browserKey = Registry.ClassesRoot.OpenSubKey(progId + @"\shell\open\command"))
                        {
                            if (browserKey != null)
                            {
                                browserPath = browserKey.GetValue(null).ToString().Split('"')[1];
                            }
                        }
                    }
                }
            }
            catch { }

            return browserPath;
        }

        /// <summary>
        /// Opens link in the browser, tags used for link fragment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Links_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}Help\\Help.html#{(sender as LinkLabel).Tag}".Replace(@"\", @"/");
            try
            {
                Process.Start(GetDefaultBrowserPath(), $"file:///{path}");
            }
            catch
            {
                MessageBox.Show("Browser not found.");
            }
        }
    }
}