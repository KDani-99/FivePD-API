using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using System.Linq;
using FivePD.API.Utils;

namespace FivePD.API
{
    /// <summary>
    /// FivePD Utilities class
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// An enum of FivePD services
        /// </summary>
        public enum Services
        {
            /// <summary>
            /// Ambulance service
            /// </summary>
            Ambulance = 0,
            /// <summary>
            /// Air ambulance service
            /// </summary>
            AirAmbulance = 1,
            /// <summary>
            /// Fire department service
            /// </summary>
            FireDept = 2,
            /// <summary>
            /// Coroner service
            /// </summary>
            Coroner = 3,
            /// <summary>
            /// Animal control service
            /// </summary>
            AnimalControl = 4,
            /// <summary>
            /// Tow truck service
            /// </summary>
            TowTruck = 5,
            /// <summary>
            /// Mechanic service
            /// </summary>
            Mechanic = 6,
            /// <summary>
            /// Prison transport service
            /// </summary>
            PrisonTransport = 7
        }

        /// <summary>
        /// A list of PedHashes that do not work with FivePD.
        /// </summary>
        public static PedHash[] UnsupportedPedHashes { get; } =
        {
            PedHash.Cop01SFY,
            PedHash.Cop01SMY,
            PedHash.Sheriff01SFY,
            PedHash.Sheriff01SMY,
            PedHash.Humpback,
            PedHash.Dolphin,
            PedHash.KillerWhale,
            PedHash.Fish,
            PedHash.HammerShark,
            PedHash.TigerShark,
            PedHash.Boar,
            PedHash.Cat,
            PedHash.ChickenHawk,
            PedHash.Chimp,
            PedHash.Coyote,
            PedHash.Cow,
            PedHash.Deer,
            PedHash.Pig,
            PedHash.Rabbit,
            PedHash.Crow,
            PedHash.Cormorant,
            PedHash.Husky,
            PedHash.Rottweiler,
            PedHash.Pug,
            PedHash.Poodle,
            PedHash.Retriever,
            PedHash.Seagull,
            PedHash.Pigeon,
            PedHash.MountainLion,
            PedHash.BradCadaverCutscene,
            PedHash.Chop,
            PedHash.Hen,
            PedHash.JohnnyKlebitz,
            PedHash.LamarDavisCutscene,
            PedHash.MagentaCutscene,
            PedHash.Marston01,
            PedHash.Misty01,
            PedHash.MovAlien01,
            PedHash.MoviePremFemaleCutscene,
            PedHash.MoviePremMaleCutscene,
            PedHash.MrsPhillipsCutscene,
            PedHash.MrKCutscene,
            PedHash.NataliaCutscene,
            PedHash.NigelCutscene,
            PedHash.NervousRonCutscene,
            PedHash.Niko01,
            PedHash.PaigeCutscene,
            PedHash.OscarCutscene,
            PedHash.OrtegaCutscene,
            PedHash.OrleansCutscene,
            PedHash.Orleans,
            PedHash.Pogo01,
            PedHash.Rat,
            PedHash.Rhesus,
            PedHash.Stingray,
            PedHash.SteveHainsCutscene,
            PedHash.Westy
        };
        /// <summary>
        /// EnableModDelegate is a method reference with zero parameter
        /// </summary>
        public delegate void EnableModDelegate();
        /// <summary>
        /// <para>Enables the mod for the local player. Can be useful for integrating other resources.</para>
        /// <para>Should only be used in Plugins.</para>
        /// <para>Can be called dynamically. (Allows you to toggle)</para>
        /// </summary>
        public static EnableModDelegate EnableMod;
        /// <summary>
        /// DisableModDelegate is a method reference with zero parameter
        /// </summary>
        public delegate void DisableModDelegate();
        /// <summary>
        /// <para>Disables the mod for the local player. Can be useful for integrating other resources.</para>
        /// <para> Should only be used in Plugins.</para>
        /// <para>Can be called dynamically. (Allows you to toggle)</para>
        /// </summary>
        public static DisableModDelegate DisableMod;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        public delegate void ForceCalloutDelegate(string guid);
        /// <summary>
        /// Forces a callout with the given GUID or name (eg. Trespassing).
        /// </summary>
        public static ForceCalloutDelegate ForceCallout;
        /// <summary>
        /// GetCurrentCalloutDelegate is a method reference with zero parameter and with return type of `Callout`
        /// </summary>
        /// <returns>Callout object</returns>
        public delegate Callout GetCurrentCalloutDelegate();
        /// <summary>
        /// Returns the current ongoing callout.
        /// </summary>
        public static GetCurrentCalloutDelegate GetCurrentCallout;
        /// <summary>
        /// GetCalloutsDelegate is a method reference with zero parameter and with return type of `List`
        /// </summary>
        /// <returns>A list of callout names</returns>
        public delegate List<string> GetCalloutsDelegate();
        /// <summary>
        /// Returns a List with every enabled callout.
        /// </summary>
        public static GetCalloutsDelegate GetCallouts;

