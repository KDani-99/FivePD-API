using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using static CitizenFX.Core.Native.API;

namespace Callouts
{
    [CalloutProperties("Slow vehicle", "FivePD", "1.0")]
    public class SlowVehicle : FivePD.API.Callout
    {
        private Ped driver;
        private Vehicle vehicle;

        public SlowVehicle()
        {
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(Utils.GetRandomPosition())));
            ShortName = "Slow vehicle";
            CalloutDescription = "";
            ResponseCode = 2;
            StartDistance = 120f;
        }

        public override async Task OnAccept()
        {
            InitBlip();

            driver = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            vehicle = await SpawnVehicle(RandomUtils.GetRandomVehicle(), Location);
            
            driver.KeepTask();
            TaskVehicleDriveWander(driver.Handle, vehicle.Handle, 5f, 143);
        }
    }
}