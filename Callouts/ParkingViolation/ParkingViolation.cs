using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CalloutAPI;

namespace ParkingViolation
{
    [CalloutProperties("ParkingViolation", "NorthCam18", "1.0", Probability.Low)]
    public class ParkingViolation : CalloutAPI.Callout
    {
        Vehicle vehicle;
        private string[] modelsList = new string[] { "akuma", "faggio", "blista", "issi", "f620", "jackal", "asea", "fugitive", "primo", "feltzer", "casco", "infernus", "buccaneer", "dukes", "baller", "beejayxl", "bison", "boxville", "burrito", "rumpo" };
        public ParkingViolation()
        {
            Random random = new Random();
            float offsetX = random.Next(100, 1000);
            float offsetY = random.Next(100, 1000);

            InitBase(World.GetNextPositionOnSidewalk(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            this.ShortName = "Parking Violation";
            this.CalloutDescription = "Caller reported a vehicle parked illegally, respond and take care of it!";
            this.ResponseCode = 1;
            this.StartDistance = 80f;
        }

        public async override Task Init()
        {
            OnAccept(15, BlipColor.Blue, BlipSprite.PersonalVehicleCar, 100);
            
            Random random = new Random();
            var selectedModel = modelsList[random.Next(modelsList.Length)];

            var selectedHash = (VehicleHash) GetHashKey(selectedModel);

            vehicle = await SpawnVehicle(selectedHash, Location);

            vehicle.IsPersistent = true;
            vehicle.IsEngineRunning = false;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            if (Game.Player.Character.Position.DistanceToSquared(Location) <= 10f)
            {
                DisplayHelpTextThisFrame("~b~Write the vehicle a citation, then tow it away!", false);
                Wait(5000);
            }
        }
    }
}
