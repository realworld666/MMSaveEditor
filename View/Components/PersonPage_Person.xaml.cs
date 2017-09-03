using MMSaveEditor.ViewModel;
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
            Type contextType = DataContext.GetType();
            PropertyInfo teamName = contextType.GetProperty("TeamName", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (teamName != null && (teamName.GetValue(DataContext) == null || (string)teamName.GetValue(DataContext) == ""))
                return;

            if (!(this.DataContext is TeamPrincipalViewModel) && !(this.DataContext is PlayerViewModel))
            {
                TransferPerson dialog = new TransferPerson(this.DataContext);
                dialog.ShowDialog();
            }
            else
            {
                MethodInfo theMethod = contextType.GetMethod("_viewTeam", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                theMethod.Invoke(DataContext, null);
            }


        }
    }
}
