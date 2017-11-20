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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MMSaveEditor.View.Components
{
    /// <summary>
    /// Interaction logic for PersonPage_TPStats.xaml
    /// </summary>
    public partial class PersonPage_DriverStats : UserControl
    {
        public PersonPage_DriverStats()
        {
            InitializeComponent();
        }

        private void stats_Initialized(object sender, EventArgs e)
        {
            var stats = sender as DataGrid;
            var props = typeof(DriverStats).GetProperties();
            foreach (var prop in props)
            {
                stats.Columns.Add(new DataGridTextColumn { Header = prop.Name });
            }

        }
    }
}
