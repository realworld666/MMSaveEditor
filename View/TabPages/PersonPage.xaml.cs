using System;
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

    }
}