        #region Vehicle
        /// <summary>
        ///  SetVehicleDataDelegate is a method reference with an int (vehicle) and VehicleData (data) parameters and with return type of `Task`
        /// </summary>
        /// <param name="networkID">The network id of the vehicle</param>
        /// <returns>(awaitable) VehicleData object</returns>
        public delegate Task<VehicleData> GetVehicleDataDelegate(int networkID);
        /// <summary>
        /// Returns a VehicleData object that is associated with the given vehicle (net id)
        /// </summary>
        public static GetVehicleDataDelegate GetVehicleData;

        /// <summary>
        /// SetVehicleDataDelegate is a method reference with an int (vehicle) and VehicleData (data) parameters
        /// </summary>
        /// <param name="vehicle">The network id of the vehicle</param>
        /// <param name="data">A VehicleData object</param>
        public delegate void SetVehicleDataDelegate(int vehicle, VehicleData data);
        /// <summary>
        /// Sets vehicle related data
        /// </summary>
        public static SetVehicleDataDelegate SetVehicleData;

        /// <summary>
        /// ExcludeVehicleFromTrafficStopDelegate is a method reference with an int (networkID) and bool (exclude) parameters
        /// </summary>
        /// <param name="networkID">The network ID of the vehicle</param>
        /// <param name="exclude">Enable/Disable traffic stop functions</param>
        public delegate void ExcludeVehicleFromTrafficStopDelegate(int networkID, bool exclude);
        /// <summary>
        /// Enables/Disables traffic stop functions on a vehicle (with the given network id)
        /// </summary>
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
        /// <summary>
        /// IsPlayerPerformingTrafficStopDelegate is a method reference with zero parameter and with return type of `bool`
        /// </summary>
        /// <returns>Whether the local player is performing a traffic stop or not</returns>
        public delegate bool IsPlayerPerformingTrafficStopDelegate();
        /// <summary>
        /// Returns whether the local player is in a traffic stop or not
        /// </summary>
        public static IsPlayerPerformingTrafficStopDelegate IsPlayerPerformingTrafficStop;
        /// <summary>
        /// GetVehicleFromTrafficStopDelegate is a method reference with zero parameter and with return type of `Vehicle`
        /// </summary>
        /// <returns>The vehicle from the traffic stop</returns>
        public delegate Vehicle GetVehicleFromTrafficStopDelegate();
        /// <summary>
        /// Returns the pulled over vehicle
        /// </summary>
        public static GetVehicleFromTrafficStopDelegate GetVehicleFromTrafficStop;
        /// <summary>
        /// GetDriverFromTrafficStopDelegate is a method reference with zero parameter and with return type of `Ped`
        /// </summary>
        /// <returns>The driver ped of the pulled over vehicle</returns>
        public delegate Ped GetDriverFromTrafficStopDelegate();
        /// <summary>
        /// Returns the driver ped from of pulled over vehicle
        /// </summary>
        public static GetDriverFromTrafficStopDelegate GetDriverFromTrafficStop;
        /// <summary>
        /// GetPassengersFromTrafficStopDelegate is a method reference with zero parameter and with return type of `Ped[]`
        /// </summary>
        /// <returns>An array of passenger peds from the pulled over vehicle (driver ped will not be added into the array)</returns>
        public delegate Ped[] GetPassengersFromTrafficStopDelegate();
        /// <summary>
        /// Returns an array of passenger peds of the pulled over vehicle.
        /// </summary>
        public static GetPassengersFromTrafficStopDelegate GetPassengersFromTrafficStop;
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
        public delegate Task<PedData> GetPedDataDelegate(int NetworkID);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NetworkID">The network id of the given entity</param>
        /// <param name="PedData">The PedData instance containing custom properties.</param>
        ///  /// <seealso cref="PedData"/>
        public delegate void SetPedDelegate(int NetworkID, PedData PedData);
        /// <summary>
        /// Sets information for the given ped.
        /// Use the method inside of the <see cref="Init"/> or <see cref="OnStart"/> methods or in an external script.<br /><br />
        /// <example>
        /// <code>
        /// PedData myData = new PedData();<br />
        /// myData.FirstName = "John";<br />
        /// myData.LastName = "Doe";<br />
        /// myData.AlcoholLevel = 0.20;<br />
        /// myData.DrugsUsed = new Drugs[] { Drugs.Meth }; <br />
        /// myData.Items = new List&lt;Item&gt; { new Item { Name = "knife", IsIllegal = true } }; <br />
        /// SetPedData(myPed.NetworkId, myData);
        /// ...
        /// or
        /// ...
        /// Ped ped;
        /// ped.SetData(); // Extension method
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="NetworkID">The network id of the pedestrian.</param>
        /// <param name="PedData">The PedData instance containing custom properties.</param>
        /// <seealso cref="PedData"/>
        public static SetPedDelegate SetPedData;

