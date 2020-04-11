using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CalloutAPI;

namespace DomesticDispute
{
    [CalloutProperties("DomesticDispute", "NorthCam18", "1.0", Probability.Medium)]
    public class DomesticDispute : CalloutAPI.Callout
    {
        Ped suspect, victim;
        public DomesticDispute()
        {
            Random random = new Random();
            float offsetX = random.Next(100, 800);
            float offsetY = random.Next(100, 800);

            InitBase(World.GetNextPositionOnSidewalk(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            this.ShortName = "Domestic Dispute";
            this.CalloutDescription = "Neighbours have reported loud screaming and fighting at the residence! Things are heating up, get there quick!";
            this.ResponseCode = 3;
            this.StartDistance = 30f;
        }

        public async override Task Init()
        {
            OnAccept();

            suspect = await SpawnPed(GetRandomPed(), Location);
            victim = await SpawnPed(GetRandomPed(), Location);

            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
            victim.AlwaysKeepTask = true;
            victim.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            if (Game.Player.Character.Position.DistanceToSquared(Location) <= 10f)
            {
                Notify("~r~[SUSPECT] Well you never cleaned up the house when I told you to!");
                Wait(5000);
                Notify("~b~[VICTIM] Well you never let me go out with my friends! So I'm sorry, but I didn't think it was fair!");
                Wait(5000);
                Notify("~r~[SUSPECT] Oh my GOD! I'm tired of your shit!");
                Wait(1000);

                suspect.Task.FightAgainst(victim);
                victim.Task.ReactAndFlee(suspect);
            }
        }

        private void Notify(string message)
        {
            SetNotificationTextEntry("STRING");
            AddTextComponentString(message);
            DrawNotification(false, false);
        }
    }
}
