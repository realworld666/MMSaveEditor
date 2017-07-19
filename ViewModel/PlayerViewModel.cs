using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class PlayerViewModel : PersonViewModel<Player>
    {
        private Player _player
        {
            get
            {
                return _personData as Player;
            }
        }
        public string PlayerTeamName
        {
            get
            {
                return _player?.team.name;
            }
        }
    }
}
