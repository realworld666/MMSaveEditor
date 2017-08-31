using System;
using System.Reflection;
using System.Windows.Controls;

namespace MMSaveEditor.View.Components
{
    /// <summary>
    /// Interaction logic for PersonPage.xaml
    /// </summary>
    public partial class PersonPage_Person : UserControl
    {
        public PersonPage_Person()
        {
            InitializeComponent();
        }

        private void transferButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if true
            TransferPerson dialog = new TransferPerson(this.DataContext);
            dialog.ShowDialog();
#else
            Type contextType = DataContext.GetType();
            MethodInfo theMethod = contextType.GetMethod("_viewTeam", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            theMethod.Invoke(DataContext, null);
#endif


        }
    }
}
