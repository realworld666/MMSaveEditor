using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.ViewModel;

namespace MMSaveEditor.View.Components
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
            DataGrid grid = traitList;
            //ObservableCollection<PersonalityTrait> traits = (ObservableCollection<PersonalityTrait>)grid.ItemsSource;
            //traits.Remove(grid.SelectedItem as PersonalityTrait);

            // HACK
            var vm = SimpleIoc.Default.GetInstance<DriverViewModel>();
            if (vm.PersonData != null && grid.SelectedItem != null)
            {
                vm.PersonData.PersonalityTraitController.RemovePersonalityTrait(grid.SelectedItem as PersonalityTrait);
                // refresh view model
                vm.SetModel(vm.PersonData);
            }
        }
    }
}
