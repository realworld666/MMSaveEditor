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
using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.ViewModel;

namespace MMSaveEditor.View
{
    /// <summary>
    /// Interaction logic for AddTrackDialog.xaml
    /// </summary>
    public partial class AddTrackDialog : Window
    {
        public Circuit ChosenCircuit;

        public AddTrackDialog()
        {
            InitializeComponent();
        }

        public int ChosenWeek;

        private void addButton_Click(object sender, RoutedEventArgs arg)
        {
            var circuitVM = SimpleIoc.Default.GetInstance<ChampionshipViewModel>();
            if (circuitVM.RaceEvents.Any(e => e.week == weekNum.Value))
            {
                MessageBoxResult result = MessageBox.Show(string.Format("There is already a race scheduled for week {0}", weekNum.Value), "Invalid Week", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (trackList.SelectedItems.Count == 1)
            {
                ChosenCircuit = trackList.SelectedItem as Circuit;
                ChosenWeek = weekNum.Value.Value;
                Close();
            }
        }
    }
}
