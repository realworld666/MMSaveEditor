using GalaSoft.MvvmLight.Ioc;
using MMSaveEditor.ViewModel;
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
            if (rulesList.SelectedItems.Count == 1)
            {
                var vm = SimpleIoc.Default.GetInstance<ChampionshipViewModel>();
                if (vm.ChampionshipData != null)
                {
                    vm.ChampionshipData.Rules.RemoveRule(rulesList.SelectedItem as PoliticalVote);
                }
            }
        }

        private void addRule_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewRuleWindow window = new NewRuleWindow();
            window.ShowDialog();

            if (window.ChosenRule != null)
            {
                var vm = SimpleIoc.Default.GetInstance<ChampionshipViewModel>();
                if (vm.ChampionshipData != null)
                {
                    vm.ChampionshipData.Rules.AddRule(window.ChosenRule);
                    vm.ChampionshipData.Rules.ActivateRules();
                    // refresh view model
                    vm.SetModel(vm.ChampionshipData);
                }
            }
        }

        private void addRuleNextYear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewRuleWindow window = new NewRuleWindow();
            window.ShowDialog();

            if (window.ChosenRule != null)
            {
                var vm = SimpleIoc.Default.GetInstance<ChampionshipViewModel>();
                if (vm.ChampionshipData != null)
                {
                    vm.ChampionshipData.NextYearsRules.AddRule(window.ChosenRule);
                    // refresh view model
                    vm.SetModel(vm.ChampionshipData);
                }
            }
        }
    }
}
