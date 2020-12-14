using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;

namespace Callouts
{
    internal static class Utils
    {
        internal static readonly Random rnd = new Random();

        /// <summary>
        /// Generated a random Vector3 with the given min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal static Vector3 GetRandomPosition(int min = 200, int max = 750)
        {
            int distance = rnd.Next(min, max);
            float offsetX = rnd.Next(-1 * distance, distance);
            float offsetY = rnd.Next(-1 * distance, distance);
            return new Vector3(offsetX, offsetY, 0);
        }
        
        /// <summary>
        /// Blocks the ped from doing default GTA events
        /// </summary>
        /// <param name="p"></param>
        internal static void KeepTask(this Ped p)
        {
            p.BlockPermanentEvents = true;
            p.AlwaysKeepTask = true;
        }

        /// <summary>
        /// Returns the closest ped from the given list relative to the ped
        /// </summary>
        /// <param name="p"></param>
        /// <param name="peds"></param>
        /// <returns></returns>
        internal static Ped GetClosestPedFromPedList(this Ped p, List<Ped> peds)
        {
            return peds
                .OrderBy(x => World.GetDistance(x.Position, p.Position))
                .FirstOrDefault();
        }
    }
}