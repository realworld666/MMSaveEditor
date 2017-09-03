using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace MMSaveEditor.View.TabPages
{
    /// <summary>
    /// Interaction logic for TeamPage.xaml
    /// </summary>
    public partial class PersonPage : UserControl
    {
        public event EventHandler<Person> ListBoxUpdated;

        public PersonPage()
        {
            InitializeComponent();
        }

        private void OnChildListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // aggregate data and info
            var handler = ListBoxUpdated;
            if (handler != null && e.AddedItems.Count > 0)
            {
                handler.Invoke(this, e.AddedItems[0] as Person);
                ListBox listbox = sender as ListBox;
                listbox.ScrollIntoView(e.AddedItems[0]);
            }
        }

        bool sortAsc = false;
        private void sortNames_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            personList.Items.SortDescriptions.Clear();
            personList.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("lastName", sortAsc ? ListSortDirection.Descending : ListSortDirection.Ascending));
            sortAsc = !sortAsc;
        }
    }
}
