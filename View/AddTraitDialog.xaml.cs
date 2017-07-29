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
    /// Interaction logic for AddTraitDialog.xaml
    /// </summary>
    public partial class AddTraitDialog : Window
    {
        public PersonalityTraitData SelectedData;

        public AddTraitDialog()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedData = traitList.SelectedItem as PersonalityTraitData;
            this.Close();
        }
    }
}
