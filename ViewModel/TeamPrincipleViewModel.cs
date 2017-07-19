using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.ViewModel
{
    public class TeamPrincipleViewModel : PersonViewModel<TeamPrincipal>
    {
        public float RaceManagement
        {
            get
            {
                if( _personData != null )
                {
                    return _personData.stats.raceManagement;
                }
                else
                {
                    return 0f;
                }
            }
            set
            {
                _personData.stats.raceManagement = value;
            }
        }

        public float Financial
        {
            get
            {
                if( _personData != null )
                {
                    return _personData.stats.financial;
                }
                else
                {
                    return 0f;
                }
            }
            set
            {
                _personData.stats.financial = value;
            }
        }

        public float Loyalty
        {
            get
            {
                if( _personData != null )
                {
                    return _personData.stats.loyalty;
                }
                else
                {
                    return 0f;
                }
            }
            set
            {
                _personData.stats.loyalty = value;
            }
        }

        public float JobSecurity
        {
            get
            {
                if( _personData != null )
                {
                    return _personData.stats.jobSecurity;
                }
                else
                {
                    return 0f;
                }
            }
            set
            {
                _personData.stats.jobSecurity = value;
            }
        }
    }
}
