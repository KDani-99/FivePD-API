using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;

namespace Mugging
{
    [CalloutProperties("Mugging", "FivePD", "1.1")]
    public class Mugging : FivePD.API.Callout
    {
        private readonly Random rnd = new Random();
        private Ped suspect, victim;

        public Mugging()
        {
            int distance = rnd.Next(200, 750);
            float offsetX = rnd.Next(-1 * distance, distance);
            float offsetY = rnd.Next(-1 * distance, distance);

            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(offsetX, offsetY, 0))));
            ShortName = "Mugging";
            CalloutDescription = "We've received a report of a mugging. Respond in code 3.";
            ResponseCode = 3;
            StartDistance = 50f;
        }

        public override async Task OnAccept()
        {
            InitBlip();
            suspect = await SpawnPed(FivePD.API.Utils.RandomUtils.GetRandomPed(), Location);
            victim = await SpawnPed(FivePD.API.Utils.RandomUtils.GetRandomPed(), suspect.GetOffsetPosition(new Vector3(2f, 4f, 0f)));

            keepTask(suspect);
            keepTask(victim);

            suspect.Weapons.Give(getRandomWeapon(), 1, true, true);
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            suspect.Task.FightAgainst(victim);

            int chance = rnd.Next(0, 10);
            // 40% that the victim will attack the suspect, otherwise run away
            if (chance >= 0 && chance <= 3)
            {
                // 50% that the victim has a weapon as well
                chance = rnd.Next(0, 10);
                if (chance >= 0 && chance <= 4) victim.Weapons.Give(getRandomWeapon(), 1, true, true);

                victim.Task.FightAgainst(suspect);
            }
            else victim.Task.ReactAndFlee(suspect);
        }

        private static void keepTask(Ped p)
        {
            p.BlockPermanentEvents = true;
            p.AlwaysKeepTask = true;
        }

        private WeaponHash getRandomWeapon()
        {
            List<WeaponHash> weapons = new List<WeaponHash>
            {
                WeaponHash.Bottle,
                WeaponHash.Crowbar,
                WeaponHash.Dagger,
                WeaponHash.Knife,
                WeaponHash.Hammer,
                WeaponHash.Wrench
            };
            return weapons[rnd.Next(weapons.Count)];
        }
    }
}
