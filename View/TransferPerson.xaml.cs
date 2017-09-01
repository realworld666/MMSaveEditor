using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for TransferPerson.xaml
    /// </summary>
    public partial class TransferPerson : Window, INotifyPropertyChanged
    {
        public bool CanCompleteTransfer
        {
            get
            {
                if (DataContext != null)
                {
                    Type contextType = DataContext.GetType();
                    PropertyInfo property = contextType.GetProperty("CurrentTeam", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    Team t = (Team)property.GetValue(DataContext);

                    if (availableTeams.SelectedItems.Count == 1 && t != availableTeams.SelectedItems[0] &&
                        replaceList.SelectedItems.Count == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public List<Person> ReplaceList
        {
            get
            {
                if (DataContext != null)
                {
                    Type contextType = DataContext.GetType();
                    MethodInfo theMethod = contextType.GetMethod("GetPeopleFromTeam", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    return (List<Person>)theMethod.Invoke(DataContext, new Object[] { availableTeams.SelectedItems[0] as Team });
                }
                return null;
            }
        }

        public TransferPerson()
        {
            InitializeComponent();
        }

        public TransferPerson(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ViewTeam_Click(object sender, RoutedEventArgs e)
        {
            Type contextType = DataContext.GetType();
            MethodInfo theMethod = contextType.GetMethod("_viewTeam", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            theMethod.Invoke(DataContext, null);
            Close();
        }

        private void DoTransfer_Click(object sender, RoutedEventArgs e)
        {
            Type contextType = DataContext.GetType();
            PropertyInfo property = contextType.GetProperty("PersonData", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Person p = (Person)property.GetValue(DataContext);

            p.Contract.Editor_SetTeam(availableTeams.SelectedItem as Team, replaceList.SelectedItem as Person);

            // refresh view model
            MethodInfo theMethod = contextType.GetMethod("SetModel", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            theMethod.Invoke(DataContext, new object[] { p });

            Close();
        }

        private void availableTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.NotifyPropertyChanged("CanCompleteTransfer");
            this.NotifyPropertyChanged("ReplaceList");
        }

        private void replaceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.NotifyPropertyChanged("CanCompleteTransfer");
        }
    }
}
