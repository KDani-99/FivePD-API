using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
namespace CalloutAPI
{
    /* This class is referenced in the main plugin to call the CalloutAPI's `LoadCallouts` method */
    public abstract class Callout
    {
        /// <summary>
        /// The list of players that has accepted the callout
        /// NOTE: The base player is always going to be added by default
        /// </summary>
        public List<Ped> AssignedPlayers;
        /// <summary>
        /// The description of the callout that will be displayed in the computer (Detailed)
        /// </summary>
        public string CalloutDescription;
        /// <summary>
        /// The shortname of the callout that will be displayed on the notification's header (eg. Trespassing)
        /// </summary>
        public string ShortName;
        /// <summary>
        /// DON'T EDIT! CaseID will be automatically generated
        /// </summary>
        public string CaseID;
        /// <summary>
        /// The amount of XP that this call will add to the player's progression once completed<br/>
        /// (NOTE: Server owners can set a fix amount, which will override this XP)
        /// </summary>
        public uint XP;
        /// <summary>
        /// Response Code (eg. 1,2,3,...)
        /// </summary>
        public int ResponseCode;
        /// <summary>
        /// The distance that the distance checker will use to detemine whether the player is in range and start the callout 
        /// </summary>
        public float StartDistance;
        /// <summary>
        /// Location of the callout. This position will be marked on the map and set as a waypoint for the player
        /// </summary>
        public Vector3 Location;
        /// <summary>
        /// The blip will be created over the `Location`
        /// </summary>
        public Blip Marker;
        /// <summary>
        /// Do not set it! It'll be used as a unique identifier by CalloutManager when creating a call
        /// </summary>
        public string Identifier;
        /// <summary>
        /// It'll be set to true once the callout starts ( the player gets in range ).<br/>
        /// It is mainly used by the callout manager to determine whether to reward a player or not
        /// </summary>
        public bool Started
        {
            get;
            private set;
        }

        public float Radius;
        public Callout() { }
        /// <summary>
        /// Probability (Low-Medium-High)
        /// </summary>
        public enum Probability
        {
            Low,
            Medium,
            High
        }

        /* Gameplay related methods */

        protected async Task<Ped> SpawnPed(PedHash pedHash,Vector3 location,float heading = 0f)
        {
            uint model = (uint)pedHash;

            RequestModel(model);

            while (!HasModelLoaded(model))
                await BaseScript.Delay(0);

            Ped ped = (Ped)Entity.FromHandle(CreatePed(0, model, location.X, location.Y, location.Z, heading, true, true));
            SetEntityAsMissionEntity(ped.Handle, true, true);

            return ped;
        }
        protected async Task<Vehicle> SpawnVehicle(VehicleHash vehicleHash,Vector3 location,float heading = 0f)
        {
            uint model = (uint)vehicleHash;

            RequestModel(model);

            while (!HasModelLoaded(model))
                await BaseScript.Delay(0);

            Vehicle vehicle = (Vehicle)Entity.FromHandle(CreateVehicle(model, location.X, location.Y, location.Z, heading, true, true));
            SetEntityAsMissionEntity(vehicle.Handle, true, true);
            
            return vehicle;
        }

        protected void InitBase(Vector3 location)
        {
            this.AssignedPlayers = new List<Ped>();
            this.AssignedPlayers.Add(Game.PlayerPed);
            this.CalloutDescription = "<Unnamed Callout Description>";
            this.ShortName = "<Unnamed Callout>";
            this.ResponseCode = -1;
            this.Location = location;
            this.Identifier = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// OnAccept will be called when the player accepts the call.
        /// You must call base.OnAccept(args) to initialise the default properties
        /// </summary>
        protected void OnAccept(float circleRadius = 75f, BlipColor color = BlipColor.Yellow, BlipSprite sprite = BlipSprite.BigCircle, int alpha = 100)
        {
            int blipHandle = AddBlipForRadius(this.Location.X, this.Location.Y, this.Location.Z, circleRadius);
            this.Radius = circleRadius;
            this.Marker = new Blip(blipHandle);
            this.Marker.Sprite = sprite;
            this.Marker.Color = color;
            this.Marker.Alpha = alpha;
        }
        /// <summary>
        /// Init() will be automatically invoked by the CalloutManager<br/>
        /// Define game logic here (eg. Spawn suspects,victims,vehicles)
        /// </summary>
        public virtual async Task Init() { }

        /// <summary>
        /// (Do not call it)<br/><br/>
        /// Destructs every spawned object automatically if defined as a field or property (can be public, private, protected and static)<br/>
        /// To spawn something, defined it as a property or field, otherwise you'll manually have to delete it locally
        /// See the documentation
        /// </summary>
        #region Events
        public virtual void OnStart(Ped closest)
        {
            this.Started = true;
            if (closest.NetworkId != Game.PlayerPed.NetworkId)
            {
                this.AssignedPlayers.Add(closest);               
            }
        }
        public virtual void OnBackupCalled(int code) { } // 1,2,3,99
        public virtual void OnBackupReceived(Player player) { } // New player
        public virtual void OnPlayerRevokedBackup(Player player) { } // Called when someone stops respoding to the caller
        public virtual void OnCancelBefore() { } // Called before Destruct()
        public virtual void OnCancelAfter() { } // Called after Destruct()

        /// <summary>
        /// EndCallout() should be called when you want to terminate or end the callout.<br /><br />
        /// You should have conditions with your logic when you want to end your callout (Eg. when the player goes to a certain point on the map, ...)<br />
        /// Calling this method will automatically mark it as a completed callout<br />
        /// If you don't call this method, the user will have to manually cancel the callout.<br /><br />
        /// Called methods after calling EndCallout() method:<br />
        /// - OnCancelBefore()<br />
        /// - OnCancelAfter()
        /// </summary>
        public Action EndCallout { get; set; }

        /* Experimental below */
        public List<object> Clues;
        /* If a criminal gets X distance away, attach a question mark nearby at every Y secs */
        protected void AttachClueToPed(Ped ped,float minDistance,int repeat = 15)
        {
            if (this.Clues == null)
                this.Clues = new List<object>();

            var clue = new
            {
                PedNetworkID = ped.NetworkId,
                minDistance,
                repeat
            };

            this.Clues.Add(clue);
        }        

        /// Receive Tick from the callout manager
        public async Task ReceiveTick()
        {
            if(Tick != null)
                await Tick.Invoke();
        }
        protected event Func<Task> Tick;

        #endregion
    }
    /// <summary>
    /// Bundles callout properties (CalloutPropertiesAttribute is used in the CalloutManager)
    /// </summary>
    public class CalloutPropertiesAttribute : Attribute
    {
        /// <summary>The name of the callout(Not the in game dispatch display name)</summary>
        public string name { get; private set; }
        public string version { get; private set; }
        /// <summary>The probability of the callout which can be: Probability.Low - Probability.Medium - Probability.High</summary>
        public Callout.Probability probability { get; private set; }

        /// <summary>
        /// Callout properties
        /// </summary>
        /// <param name="name">The name of the callout (Not the in game dispatch display name)</param>
        /// <param name="probability">Set the probability of the callout (eg. Probability.Low)</param>
        public CalloutPropertiesAttribute(string name, string version,Callout.Probability probability)
        {
            this.name = name;
            this.version = version;
            this.probability = probability;
        }
    }
}
