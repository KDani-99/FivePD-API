# FivePD-API

### Get started

##### Creating a new callout using the example
1. Download the CalloutExample project.
2. Open the project and start making your new callout.

##### From scratch
1. Create a new C# Class Library project and make sure the target framework version is 4.5.2.
2. Add CalloutAPI.dll as a reference.
3. Add CitizenFX.Core.dll as a reference.
(For detailed instructions, [click here](https://docs.fivem.net/docs/scripting-manual/runtimes/csharp/ "refer here"))

##### Fields

A few of these fields should never be used, marked with red.

Contains every player who have been dispatched for this call. Players are automatically added/removed, so you should leave as it is.
```cs
public List<Ped> AssignedPlayers;
```
Description of the call. This information will be displayed in the computer.
```cs
public string CalloutDescription;
```
Short name of the call. This name will be displayed when the callout pops up as a new mission. (Eg. Trespassing)
```cs
public string ShortName;
```
Case ID is the unique identifier of the call in the database. It'll be automatically generated. <span style="color:red;">(**Don't edit this.**)</span>
```cs
public string CaseID;
```
XP is the amount of XP this call should reward the player after completing the call. XP reward only works if the server owner enables callout reward progression. XP can't be less than 0!
```cs
public uint XP;
```
Response code of the call.
```cs
public int ResponseCode;
```
The distance that the distance checker will use to detemine whether an assigned player is in range and start the callout.
```cs
public float StartDistance;
```
Location of the callout. This position will be marked on the map and set as a waypoint for the player
```cs
public Vector3 Location;
```
The blip that will be created over the \`Location`
```cs
public Blip Marker;
```
Identifier differs from the callout ID, it's used for computer related events. <span style="color:red;">(**Don't edit this.**)</span>
```cs
public string Identifier;
```
It'll be set to true once the callout starts ( the player is in range ).
It is mainly used by the callout manager to determine whether to reward a player or not.
<span style="color:red;">(**Don't edit this.**)</span>
```cs
public bool Started;
```
Callout probability. How often the callout is available.
```cs
public enum Probability
{
    Low,
    Medium,
    High
}
```
#### Available Methods
```cs
protected async Task<Ped> SpawnPed(PedHash pedHash,Vector3 location,float heading = 0f) {}
```
```cs
protected async Task<Vehicle> SpawnVehicle(VehicleHash vehicleHash,Vector3 location,float heading = 0f) {}
```
```cs
protected void InitBase(Vector3 location) {}
```
```cs
protected void OnAccept(float circleRadius = 75f, BlipColor color = BlipColor.Yellow, BlipSprite sprite = BlipSprite.BigCircle, int alpha = 100) {}
```
```cs
public virtual async Task Init() { }
```
#### Event Methods
You can define your own logic for events. These events will be called by the calloutmanager.

What should these be used for?
Well, events can be pretty useful if you want different actions to happen in different events. For example, you can change the ped task to attack the player, flee or give up.

New events will be added in the future. If you'd like to have a new event added, create a pull request.

```cs
public virtual void OnStart(Ped closest) {}
```
OnStart is called when the callout starts. 
```cs
public virtual void OnBackupCalled(int code) { } // 1,2,3,99
```
OnBackupCalled is called when the player calls for backup (backup code is passed as a param)
```cs
public virtual void OnPlayerRevokedBackup(Player player) { } 
```
OnPlayerRevokedBackup is called when someone stops respoding to the backup request. (The player who accepted the backup, not the player who requested it!)
```cs
public virtual void OnCancelBefore() { } // Called before Destruct()
```
OnCancelBefore is called before calloutmanager cleans up the call (entities).. 
```cs
public virtual void OnCancelAfter() { } // Called after Destruct()
```
OnCancelAfter is called after calloutmanager cleans up the call (entities).
