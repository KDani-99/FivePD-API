using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace Callouts
{
    [CalloutProperties("Unconscious person", "FivePD", "1.0")]
    public class UnconsciousPerson : FivePD.API.Callout
    {
        private Ped ped;
        
        public UnconsciousPerson()
        {
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(Utils.GetRandomPosition())));
            ShortName = "Unconscious person";
            CalloutDescription = "";
            ResponseCode = 3;
            StartDistance = 200f;
        }
        
        public override async Task OnAccept()
        {
            InitBlip();

            ped = await SpawnPed(RandomUtils.GetRandomPed(), Location);

            ped.KeepTask();
            ped.Kill();
        }
    }
}