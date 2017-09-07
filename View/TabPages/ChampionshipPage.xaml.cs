using System;
using System.Windows.Controls;

namespace MMSaveEditor.View.TabPages
{
    /// <summary>
    /// Interaction logic for ChampionshipPage .xaml
    /// </summary>
    public partial class ChampionshipPage : UserControl
    {
        public event EventHandler<Championship> ListBoxUpdated;

        public ChampionshipPage()
        {
            InitializeComponent();
        }

        private void OnChildListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // aggregate data and info
            var handler = ListBoxUpdated;
            if (handler != null && e.AddedItems.Count > 0)
                handler.Invoke(this, e.AddedItems[0] as Championship);
        }

        private void removeRule_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
