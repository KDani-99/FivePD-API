using System;
using CitizenFX.Core;
using System.Dynamic;
using System.Threading.Tasks;

namespace FivePD.API.Utils
{
    public abstract class Client
    {
        public static Ped Ped => Game.PlayerPed;

        public static ExpandoObject PedData
        {
            get
            {
                return Utilities.GetPedData(Ped.NetworkId).Result;
            }

            set
            {
                Utilities.SetPedData(Ped.NetworkId, value);
            }
        }

        public static Player Player => Game.Player;

        public static Func<ExpandoObject> PlayerData => Utilities.GetPlayerData;

        public static Callout Callout => Utilities.GetCurrentCallout();

        public static WorldZone WorldZone => Ped.Position.GetCurrentWorldZone();

        public static bool OnDuty
        {
            get
            {
                return Utilities.IsPlayerOnDuty();
            }

            set
            {
                Utilities.SetPlayerDuty(value);
            }
        }

        public static bool OnTrafficStop => Utilities.IsPlayerPerformingTrafficStop().Result;

        public static ExpandoObject VehicleData
        {
            get
            {
                if (Ped.CurrentVehicle == null)
                {
                    return null;
                }
                else
                {
                    return Utilities.GetVehicleData(Ped.CurrentVehicle.NetworkId).Result;
                }
            }

            set
            {
                if (Ped.CurrentVehicle != null)
                {
                    Utilities.SetVehicleData(Ped.CurrentVehicle.NetworkId, value);
                }
            }
        }
    }
}
