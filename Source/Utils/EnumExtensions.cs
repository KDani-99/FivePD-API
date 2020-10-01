using System;
using System.Collections.Generic;
using System.Linq;

namespace FivePD.API.Utils
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get all values of <typeparamref name="TEnum"/> in an iterable collection.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum</typeparam>
        public static IEnumerable<TEnum> EnumAsEnumerable<TEnum>() where TEnum : struct
            => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

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
