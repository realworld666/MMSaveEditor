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
using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.ViewModel;

namespace MMSaveEditor.View.Components
{
    /// <summary>
    /// Interaction logic for PersonPage_TPStats.xaml
    /// </summary>
    public partial class PersonPage_Traits : UserControl
    {
        public event EventHandler<PersonalityTraitData> TraitAdded;

        public PersonPage_Traits()
        {
            InitializeComponent();
        }

        private void addTrait_Click(object sender, RoutedEventArgs e)
        {
            AddTraitDialog dialog = new AddTraitDialog();
            dialog.ShowDialog();
            // aggregate data and info
            var handler = TraitAdded;
            if (handler != null && dialog.SelectedData != null)
                handler.Invoke(this, dialog.SelectedData);

            // HACK
            var vm = SimpleIoc.Default.GetInstance<DriverViewModel>();
            vm.PersonData.addTrait(dialog.SelectedData);
            // refresh view model
            vm.SetModel(vm.PersonData);
        }
    }
}
