using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class PlayerViewModel : TeamPrincipalViewModelBase<Player>
    {
        public string PlayerTeamName => PersonData?.team.name;
    }
}
