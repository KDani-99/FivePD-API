using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CalloutAPI;

namespace DefaultCallout
{
    [CalloutProperties("Trespassing","FivePD", "1.0", Probability.Medium)]
    public class DefaultCallout : CalloutAPI.Callout
    {

        public DefaultCallout()
        {
            /* Called when the callout is displayed */
            /* How far the callout should be? let's say random by default, you can change it */

            /* NOTE: Do not ever place spawning here!! It'll be added to the callout queue, so the constructor will be called */

            /* Randomize callout distance */
            /* InitBase() must be called! */
            this.InitBase(new Vector3(-935,196,67));
            
            /* Give information about your callout */
            this.ShortName = "DefaultCallout";
            this.CalloutDescription = "Text";
            this.ResponseCode = 3;

            /* How close the player needs to be to start the action (OnStart())*/
            this.StartDistance = 20f; // 30 feet? metres? unit...
        }
        public async override Task Init()
        {
            /* Called when the callout is accepted */
            /* Blip spawn happens in base.OnAccept() - must be called */
            this.OnAccept();

            /* This is the method where you should spawn the peds,vehicles, etc. */
            /* Use the SpawnPed or SpawnVehicle method to get a properly networked ped (react to other players) */
            /* Eg. await SpawnPed(PedHash.ChinGoonCutscene, this.Location,12); - Must be awaited*/

        }
        public override void OnStart(Ped player)
        {
            /* Called when a player gets in range */

            base.OnStart(player); // -> to remove the blip from the map (yellow circle by default). Must be called
        }
    }
}
