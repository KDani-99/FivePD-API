using System.Collections.Generic;
using System.Text.RegularExpressions;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace CalloutsV_Online.Engine
{
    internal class WorldZone
    {
        private static readonly List<WorldZone> Zones = new List<WorldZone>();
        private readonly EWorldZoneCounty county;

        private string GameName { get; }

        internal string RealAreaName { get; }

        internal string County
        {
            get
            {
                var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

                return r.Replace(county.ToString(), " ");
            }
        }

        private WorldZone(string gameName, string areaName, EWorldZoneCounty county)
        {
            GameName = gameName;
            RealAreaName = areaName;
            this.county = county;
        }


        internal static WorldZone GetZoneAtPosition(Vector3 position)
        {
            if (Zones.Count <= 0)
                RegisterWorldZones();
            var str1 = Function.Call<string>(Hash.GET_NAME_OF_ZONE, position.X, position.Y, position.Z);
            var str2 = str1.ToLower();
            foreach (var worldZone in Zones)
                if (worldZone.GameName.ToLower() == str2)
                {
                    if (str2 == "sanand" && position.X > 3000.0 && position.Y < -4000.0)
                        return new WorldZone("NorthYankton", "North Yankton", EWorldZoneCounty.NorthYankton);
                    return worldZone;
                }

            return new WorldZone(str1, str1, EWorldZoneCounty.SanAndreas);
        }

        private static void RegisterWorldZones()
        {
            Zones.Add(new WorldZone("AirP", "Los Santos International", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Alamo", "Alamo Sea", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Alta", "Alta", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("ArmyB", "AFB Zancudo", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("BanhamCa", "Banham Canyon", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Banning", "Banning", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Baytre", "Baytree Canyon", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Beach", "Vespucci Beach", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("BhamCa", "Banham Canyon", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("BradP", "Braddock Pass", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("BradT", "Braddock Tunnel", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Burton", "Burton", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("CANNY", "Raton Canyon", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("CCreak", "Cassidy Creek", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("CHIL", "Vinewood Hills", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("CHU", "Chumash", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("CMSW", "Chiliad Wilderness", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("CalafB", "Calafia Bridge", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("ChamH", "Chamberlain Hills", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Cypre", "Cypress Flats", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("DTVine", "Downtown Vinewood", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Davis", "Davis", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("DeLBe", "Del Perro Beach", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("DelPe", "Del Perro", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("DelSol", "Puerto Del Sol", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Desrt", "Gd. Senora Desert", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Downt", "Downtown", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("EBuro", "El Burro Heights", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("ELGorL", "El Gordo Lighthouse", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("East_V", "East Vinewood", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Elysian", "Elysian Island", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Galfish", "Galilee", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Galli", "Galilep Park", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Golf", "GWC Golf Club", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("GrapeS", "Grapeseed", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Greatc", "Great Chaparral", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Harmo", "Harmony", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Hawick", "Hawick", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Hors", "Vinewood Racetrack", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("HumLab", "Humane Labs", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Jail", "Bolingbroke Pen.", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Koreat", "Little Seoul", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("LAct", "Land Act Reservoir", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("LDam", "Land Act Dam", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("LMesa", "La Mesa", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Lago", "Lago Zancudo", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("LegSqu", "Legion Square", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("LosPuer", "La Puerta", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("MTChil", "Mount Chiliad", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("MTGordo", "Mount Gordo", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("MTJose", "Mount Josiah", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Mirr", "Mirror Park", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Morn", "Morningwood", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Movie", "Backlot City", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Murri", "Murrieta Heights", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("NCHU", "North Chumash", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Noose", "NOOSE HQ", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Observ", "Galileo Observatory", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Oceana", "Pacific Ocean", EWorldZoneCounty.SanAndreas));
            Zones.Add(new WorldZone("PBOX", "Pillbox Hill", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("PBluff", "Pacific Bluffs", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("PalCov", "Plaeto Cove", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("PalFor", "Paleto Forest", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("PalHigh", "Palomino Highlands", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Paleto", "Paleto Bay", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Palmpow", "Palmer-T. Pow. Sta.", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("ProcoB", "Procopio Beach", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Prol", "Ludendorff", EWorldZoneCounty.NorthYankton));
            Zones.Add(new WorldZone("RANCHO", "Rancho", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("RGLEN", "Richman Glen", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("RTRAK", "Redwood Lights Track", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Richm", "Richman", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Rockf", "Rockford Hills", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("SKID", "Mission Row", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("STRAW", "Strawberry", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("SanChia", "San Chiansky Mountains", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Sandy", "Sandy Shores", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Slab", "Stab City", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("Stad", "Maze Bank Arena", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("TEXTI", "Textile City", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Tatamo", "Tataviam Mountains", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("Termina", "Terminal", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("TongVaH", "Tongava Hills", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("TongvaV", "Tongava Valley", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("VCana", "Vespucci Canals", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Vesp", "Vespucci", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Vine", "Vinewood", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("WVine", "West Vinewood", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("WindF", "Ron Alt. Wind Farm", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("ZP_ORT", "Port of South LS", EWorldZoneCounty.LosSantos));
            Zones.Add(new WorldZone("Zancudo", "Zancudo River", EWorldZoneCounty.BlaineCounty));
            Zones.Add(new WorldZone("zQ_UAR", "Davis Quartz Quarry", EWorldZoneCounty.LosSantosCounty));
            Zones.Add(new WorldZone("SanAnd", "San Andreas", EWorldZoneCounty.SanAndreas));
        }


        private enum EWorldZoneCounty
        {
            LosSantos,
            LosSantosCounty,
            BlaineCounty,
            SanAndreas,
            NorthYankton
        }
    }
}
