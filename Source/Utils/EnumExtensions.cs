using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FivePD.API.Utils
{
    static class EnumExtensions
    {
        internal static string ToFriendlyString(this WorldZone.EWorldZoneCounty county)
        {
            switch (county)
            {
                case WorldZone.EWorldZoneCounty.LosSantos:
                    return "Los Santos";
                case WorldZone.EWorldZoneCounty.LosSantosCounty:
                    return "Los Santos County";
                case WorldZone.EWorldZoneCounty.BlaineCounty:
                    return "Blaine County";
                case WorldZone.EWorldZoneCounty.SanAndreas:
                    return "San Andreas";
                case WorldZone.EWorldZoneCounty.NorthYankton:
                    return "North Yankton";
                default:
                    return "Error";
            }
        }
    }
}
