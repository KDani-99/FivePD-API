using System.Threading.Tasks;
using FivePD.API;
using CitizenFX.Core;

namespace Plugin
{
    public class Plugin : FivePD.API.Plugin
    {
        internal Plugin()
        {
            Events.OnDutyStatusChange += OnDutyStatusChange;
        }

        private async Task OnDutyStatusChange(bool isOnDuty)
        {
            if (isOnDuty)
                Debug.WriteLine("The player is on duty"); 
            else
                Debug.WriteLine("The player is off duty");
        }

    }
}
