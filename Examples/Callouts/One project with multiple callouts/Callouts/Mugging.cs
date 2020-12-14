using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;

namespace Callouts
{
    [CalloutProperties("Mugging", "FivePD", "1.0")]
    public class Mugging : FivePD.API.Callout
    {
        private Ped suspect, victim;

        public Mugging()
        {
            InitInfo(World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(Utils.GetRandomPosition())));
            ShortName = "Mugging";
            CalloutDescription = "";
            ResponseCode = 3;
            StartDistance = 120f;
        }

        public override async Task OnAccept()
        {
            InitBlip();

            suspect = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            victim = await SpawnPed(RandomUtils.GetRandomPed(), Location);
            suspect.KeepTask();
            victim.KeepTask();

            suspect.Weapons.Give(WeaponHash.Knife, 1, true, true);
        }

        public override void OnStart(Ped closest)
        {
            base.OnStart(closest);
            Tick += Action;
            suspect.Task.FightAgainst(victim, -1);
        }

        public override void OnCancelBefore()
        {
            Tick -= Action;
            base.OnCancelBefore();
        }

        private async Task Action()
        {
            Ped closest = (!victim.Exists() || victim.IsDead) ? suspect.GetClosestPedFromPedList(AssignedPlayers) : victim;
            if (closest == null || closest.IsDead) return;
            suspect.Task.ClearAll();
            if (World.GetDistance(closest.Position, suspect.Position) <= 20f)
            {
                suspect.Task.FightAgainst(closest);
            }
            await BaseScript.Delay(2000);
        }
    }
}