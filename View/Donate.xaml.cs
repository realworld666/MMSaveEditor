using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MMSaveEditor.View
{
    /// <summary>
    /// Interaction logic for Donate.xaml
    /// </summary>
    public partial class Donate : Window
    {
        public Donate()
        {
            InitializeComponent();
        }

        private void donate2_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://paypal.me/realworld666/2");
            Close();
            MMSaveEditor.Properties.Settings.Default.HasDonated = true;
            MMSaveEditor.Properties.Settings.Default.Save();
        }

        private void donate5_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://paypal.me/realworld666/5");
            Close();
            MMSaveEditor.Properties.Settings.Default.HasDonated = true;
            MMSaveEditor.Properties.Settings.Default.Save();
        }

        private void donateCustom_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://paypal.me/realworld666");
            Close();
            MMSaveEditor.Properties.Settings.Default.HasDonated = true;
            MMSaveEditor.Properties.Settings.Default.Save();
        }
    }
}
