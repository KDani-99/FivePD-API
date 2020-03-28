using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CalloutAPI;

namespace Mugging
{
    [CalloutProperties("Mugging", "FivePD", "1.0", Probability.Medium)]
    public class Mugging : CalloutAPI.Callout
    {
        Ped suspect, victim;
        public Mugging()
        {
            Random rnd = new Random();
            float offsetX = rnd.Next(100, 700);
            float offsetY = rnd.Next(100, 700);

            InitBase(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));

            ShortName = "Mugging";
            CalloutDescription = "We've got a call that somebody is mugging a person. Respond immediately!";
            ResponseCode = 3;
            StartDistance = 120f;
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

            suspect.Weapons.Give(WeaponHash.Knife, 1, true, true);
            suspect.Task.FightAgainst(victim);
            victim.Task.ReactAndFlee(suspect);
        }
    }
}
