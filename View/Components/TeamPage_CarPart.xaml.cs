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
    /// Interaction logic for TeamPage_CarPart.xaml
    /// </summary>
    public partial class TeamPage_CarPart : UserControl
    {
        public TeamPage_CarPart()
        {
            InitializeComponent();
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // DataGrid grid = sender as DataGrid;
            // var data = grid.CurrentItem as CarPart;
            //data.StatsUpdated();
        }
    }
}