        public delegate Task<Ped> SpawnPedDelegate(Model model, Vector3 position, float heading);
        /// <summary>
        /// Requests the server to spawn a ped with the given parameters.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="position">Position</param>
        /// <param name="heading">Heading</param>
        /// <returns>Task</returns>
        /// <seealso cref="SpawnPedDelegate"/>
        public static SpawnPedDelegate SpawnPed;

        public delegate Task<Vehicle> SpawnVehicleDelegate(Model model, Vector3 position, float heading);
        /// <summary>
        /// Requests the server to spawn a vehicle with the given parameters.
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="position">Position</param>
        /// <param name="heading">Heading</param>
        /// <returns>Task</returns>
        /// <seealso cref="SpawnVehicleDelegate"/>
        public static SpawnVehicleDelegate SpawnVehicle;
        #endregion

        #region Player
        /// <summary>
        /// <para>Call GetPlayerData() if you want to access the PlayerData object. </para>
        /// <para>Please note that you cannot call this method in the constructor of your callout. </para>
        /// <para>Use the method inside of the <see cref="Init"/> or <see cref="OnStart"/> methods.</para>
        /// <para>The following properties can be accessed in the PlayerData:</para>
        ///      - DisplayName (string) <br />
        ///      - Callsign (string) <br />
        ///      - Department (string)  <br />
        ///      - DepartmentID (int)  <br />
        /// </summary>
        public static Func<PlayerData> GetPlayerData;
        /// <summary>
        /// IsPlayerOnDutyDelegate is a method reference with zero parameter
        /// </summary>
        /// <returns>Whether the local player is on duty</returns>
        public delegate bool IsPlayerOnDutyDelegate();
        /// <summary>
        /// Returns whether the local player is on duty
        /// </summary>
        public static IsPlayerOnDutyDelegate IsPlayerOnDuty;
        /// <summary>
        /// SetPlayerDutyDelegate is a method reference with a bool (onDuty) parameter
        /// </summary>
        /// <param name="onDuty">On/Off</param>
        public delegate void SetPlayerDutyDelegate(bool onDuty);
        /// <summary>
        /// Sets duty status of the local player (on/off)
        /// </summary>
        public static SetPlayerDutyDelegate SetPlayerDuty;
        /// <summary>
        /// SyncBlipDelegate is a method reference with a Blip (blip) parameter
        /// </summary>
        /// <param name="blip">Blip to sync</param>
        public delegate void SyncBlipDelegate(Blip blip);
        /// <summary>
        /// Syncs the given blip with other players
        /// </summary>
        public static SyncBlipDelegate SyncBlip;
        /// <summary>
        /// SyncBlipWithRadiusDelegate is a method reference with a Blip (blip) and float (radius) parameters
        /// </summary>
        /// <param name="blip">Blip to sync</param>
        /// <param name="radius">radius of the blip</param>
        public delegate void SyncBlipWithRadiusDelegate(Blip blip,float radius);
        /// <summary>
        /// Syncs the given blip (with radius) with other players
        /// </summary>
        public static SyncBlipWithRadiusDelegate SyncBlipWithRadius;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="blip"></param>
        public delegate void SyncBlipDeleteDelegate(Blip blip);
        /// <summary>
        /// Deletes the previously synced blip object (globally)
        /// </summary>
        public static SyncBlipDeleteDelegate SyncBlipDelete;
        /// <summary>
        /// <para>SetPlayerDataFlags is an enum that contains every possible option that you can use to set player data</para>
        /// <para>Use `-1` as the department id if you want to kick a player from the player's current department</para>
        /// </summary>
        public enum SetPlayerDataFlags
        {
            DepartmentID = 1,
            Callsign = 2,
            All = DepartmentID | Callsign
        }
        /// <summary>
        /// SetPlayerDataDelegate is a method reference with a PlayerData (playerData) and SetPlayerDataFlags (flags) parameters and with return type of `Task`
        /// </summary>
        /// <param name="playerData">The player data object</param>
        /// <param name="flags">Flags</param>
        /// <returns>(awaitable) Task</returns>
        public delegate Task SetPlayerDataDelegate(PlayerData playerData, SetPlayerDataFlags flags);
        /// <summary>
        /// Sets player related data (persistent)
        /// </summary>
        public static SetPlayerDataDelegate SetPlayerData;
        /// <summary>
        /// Returns the closest ped.
        /// </summary>
        public static Ped GetClosestPed(Ped p)
        {
            Dictionary<Ped, float> closestPeds = new Dictionary<Ped, float>();
            World.GetAllPeds()
                .Where(ped => ped != Game.PlayerPed).ToList()
                .ForEach(ped => closestPeds.Add(ped, ped.Position.DistanceTo(p.Position)));

            if (closestPeds.Count == 0)
            {
                return null;
            }

            return closestPeds.OrderBy(distance => distance.Value).FirstOrDefault().Key;
        }
        /// <summary>
        /// Returns a random position in the player's department area.
        /// </summary>
        public static Func<Vector3> GetRandomPosInPlayerDepartmentArea;
        #endregion
    }
}
