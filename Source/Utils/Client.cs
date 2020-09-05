using CitizenFX.Core;
using System.Dynamic;

namespace FivePD.API.Utils
{
    /// <summary>
    /// Handy class containing things related to 'the client'
    /// </summary>
    public abstract class Client
    {
        /// <summary>
        /// Get current ped
        /// </summary>
        public static Ped Ped => Game.PlayerPed;

        /// <summary>
        /// Get current player
        /// </summary>
        public static Player Player => Game.Player;

        /// <summary>
        /// Get current player data
        /// </summary>
        public static ExpandoObject PlayerData => Utilities.GetPlayerData.Invoke();

        /// <summary>
        /// Get current callout
        /// </summary>
        public static Callout Callout => Utilities.GetCurrentCallout();

        /// <summary>
        /// Get WorldZone currently in
        /// </summary>
        public static WorldZone WorldZone => Ped.Position.GetCurrentWorldZone();

        /// <summary>
        /// Get or set on duty status
        /// </summary>
        public static bool OnDuty
        {
            get => Utilities.IsPlayerOnDuty();
            set => Utilities.SetPlayerDuty(value);
        }

        /// <summary>
        /// Get is client currently on a traffic stop
        /// </summary>
        public static bool OnTrafficStop => Utilities.IsPlayerPerformingTrafficStop().Result;
    }
}
