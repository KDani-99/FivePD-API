using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace FivePD.API
{
    public static class Utilities
    {
        public enum Services
        {
            Ambulance = 0,
            AirAmbulance = 1,
            FireDept = 2,
            Coroner = 3,
            AnimalControl = 4,
            TowTruck = 5,
            Mechanic = 6,
            PrisonTransport = 7
        }

        public delegate void ForceCalloutDelegate(string guid);
        /// <summary>
        /// Forces a callout with the given GUID or name (eg. Trespassing).
        /// </summary>
        public static ForceCalloutDelegate ForceCallout;

        public delegate Callout GetCurrentCalloutDelegate();
        /// <summary>
        /// Returns the current ongoing callout.
        /// </summary>
        public static GetCurrentCalloutDelegate GetCurrentCallout;

        public delegate List<string> GetCalloutsDelegate();
        /// <summary>
        /// Returns a List with every enabled callout.
        /// </summary>
        public static GetCalloutsDelegate GetCallouts;

        #region Vehicle
        public delegate Task<ExpandoObject> GetVehicleDataDelegate(int networkID);
        public static GetVehicleDataDelegate GetVehicleData;

        public delegate void SetVehicleDataDelegate(int vehicle,ExpandoObject data);
        public static SetVehicleDataDelegate SetVehicleData;

        public delegate void ExcludeVehicleFromTrafficStopDelegate(int networkID, bool exclude);
        public static ExcludeVehicleFromTrafficStopDelegate ExcludeVehicleFromTrafficStop;
        #endregion

        #region Services
        public delegate void RequestServiceDelegate(Services service);
        /// <summary>
        /// Requests a service to a given position.
        /// </summary>
        public static RequestServiceDelegate RequestService;
        public delegate void CancelServiceDelegate(Services service);
        /// <summary>
        /// Cancels the last requested service (by service type).
        /// </summary>
        public static CancelServiceDelegate CancelService;

        #endregion

        #region Traffic Stop
        public delegate Task<bool> IsPlayerPerformingTrafficStopDelegate();
        public static IsPlayerPerformingTrafficStopDelegate IsPlayerPerformingTrafficStop;

        public delegate Task<Vehicle> GetVehicleFromTrafficStopDelegate();
        public static GetVehicleFromTrafficStopDelegate GetVehicleFromTrafficStop;
        #endregion

        #region Backups
        public enum Backups
        {
            Code1 = 1,
            Code2 = 2,
            Code3 = 3,
            Code99 = 99
        }
        public delegate void RequestBackupDelegate(Backups backup);
        public static RequestBackupDelegate RequestBackup;

        public delegate void CancelBackupDelegate();
        public static CancelBackupDelegate CancelBackup;
        #endregion

        #region Ped
        public delegate Task<ExpandoObject> GetPedDataDelegate(int NetworkID);
        /// <summary>
        /// Retrieve internal FivePD information about a specific pedestrian.
        /// Use the method inside of the <see cref="Init"/> or <see cref="OnStart"/> methods or in an external script.<br /><br />
        /// The following properties can be accessed:<br />
        /// 	- Firstname (string) <br />
        /// 	- Lastname (string) <br />
        /// 	- Warrant (string) <br />
        /// 	- License (string) <br />
        /// 	- DOB (string) -> format: <c>mm/dd/yyyy</c><br />
        /// 	- AlcoholLevel (double) <br /> 
        /// 	- DrugsUsed (bool []) -> 0 = Meth, 1 = Cocaine, 2 = Marijuana<br />
        /// 	- Gender (string) <br />
        /// 	- Age (int) <br />
        /// 	- Address (string) <br />
        /// 	- Items (List&lt;dynamic&gt;) <br />
        /// 	- Violations (List&lt;dynamic&gt;)
        /// <example>
        /// <code>dynamic myData = await GetPedData(myPed.NetworkId);</code>
        /// </example>
        /// </summary>
        /// <param name="NetworkID">The network ID of the pedestrian that you're trying to get the data from.</param>
        /// <returns></returns>
        public static GetPedDataDelegate GetPedData;

        public delegate void SetPedDelegate(int NetworkID, ExpandoObject PedData);
        /// <summary>
        /// Set information for a specific pedestrian.
        /// Use the method inside of the <see cref="Init"/> or <see cref="OnStart"/> methods or in an external script.<br /><br />
        /// The following properties can be set in the <c>PedData</c>:<br />
        ///     - firstname (string)<br />
        ///     - lastname (string)<br />
        ///     - alcoholLevel (double)<br />
        ///     - drugsUsed (bool[]) -> 0 = Meth, 1 = Cocaine, 2 = Marijuana<br />
        ///     - items (List&lt;object&gt;)
        /// <example>
        /// <code>
        /// dynamic myData = new ExpandoObject();<br />
        /// myData.firstname = "John";<br />
        /// myData.lastname = "Doe";<br />
        /// myData.alcoholLevel = 0.20;<br />
        /// myData.drugsUsed = new bool[] { false, false, true }; // High on marijuana<br />
        /// myData.items = new List&lt;object&gt; { new { Name = "knife", IsIllegal = true } }; <br />
        /// SetPedDelegate(myPed.NetworkId, myData);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="NetworkID">The network id of the pedestrian.</param>
        /// <param name="PedData">The dynamic object with the values to set. The values are case sensitive.</param>
        /// <seealso cref="SetPedDelegate"/>
        public static SetPedDelegate SetPedData;
        #endregion

        #region Player
        /// <summary>
        /// Call GetPlayerData() if you want to access the PlayerData object. 
        /// Please note that you cannot call this method in the constructor of your callout. 
        /// Use the method inside of the <see cref="Init"/> or <see cref="OnStart"/> methods.<br /><br />
        /// The following properties can be accessed in the ExpandoObject:<br />
        ///      - DisplayName (string) <br />
        ///      - Callsign (string) <br />
        ///      - Department (string)  <br />
        ///      - DepartmentID (int)  <br />
        ///      - XP (int)
        /// </summary>
        public static Func<ExpandoObject> GetPlayerData;

        public delegate bool IsPlayerOnDutyDelegate();
        public static IsPlayerOnDutyDelegate IsPlayerOnDuty;

        public delegate void SetPlayerDutyDelegate(bool onDuty);
        public static SetPlayerDutyDelegate SetPlayerDuty;

        /// <summary>
        /// Returns the closest ped.
        /// </summary>
        public static Ped GetClosestPed(Ped p)
        {
            Ped[] all = World.GetAllPeds();
            if (all.Length == 0)
                return null;
            float closest = float.MaxValue;
            Ped close = null;
            foreach (Ped ped in all)
            {
                if (Game.PlayerPed == ped)
                    continue;
                float distance = World.GetDistance(ped.Position, p.Position);
                if (distance < closest)
                {
                    close = ped;
                    closest = distance;
                }
            };
            return close;
        }
        /// <summary>
        /// Returns a random position in the player's department area.
        /// </summary>
        public static Func<Vector3> GetRandomPosInPlayerDepartmentArea;
        #endregion
    }
}
