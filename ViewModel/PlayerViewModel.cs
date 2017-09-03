using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class PlayerViewModel : TeamPrincipalViewModelBase<Player>
    {
        public override void SetModel(Player pPersonData)
        {
            base.SetModel(pPersonData);

            People = new ObservableCollection<Player>(new[] { pPersonData });
        }
    }
}
