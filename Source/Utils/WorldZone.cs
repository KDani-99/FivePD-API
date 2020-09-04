using System.Linq;
using CitizenFX.Core;
using System.Collections.Generic;

namespace FivePD.API.Utils
{
    class WorldZone
    {
        private readonly EWorldZoneCounty county;
        private static readonly IReadOnlyList<WorldZone> Zones = new List<WorldZone>() {
            #region Zones
            new WorldZone("AirP", "Los Santos International", EWorldZoneCounty.LosSantos),
            new WorldZone("Alamo", "Alamo Sea", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Alta", "Alta", EWorldZoneCounty.LosSantos),
            new WorldZone("ArmyB", "AFB Zancudo", EWorldZoneCounty.BlaineCounty),
            new WorldZone("BanhamCa", "Banham Canyon", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Banning", "Banning", EWorldZoneCounty.LosSantos),
            new WorldZone("Baytre", "Baytree Canyon", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Beach", "Vespucci Beach", EWorldZoneCounty.LosSantos),
            new WorldZone("BhamCa", "Banham Canyon", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("BradP", "Braddock Pass", EWorldZoneCounty.BlaineCounty),
            new WorldZone("BradT", "Braddock Tunnel", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Burton", "Burton", EWorldZoneCounty.LosSantos),
            new WorldZone("CANNY", "Raton Canyon", EWorldZoneCounty.BlaineCounty),
            new WorldZone("CCreak", "Cassidy Creek", EWorldZoneCounty.BlaineCounty),
            new WorldZone("CHIL", "Vinewood Hills", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("CHU", "Chumash", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("CMSW", "Chiliad Wilderness", EWorldZoneCounty.LosSantos),
            new WorldZone("CalafB", "Calafia Bridge", EWorldZoneCounty.BlaineCounty),
            new WorldZone("ChamH", "Chamberlain Hills", EWorldZoneCounty.LosSantos),
            new WorldZone("Cypre", "Cypress Flats", EWorldZoneCounty.LosSantos),
            new WorldZone("DTVine", "Downtown Vinewood", EWorldZoneCounty.LosSantos),
            new WorldZone("Davis", "Davis", EWorldZoneCounty.LosSantos),
            new WorldZone("DeLBe", "Del Perro Beach", EWorldZoneCounty.LosSantos),
            new WorldZone("DelPe", "Del Perro", EWorldZoneCounty.LosSantos),
            new WorldZone("DelSol", "Puerto Del Sol", EWorldZoneCounty.LosSantos),
            new WorldZone("Desrt", "Gd. Senora Desert", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Downt", "Downtown", EWorldZoneCounty.LosSantos),
            new WorldZone("EBuro", "El Burro Heights", EWorldZoneCounty.LosSantos),
            new WorldZone("ELGorL", "El Gordo Lighthouse", EWorldZoneCounty.BlaineCounty),
            new WorldZone("East_V", "East Vinewood", EWorldZoneCounty.LosSantos),
            new WorldZone("Elysian", "Elysian Island", EWorldZoneCounty.LosSantos),
            new WorldZone("Galfish", "Galilee", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Galli", "Galilep Park", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Golf", "GWC Golf Club", EWorldZoneCounty.LosSantos),
            new WorldZone("GrapeS", "Grapeseed", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Greatc", "Great Chaparral", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Harmo", "Harmony", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Hawick", "Hawick", EWorldZoneCounty.LosSantos),
            new WorldZone("Hors", "Vinewood Racetrack", EWorldZoneCounty.LosSantos),
            new WorldZone("HumLab", "Humane Labs", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Jail", "Bolingbroke Pen.", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Koreat", "Little Seoul", EWorldZoneCounty.LosSantos),
            new WorldZone("LAct", "Land Act Reservoir", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("LDam", "Land Act Dam", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("LMesa", "La Mesa", EWorldZoneCounty.LosSantos),
            new WorldZone("Lago", "Lago Zancudo", EWorldZoneCounty.BlaineCounty),
            new WorldZone("LegSqu", "Legion Square", EWorldZoneCounty.LosSantos),
            new WorldZone("LosPuer", "La Puerta", EWorldZoneCounty.LosSantos),
            new WorldZone("MTChil", "Mount Chiliad", EWorldZoneCounty.BlaineCounty),
            new WorldZone("MTGordo", "Mount Gordo", EWorldZoneCounty.BlaineCounty),
            new WorldZone("MTJose", "Mount Josiah", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Mirr", "Mirror Park", EWorldZoneCounty.LosSantos),
            new WorldZone("Morn", "Morningwood", EWorldZoneCounty.LosSantos),
            new WorldZone("Movie", "Backlot City", EWorldZoneCounty.LosSantos),
            new WorldZone("Murri", "Murrieta Heights", EWorldZoneCounty.LosSantos),
            new WorldZone("NCHU", "North Chumash", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Noose", "NOOSE HQ", EWorldZoneCounty.LosSantos),
            new WorldZone("Observ", "Galileo Observatory", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Oceana", "Pacific Ocean", EWorldZoneCounty.SanAndreas),
            new WorldZone("PBOX", "Pillbox Hill", EWorldZoneCounty.LosSantos),
            new WorldZone("PBluff", "Pacific Bluffs", EWorldZoneCounty.LosSantos),
            new WorldZone("PalCov", "Plaeto Cove", EWorldZoneCounty.BlaineCounty),
            new WorldZone("PalFor", "Paleto Forest", EWorldZoneCounty.BlaineCounty),
            new WorldZone("PalHigh", "Palomino Highlands", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Paleto", "Paleto Bay", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Palmpow", "Palmer-T. Pow. Sta.", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("ProcoB", "Procopio Beach", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Prol", "Ludendorff", EWorldZoneCounty.NorthYankton),
            new WorldZone("RANCHO", "Rancho", EWorldZoneCounty.LosSantos),
            new WorldZone("RGLEN", "Richman Glen", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("RTRAK", "Redwood Lights Track", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Richm", "Richman", EWorldZoneCounty.LosSantos),
            new WorldZone("Rockf", "Rockford Hills", EWorldZoneCounty.LosSantos),
            new WorldZone("SKID", "Mission Row", EWorldZoneCounty.LosSantos),
            new WorldZone("STRAW", "Strawberry", EWorldZoneCounty.LosSantos),
            new WorldZone("SanChia", "San Chiansky Mountains", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Sandy", "Sandy Shores", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Slab", "Stab City", EWorldZoneCounty.BlaineCounty),
            new WorldZone("Stad", "Maze Bank Arena", EWorldZoneCounty.LosSantos),
            new WorldZone("TEXTI", "Textile City", EWorldZoneCounty.LosSantos),
            new WorldZone("Tatamo", "Tataviam Mountains", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("Termina", "Terminal", EWorldZoneCounty.LosSantos),
            new WorldZone("TongVaH", "Tongava Hills", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("TongvaV", "Tongava Valley", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("VCana", "Vespucci Canals", EWorldZoneCounty.LosSantos),
            new WorldZone("Vesp", "Vespucci", EWorldZoneCounty.LosSantos),
            new WorldZone("Vine", "Vinewood", EWorldZoneCounty.LosSantos),
            new WorldZone("WVine", "West Vinewood", EWorldZoneCounty.LosSantos),
            new WorldZone("WindF", "Ron Alt. Wind Farm", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("ZP_ORT", "Port of South LS", EWorldZoneCounty.LosSantos),
            new WorldZone("Zancudo", "Zancudo River", EWorldZoneCounty.BlaineCounty),
            new WorldZone("zQ_UAR", "Davis Quartz Quarry", EWorldZoneCounty.LosSantosCounty),
            new WorldZone("SanAnd", "San Andreas", EWorldZoneCounty.SanAndreas)
            #endregion
        };

        private string GameName { get; }

        internal string RealAreaName { get; }

        internal string County
        {
            get => county.ToFriendlyString();
        }

        private WorldZone(string gameName, string areaName, EWorldZoneCounty county)
        {
            GameName = gameName;
            RealAreaName = areaName;
            this.county = county;
        }

        internal static WorldZone GetZoneAtPosition(Vector3 position)
        {
            string zoneName = World.GetZoneDisplayName(position);
            string zoneNameLower = zoneName.ToLower();

            WorldZone worldZone = Zones.FirstOrDefault(zone => zone.GameName.ToLower() == zoneNameLower);

            if (worldZone == null) return new WorldZone(zoneName, zoneName, EWorldZoneCounty.SanAndreas);

            if (zoneNameLower == "sanand" && position.X > 3000.0 && position.Y < -4000.0) return new WorldZone("NorthYankton", "North Yankton", EWorldZoneCounty.NorthYankton);

            return worldZone;
        }

        public enum EWorldZoneCounty
        {
            LosSantos,
            LosSantosCounty,
            BlaineCounty,
            SanAndreas,
            NorthYankton
        }
    }
}