using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.DataGrid;

namespace MMSaveEditor.View.TabPages
{
    /// <summary>
    /// Interaction logic for PersonPage_Trait.xaml
    /// </summary>
    public partial class PersonPage_Trait : UserControl
    {
        public PersonPage_Trait()
        {
            InitializeComponent();
        }

        private void removeTrait_Click(object sender, RoutedEventArgs e)
        {
            DataGridControl grid = traitList;
            ObservableCollection<PersonalityTrait> traits = (ObservableCollection<PersonalityTrait>)grid.ItemsSource;
            traits.Remove(grid.SelectedItem as PersonalityTrait);
        }

        private void addTrait_Click(object sender, RoutedEventArgs e)
        {
            AddTraitDialog dialog = new AddTraitDialog();
            dialog.ShowDialog();
        }
    }
}
