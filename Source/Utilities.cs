using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using System.Linq;
using FivePD.API.Utils;

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

        public delegate void EnableModDelegate();
        /// <summary>
        /// Enables the mod for the local player. Can be useful for integrating other resources.
        /// Should only be used in Plugins.
        /// Can be called dynamically. (Allows you to toggle)
        /// </summary>
        public static EnableModDelegate EnableMod;

        public delegate void DisableModDelegate();
        /// <summary>
        /// Disables the mod for the local player. Can be useful for integrating other resources.
        /// Should only be used in Plugins.
        /// Can be called dynamically. (Allows you to toggle)
        /// </summary>
        public static DisableModDelegate DisableMod;

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
        public delegate Task<VehicleData> GetVehicleDataDelegate(int networkID);
        public static GetVehicleDataDelegate GetVehicleData;

        public delegate void SetVehicleDataDelegate(int vehicle, VehicleData data);
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
        public delegate bool IsPlayerPerformingTrafficStopDelegate();
        public static IsPlayerPerformingTrafficStopDelegate IsPlayerPerformingTrafficStop;

        public delegate Vehicle GetVehicleFromTrafficStopDelegate();
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
        /// Call GetPlayerData() if you want to access the PlayerData object. 
        /// Please note that you cannot call this method in the constructor of your callout. 
        /// Use the method inside of the <see cref="Init"/> or <see cref="OnStart"/> methods.<br /><br />
        /// The following properties can be accessed in the PlayerData:<br />
        ///      - DisplayName (string) <br />
        ///      - Callsign (string) <br />
        ///      - Department (string)  <br />
        ///      - DepartmentID (int)  <br />
        /// </summary>
        public static Func<PlayerData> GetPlayerData;

        public delegate bool IsPlayerOnDutyDelegate();
        public static IsPlayerOnDutyDelegate IsPlayerOnDuty;

        public delegate void SetPlayerDutyDelegate(bool onDuty);
        public static SetPlayerDutyDelegate SetPlayerDuty;

        public delegate void SyncBlipDelegate(Blip blip);
        public static SyncBlipDelegate SyncBlip;

        public delegate void SyncBlipWithRadiusDelegate(Blip blip,float radius);
        public static SyncBlipWithRadiusDelegate SyncBlipWithRadius;
        
        public delegate void SyncBlipDeleteDelegate(Blip blip);
        public static SyncBlipDeleteDelegate SyncBlipDelete;
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
