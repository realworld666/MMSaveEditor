using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.ViewModel;
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
            //ObservableCollection<PersonalityTrait> traits = (ObservableCollection<PersonalityTrait>)grid.ItemsSource;
            //traits.Remove(grid.SelectedItem as PersonalityTrait);

            // HACK
            var vm = SimpleIoc.Default.GetInstance<DriverViewModel>();
            vm.PersonData.removeTrait(grid.SelectedItem as PersonalityTrait);
            // refresh view model
            vm.SetModel(vm.PersonData);
        }
    }
}
