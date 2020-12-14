using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace Callouts
{
    [CalloutProperties("Active shooters", "FivePD", "1.0")]
    public class ActiveShooters : FivePD.API.Callout
    {
        private Ped ped, ped2;
        private bool hasStarted;

        public ActiveShooters()
        {
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(Utils.GetRandomPosition(300,800))));
            ShortName = "Active shooters";
            CalloutDescription = "";
            ResponseCode = 3;
            StartDistance = 150f;
        }
        
        public override async Task OnAccept()
        {
            InitBlip();

            ped = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            ped.KeepTask();
            ped.Armor = 3000;
            ped2 = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            ped2.KeepTask();
            ped2.Armor = 3000;
            
            var weapons = new[]
            {
                WeaponHash.Pistol, 
                WeaponHash.MicroSMG, 
                WeaponHash.SawnOffShotgun, 
                WeaponHash.AssaultRifle,
                WeaponHash.SawnOffShotgun,
                WeaponHash.HeavySniper
            };
            ped.Weapons.Give(weapons[Utils.rnd.Next(weapons.Length)], int.MaxValue, true, true);
            ped2.Weapons.Give(weapons[Utils.rnd.Next(weapons.Length)], int.MaxValue, true, true);

            Tick += Action;
        }

        public override void OnStart(Ped closest)
        {
            base.OnStart(closest);
            hasStarted = true;
            ped.Task.ShootAt(closest);
            ped2.Task.ShootAt(closest);
        }
        
        public override void OnCancelBefore()
        {
            Tick -= Action;
            base.OnCancelBefore();
        }

        private async Task Action()
        {
            if (hasStarted)
            {
                if(ped.Exists() && ped.IsAlive) await DoTheAction(ped, AssignedPlayers);
                if(ped2.Exists() && ped2.IsAlive) await DoTheAction(ped2, AssignedPlayers);
            }
            else
            {
                if(ped.Exists() && ped.IsAlive) await DoTheAction(ped, null);
                if(ped2.Exists() && ped2.IsAlive) await DoTheAction(ped2, null);
            }
            await BaseScript.Delay(Utils.rnd.Next(1000, 4500));
        }

        private async Task DoTheAction(Ped ped, List<Ped> pedsToShoot)
        {
            Ped pedToShoot = pedsToShoot == null ? Utilities.GetClosestPed(ped) : ped.GetClosestPedFromPedList(pedsToShoot);
            if (pedToShoot == null || pedToShoot.IsDead) return;

            int chance = Utils.rnd.Next(10);
            if(chance >= 0 && chance <= 5)
            {
                ped.Task.ShootAt(pedToShoot);
            }
            else
            {
                Vector3 pos = ped.GetOffsetPosition(Utils.GetRandomPosition(5, 25));
                ped.Task.RunTo(pos);
                while (World.GetDistance(pos, ped.Position) >= 3f)
                {
                    await BaseScript.Delay(10);
                }
                ped.Task.ShootAt(pedToShoot);
            }
        }
    }
}